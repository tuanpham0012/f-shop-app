using ShopAppApi.Data;
using ShopAppApi.Request;
using ShopAppApi.Response;
using ShopAppApi.ViewModels;

namespace ShopAppApi.Repositories.Orders
{
    public interface IOrderRepository
    {
        Task<PaginatedList<OrderVM>> GetAll(OrderRequest request);
        public Task Create(StoreMenuRequest menu);
        public Task Update(long Id, UpdateMenuRequest menu);
        public Task<Menu> Show(long Id);
    }
}
