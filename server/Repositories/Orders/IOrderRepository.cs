using ShopAppApi.Data;
using ShopAppApi.Request;
using ShopAppApi.Response;
using ShopAppApi.ViewModels;

namespace ShopAppApi.Repositories.Orders
{
    public interface IOrderRepository
    {
        Task<PaginatedList<OrderVM>> GetAll(OrderRequest request);
        public Task Create(StoreOrderRequest request);
        public Task Update(long Id, UpdateMenuRequest menu);
        Task<OrderVM> Show(long Id);
        Task<List<OrderDetailVM>> GetOrderDetails(long OrderId);
    }
}
