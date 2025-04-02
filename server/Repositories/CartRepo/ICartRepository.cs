using ShopAppApi.Data;
using ShopAppApi.Request;
using ShopAppApi.ViewModels;

namespace ShopAppApi.Repositories.CartRepo
{
    public interface ICartRepository
    {
        public void AddToCart(AddCartRequest request);
        public Task<List<CartVM>> GetCart(long CustomerId);
    }
}