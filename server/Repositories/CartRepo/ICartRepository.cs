using ShopAppApi.Data;
using ShopAppApi.Request;
using ShopAppApi.ViewModels;

namespace ShopAppApi.Repositories.CartRepo
{
    public interface ICartRepository
    {
        public void AddToCart(AddCartRequest request);
        public void UpdateCart(long Id, UpdateCartRequest request);
        public void DeleteCart(long Id);
        public void DeleteAllCart(DeleteCartRequest request);
        public Task<List<CartVM>> GetCart(long CustomerId);
    }
}