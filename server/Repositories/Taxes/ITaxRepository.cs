using ShopAppApi.Data;
using ShopAppApi.Request;

namespace ShopAppApi.Repositories.Products
{
    public interface ITaxRepository
    {
        Task<List<Tax>> GetAll(TaxRequest request);

        Task<Tax> Create(StoreTaxRequest tax);
    }
}
