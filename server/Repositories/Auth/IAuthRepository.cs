using ShopAppApi.Data;
using ShopAppApi.Request;
using ShopAppApi.ViewModels;

namespace ShopAppApi.Repositories.Auth
{
    public interface IAuthRepository
    {
        Task<LoginInfoVM> Login(LoginRequest request);
        Task<LoginInfoVM> GetInfo(long customerId);
    }
}