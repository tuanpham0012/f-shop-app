using ShopAppApi.Data;
using ShopAppApi.Request;
using ShopAppApi.Response;
using ShopAppApi.ViewModels;

namespace ShopAppApi.Repositories.RepoCustomer
{
    public interface ICustomerRepository
    {
        Task<PaginatedList<CustomerVM>> GetAll(CustomerRequest request);
        Customer? Find(int id);
        Task Create(StoreCustomerRequest customer);
        Task Update(int id, UpdateCustomerRequest customer);
        Boolean Delete(int id);

        Task<DeliveryAddress> CreateDelivery(DeliveryRequest request);
        Task<List<DeliveryAddressVM>> GetDelivery(long customerId);
        Task<DeliveryAddress> UpdateDelivery(long customerId, DeliveryRequest request);
    }
}
