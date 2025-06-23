using System.Globalization;
using Microsoft.EntityFrameworkCore;
using ShopAppApi.Data;
using ShopAppApi.Helpers.Interfaces;
using ShopAppApi.Request;
using ShopAppApi.Response;
using ShopAppApi.Services.RedisCache;
using ShopAppApi.ViewModels;

namespace ShopAppApi.Repositories.Orders
{
    public class OrderRepository(ShopAppContext context, IRedisCache cache, IFileHelper fileHelper) : IOrderRepository
    {

        public async Task Create(StoreOrderRequest request)
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            var order = new Order
            {
                Code = Guid.NewGuid().ToString("D"),
                OrderDate = DateOnly.FromDateTime(DateTime.Now),
                CustomerId = request.CustomerId,
                PaymentMethodId = request.PaymentMethodId,
                ShippingUnitId = request.ShippingUnitId,
                TotalPrice = 0,
                TaxFee = 0,
                Note = request.Note,
                TotalAmount = 0,
                DiscountAmount = 0,
                ShippingFee = 0,
                ShippingAddress = request.Address,
                ShippingPhone = request.PhoneNumber,
                ReceiverName = request.ReceiverName,
                Status = 0,
                CreatedAt = DateTime.Now
            };
            foreach (var detail in request.OrderDetails)
            {
                if (detail.Quantity <= 0)
                {
                    throw new ArgumentException("Quantity must be greater than zero.");
                }
                var sku = await context.Skus.FindAsync(detail.SkuId) ?? throw new ArgumentException("Sku not found");
                var orderDetailPrice = sku.Price * detail.Quantity;
                order.TotalPrice += orderDetailPrice;
                order.TotalAmount += orderDetailPrice;

                var orderDetail = new OrderDetail
                {
                    ProductId = sku.ProductId,
                    ProductName = sku.Product.Name,
                    SkuId = sku.Id,
                    Quantity = detail.Quantity,
                    UnitPrice = sku.Price,
                    TotalAmount = orderDetailPrice,
                    DiscountAmount = 0, // Assuming no discount for simplicity
                    OrderId = order.Id
                };
                context.OrderDetails.Add(orderDetail);
            }
            context.Orders.Add(order);
            await context.SaveChangesAsync();
            await transaction.CommitAsync();
        }

        public async Task Update(long Id, UpdateMenuRequest menu)
        {
            using var transaction = await context.Database.BeginTransactionAsync();

            await transaction.CommitAsync();

        }

        public async Task<PaginatedList<OrderVM>> GetAll(OrderRequest request)
        {
            var query = context.Orders.AsQueryable().Select(o => new OrderVM
            {
                Id = o.Id,
                Code = o.Code,
                OrderDate = o.OrderDate.ToString("MM/dd/yyyy"),
                DeliveryDate = o.DeliveryDate.HasValue ? o.DeliveryDate.Value.ToString("MM/dd/yyyy") : "",
                Customer = new CustomerVM
                {
                    Name = o.Customer.Name
                },
                User = o.User,
                PaymentMethod = new PaymentMethodVM
                {
                    Name = o.PaymentMethod.Name
                },
                TotalPrice = o.TotalPrice,
                TaxFee = o.TaxFee,
                Note = o.Note,
                ShippingAddress = o.ShippingAddress,
                ShippingPhone = o.ShippingPhone,
                TotalAmount = o.TotalAmount,
                ShippingFee = o.ShippingFee,
                DiscountAmount = o.DiscountAmount,
                ReceiverName = o.ReceiverName,
                Status = o.Status,
                TextStatus = TextStatus(o.Status),
                ProductCount = o.OrderDetails.Count
            });
            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query = query.Where(o => o.Code.Contains(request.Search) || o.ShippingPhone.Contains(request.Search) || o.ReceiverName.Contains(request.Search));
            }
            if (request.CustomerId != null)
            {
                query = query.Where(o => o.CustomerId == request.CustomerId);
            }
            if (request.UserId != null)
            {
                query = query.Where(o => o.UserId == request.UserId);
            }
            if (request.PaymentMethodId != null)
            {
                query = query.Where(o => o.PaymentMethodId == request.PaymentMethodId);
            }
            if (request.ShippingUnitId != null)
            {
                query = query.Where(o => o.ShippingUnitId == request.ShippingUnitId);
            }
            if (request.Status != null)
            {
                query = query.Where(o => o.Status == request.Status);
            }
            query = query.OrderByDescending(x => x.Id);

            var Data = await PaginatedList<OrderVM>.CreateAsync(query.AsSingleQuery().AsNoTracking(), request.Page, request.PageSize);

            return Data;
        }

        private static string TextStatus(int Status)
        {
            switch (Status)
            {
                case 0:
                    return "Đang xử lý";
                case 1:
                    return "Chuẩn bị hàng";
                case 2:
                    return "Đã gửi";
                case 3:
                    return "Đã nhận hàng";
                case 4:
                    return "Đã huỷ";
                default:
                    return "Đang xử lý";
            }
        }

        public async Task<OrderVM> Show(long Id)
        {
            CultureInfo viVn = new CultureInfo("vi-VN");
            var data = await context.Orders.Select(o => new OrderVM
            {
                Id = o.Id,
                Code = o.Code,
                OrderDate = o.OrderDate.ToString("MM/dd/yyyy"),
                DeliveryDate = o.DeliveryDate.HasValue ? o.DeliveryDate.Value.ToString("MM/dd/yyyy") : "",
                Customer = new CustomerVM
                {
                    Name = o.Customer.Name
                },
                User = o.User,
                PaymentMethod = new PaymentMethodVM
                {
                    Name = o.PaymentMethod.Name
                },
                ShippingUnit = new ShippingUnitVM
                {
                    Name = o.ShippingUnit.Name
                },
                TotalPrice = o.TotalPrice,
                TaxFee = o.TaxFee,
                Note = o.Note,
                ShippingAddress = o.ShippingAddress,
                ShippingPhone = o.ShippingPhone,
                TotalAmount = o.TotalAmount,
                ShippingFee = o.ShippingFee,
                DiscountAmount = o.DiscountAmount,
                ReceiverName = o.ReceiverName,
                Status = o.Status,
                TextStatus = TextStatus(o.Status),
                ProductCount = o.OrderDetails.Count,
                OrderDetails = new List<OrderDetailVM>(),
            }).SingleOrDefaultAsync(c => c.Id == Id) ?? throw new ArgumentException("Order not found");
            return data;
        }

        public async Task<List<OrderDetailVM>> GetOrderDetails(long OrderId)
        {
            var data = await context.OrderDetails.AsQueryable().Where(o => o.OrderId == OrderId).Select(o => new OrderDetailVM
            {
                ProductName = o.ProductName,
                UnitPrice = o.UnitPrice,
                Quantity = o.Quantity,
                TotalAmount = o.TotalAmount,
                DiscountAmount = o.DiscountAmount,
                UnitDiscount = o.UnitDiscount,
                TaxFee = o.TaxFee,
                Sku = new SkuVM
                {
                    ProductId = o.Sku.ProductId,
                    Barcode = o.Sku.Barcode,
                    Name = o.Sku.Name,
                    ImagePath = fileHelper.GetLink(o.Sku.ImageCode),
                    Variants = o.Sku.Variants.Select(v => new VariantVM
                    {
                        Code = v.OptionValue.Code ?? "",
                        OptionName = v.Option.Name,
                        ValueName = v.OptionValue.Label
                    }).ToList()
                }
            }).AsNoTracking().ToListAsync();

            return data;
        }
    }
}