using ShopAppApi.Data;
using ShopAppApi.Request;
using ShopAppApi.Response;

namespace ShopAppApi.Repositories.RepoCustomer
{
    public interface ICustomerRepository
    {
        Task<PaginatedList<Customer>> GetAll(CustomerRequest request);
        Customer? Find(int id);
        Task Create(StoreCustomerRequest customer);
        Task Update(int id, UpdateCustomerRequest customer);
        Boolean Delete(int id);
    }
}
