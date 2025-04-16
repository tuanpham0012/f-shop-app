using Microsoft.EntityFrameworkCore;
using ShopAppApi.Data;
using ShopAppApi.Repositories.RedisCache;
using ShopAppApi.Request;
using ShopAppApi.Response;
using ShopAppApi.ViewModels;

namespace ShopAppApi.Repositories.Orders
{
    public class OrderRepository(ShopAppContext context, IRedisCache cache) : IOrderRepository
    {

        public async Task Create(StoreMenuRequest menu)
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            
            await transaction.CommitAsync();
        }

        public async Task Update(long Id, UpdateMenuRequest menu)
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            
            await transaction.CommitAsync();

        }

        public async Task<PaginatedList<OrderVM>> GetAll(OrderRequest request)
        {
            var query = context.Orders.AsQueryable().Select( o => new OrderVM
            {
                Id = o.Id,
                Code = o.Code,
                OrderDate = o.OrderDate,
                DeliveryDate = o.DeliveryDate,
                Customer = new CustomerVM{
                    Name = o.Customer.Name
                },
                User = o.User,
                PaymentMethod = new PaymentMethodVM{
                    Name = o.PaymentMethod.Name
                },
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
            if(!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query = query.Where(o => o.Code.Contains(request.Search) || o.ShippingPhone.Contains(request.Search) || o.ReceiverName.Contains(request.Search));
            }
            if(request.CustomerId != null)
            {
                query = query.Where(o => o.CustomerId == request.CustomerId);
            }
            if(request.UserId != null)
            {
                query = query.Where(o => o.UserId == request.UserId);
            }
            if(request.PaymentMethodId != null)
            {
                query = query.Where(o => o.PaymentMethodId == request.PaymentMethodId);
            }
            if(request.ShippingUnitId != null)
            {
                query = query.Where(o => o.ShippingUnitId == request.ShippingUnitId);
            }
            if(request.Status != null)
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

        public async Task<Menu> Show(long Id)
        {
            var menu = await context.Menus.SingleOrDefaultAsync(c => c.Id == Id);
            return menu ?? throw new ArgumentException("Menu not found");
        }
    }
}