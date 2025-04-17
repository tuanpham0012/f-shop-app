
using Microsoft.EntityFrameworkCore;
using ShopAppApi.Data;
using ShopAppApi.Helpers.Interfaces;
using ShopAppApi.Request;
using ShopAppApi.ViewModels;

namespace ShopAppApi.Repositories.CartRepo
{
    public class CartRepository(ShopAppContext _context, IFileHelper _fileHelper) : ICartRepository
    {
        private readonly ShopAppContext context = _context;
        private readonly IFileHelper fileHelper = _fileHelper;
        public void AddToCart(AddCartRequest request)
        {
            using var transaction = context.Database.BeginTransaction();
            var sku = context.Skus.SingleOrDefault(x => x.Id == request.SkuId) ?? throw new ArgumentException("Sku Not found");
            var getCart = context.Carts.FirstOrDefault(x => x.CustomerId == request.CustomerId && x.SkuId == request.SkuId);
            if (getCart == null)
            {
                var newCart = new Cart
                {
                    CustomerId = request.CustomerId ?? 0,
                    ProductId = sku.ProductId,
                    SkuId = request.SkuId,
                    Quantity = request.Quantity,
                };
                context.Carts.Add(newCart);
                context.SaveChanges();
            }
            else
            {
                getCart.Quantity += request.Quantity;
                context.SaveChanges();
            }
            transaction.Commit();
        }

        public void UpdateCart(long Id, UpdateCartRequest request)
        {
            using var transaction = context.Database.BeginTransaction();
            var getCart = context.Carts.SingleOrDefault(x => x.Id == Id) ?? throw new ArgumentException("Cart Not found");
            switch (request.Type)
            {
                case 1:
                    getCart.Quantity++;
                    break;
                case 2:
                    getCart.Quantity--;
                    break;
                default:
                    getCart.Quantity = request.Quantity;
                    break;
            }
            // if (getCart.Quantity > getCart.Sku.Stock)
            // {
            //     throw new ArgumentException("Quantity is greater than stock");
            // }
            getCart.Quantity = getCart.Quantity < 1 ? 1 : getCart.Quantity;
            context.SaveChanges();
            transaction.Commit();
        }
        public void DeleteCart(long Id)
        {
            using var transaction = context.Database.BeginTransaction();
            var getCart = context.Carts.SingleOrDefault(x => x.Id == Id) ?? throw new ArgumentException("Cart Not found");
            context.Carts.Remove(getCart);
            context.SaveChanges();
            transaction.Commit();
        }

        public void DeleteAllCart(DeleteCartRequest request)
        {
            using var transaction = context.Database.BeginTransaction();
            var getCart = context.Carts.Where(x => request.Ids.Contains(x.Id)).ToList();
            if (getCart.Count > 0)
            {
                context.Carts.RemoveRange(getCart);
                context.SaveChanges();
            }
            transaction.Commit();
        }

        public async Task<List<CartVM>> GetCart(long CustomerId)
        {
            var getCart = await context.Carts.Where(x => x.CustomerId == CustomerId)
            .Select(x => new CartVM
            {
                Id = x.Id,
                CustomerId = x.CustomerId,
                ProductId = x.ProductId,
                SkuId = x.SkuId,
                Quantity = x.Quantity,
                Product = new ProductVM
                {
                    Id = x.Product.Id,
                    Name = x.Product.Name,
                    Barcode = x.Product.Barcode,
                    ImageThumb = fileHelper.GetLink(x.Product.ImageThumb),
                },
                Sku = new SkuVM
                {
                    Id = x.Sku.Id,
                    Barcode = x.Sku.Barcode,
                    ImageCode = x.Sku.ImageCode,
                    ImagePath = string.IsNullOrWhiteSpace(x.Sku.ImagePath) ? fileHelper.GetLink(x.Product.ImageThumb) : fileHelper.GetLink(x.Sku.ImagePath),
                    Price = x.Sku.Price,
                    Name = x.Sku.Name,
                    Stock = x.Sku.Stock,
                    Variants = x.Sku.Variants.Select(v => new VariantVM
                    {
                        Id = v.Id,
                        OptionId = v.OptionId,
                        OptionValueId = v.OptionValueId,
                        Code = v.OptionValue.Code ?? "",
                        OptionName = v.Option.Name,
                        ValueName = v.OptionValue.Label
                    }).ToList()
                },
                TotalPrice = (long)(x.Quantity * x.Sku.Price)
            }).OrderBy(x => x.ProductId).ToListAsync();
            return getCart;
        }

        public void Checkout(long CustomerId, CheckoutRequest request)
        {
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var carts = context.Carts.Include("Product").Include("Sku").Where(x => request.CartIds.Contains(x.Id)).ToList();
                Customer customer = context.Customers.SingleOrDefault(c => c.Id == CustomerId) ?? throw new AggregateException("Không tìm thấy thông tin");
                var order = new Order
                {
                    Code = Guid.NewGuid().ToString("D"),
                    OrderDate = DateOnly.FromDateTime(DateTime.Now),
                    CustomerId = CustomerId,
                    PaymentMethodId = request.PaymentMethodId,
                    ShippingUnitId = request.ShippingUnitId,
                    TotalPrice = 0,
                    TaxFee = 0,
                    Note = request.Note,
                    TotalAmount = 0,
                    DiscountAmount = 0,
                    ShippingFee = 0,
                    ShippingAddress = customer.Address,
                    ShippingPhone = customer.Phone,
                    ReceiverName = customer.Name,
                    Status = 0,
                    CreatedAt = DateTime.Now
                };
                context.Add(order);
                context.SaveChanges();
                double totalAmount = 0;
                double totalPrice = 0;
                foreach (var cart in carts)
                {
                    var orderDetailPrice = cart.Sku.Price * cart.Quantity;
                    totalPrice += orderDetailPrice;
                    totalAmount += orderDetailPrice;
                    var orderDetail = new OrderDetail
                    {
                        ProductId = cart.ProductId,
                        ProductName = cart.Product.Name,
                        SkuId = cart.SkuId,
                        Quantity = cart.Quantity,
                        UnitPrice = cart.Sku.Price,
                        TotalAmount = orderDetailPrice,
                        DiscountAmount = 0,
                        OrderId = order.Id
                    };
                    context.Add(orderDetail);
                    UpdateStatisticProduct(cart.ProductId, cart.Quantity);
                }
                context.RemoveRange(carts);
                order.TotalAmount = totalAmount;
                order.TotalPrice = totalPrice;
                context.SaveChanges();
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new ArgumentException(ex.Message);
            }
            
        }

        private void UpdateStatisticProduct(long ProductId, int Quantity)
        {
            var statistic = context.ProductStatistics.SingleOrDefault(x => x.ProductId == ProductId);
            if (statistic == null)
            {
                statistic = new ProductStatistic
                {
                    ProductId = ProductId,
                    SoldCount = Quantity,
                };
                context.Add(statistic);
            }
            else
            {
                statistic.SoldCount += Quantity;
            }
            context.SaveChanges();
        }
    }
}