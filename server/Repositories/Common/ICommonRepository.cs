using ShopAppApi.Data;
using ShopAppApi.Request;
using ShopAppApi.ViewModels;

namespace ShopAppApi.Repositories.Common
{
    public interface ICommonRepository
    {
        Task<List<PaymentMethod>> PaymentMethods();
        Task<List<ShippingUnit>> ShippingUnits();
        Task<List<Province>> Provinces();
        Task<List<Ward>> Wards(long provinceId);
        Task<string> GetServerName();
    }
}