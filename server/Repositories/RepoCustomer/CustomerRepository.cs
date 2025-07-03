using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShopAppApi.Data;
using ShopAppApi.Helpers;
using ShopAppApi.Helpers.Interfaces;
using ShopAppApi.Request;
using ShopAppApi.Response;
using ShopAppApi.ViewModels;
using System.Net;
using System.Numerics;

namespace ShopAppApi.Repositories.RepoCustomer
{
    public class CustomerRepository(ShopAppContext context, IStringHelper stringHelper) : ICustomerRepository
    {
        private readonly ShopAppContext _context = context;
        public Boolean Delete(int id)
        {
            var entry = _context.Customers.FirstOrDefault(cus => cus.Id == id);

            if (entry != null)
            {
                _context.Remove(entry);
                _context.SaveChanges();
                return true;
            }
            return false;

        }

        public async Task<PaginatedList<CustomerVM>> GetAll(CustomerRequest request)
        {
            IQueryable<CustomerVM> query = _context.Customers.Select(customer => new CustomerVM
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address,
                Status = customer.Status,
                DisplayName = $"{customer.Name} - {customer.Phone}",
            }).AsQueryable().OrderByDescending(q => q.Id);

            if (!string.IsNullOrEmpty(request.Search))
            {
                query = query.Where(q => q.Name.Contains(request.Search) || q.Email.Contains(request.Search) || q.Phone.Contains(request.Search));
            }

            if (request.Status != null)
            {
                query = query.Where(q => q.Status == request.Status);
            }



            var data = await PaginatedList<CustomerVM>.CreateAsync(
                query.AsNoTracking(), request.Page, request.PageSize);

            //if (!request.search.IsNullOrEmpty())

            return data;
        }

        public Customer? Find(int id)
        {
            var entry = _context.Customers.FirstOrDefault(cus => cus.Id == id);
            if (entry != null)
            {
                return new Customer
                {
                    Id = entry.Id,
                    Name = entry.Name,
                    Email = entry.Email,
                    Phone = entry.Phone,
                    Address = entry.Address,
                    Status = entry.Status
                };
            }
            return null;
        }

        public async Task Create(StoreCustomerRequest customer)
        {
            using var transaction = _context.Database.BeginTransaction();
            HashSalt hash = stringHelper.EncryptPassword(customer.Password);
            var entry = new Customer
            {
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone ?? "",
                Address = customer.Address ?? "",
                Password = hash.Hash,
                Salt = hash.Salt,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Status = customer.Status,
                Gender = customer.Gender

            };
            _context.Add(entry);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }

        public async Task Update(int id, UpdateCustomerRequest customer)
        {
            using var transaction = _context.Database.BeginTransaction();
            var _customer = _context.Customers.FirstOrDefault(cus => cus.Id == id);
            if (_customer != null)
            {
                _customer.Name = customer.Name;
                _customer.Email = customer.Email;
                _customer.Phone = customer.Phone ?? "";
                _customer.Address = customer.Address ?? "";
                _customer.Status = customer.Status;
                _customer.Gender = customer.Gender;
                _customer.UpdatedAt = DateTime.UtcNow;
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }

        }

        public async Task<DeliveryAddress> CreateDelivery(DeliveryRequest request)
        {
            if (request.Default == true)
            {
                // Set all other addresses to not default
                var existingDefault = _context.DeliveryAddresses
                    .Where(d => d.CustomerId == request.CustomerId && d.Default == true)
                    .ToList();

                foreach (var address in existingDefault)
                {
                    address.Default = false;
                }
                await _context.SaveChangesAsync();
            }
            var deliveryAddress = new DeliveryAddress
            {
                FullName = request.FullName,
                Address = request.Address,
                Phone = request.Phone,
                ProvinceId = request.ProvinceId,
                WardId = request.WardId,
                Default = request.Default,
                CustomerId = request.CustomerId ?? 0
            };
            _context.DeliveryAddresses.Add(deliveryAddress);
            await _context.SaveChangesAsync();
            return deliveryAddress;
        }

        public async Task<List<DeliveryAddressVM>> GetDelivery(long customerId)
        {
            return await _context.DeliveryAddresses
                .Include(d => d.Province)
                .Include(d => d.Ward)
                .Where(d => d.CustomerId == customerId)
                .Select(d => new DeliveryAddressVM
                {
                    Id = d.Id,
                    CustomerId = d.CustomerId,
                    FullName = d.FullName,
                    Address = d.Address,
                    Phone = d.Phone,
                    ProvinceId = d.ProvinceId,
                    WardId = d.WardId,
                    Lat = d.Lat,
                    Lng = d.Lng,
                    Default = d.Default,
                    DetailAddress = $"{d.Address},{(d.Province != null ? d.Province.Name ?? "" : "")},{(d.Ward != null ? d.Ward.Name ?? "" : "")}, ",
                })
                .ToListAsync();
        }

        public async Task<DeliveryAddress> UpdateDelivery(long customerId, DeliveryRequest request)
        {
            using var transaction = _context.Database.BeginTransaction();
            var deliveryAddress = await _context.DeliveryAddresses.FirstOrDefaultAsync(d => d.Id == request.CustomerId && d.CustomerId == customerId);
            if (deliveryAddress != null)
            {
                deliveryAddress.FullName = request.FullName;
                deliveryAddress.Address = request.Address;
                deliveryAddress.Phone = request.Phone;
                deliveryAddress.ProvinceId = request.ProvinceId;
                deliveryAddress.WardId = request.WardId;
                deliveryAddress.Default = request.Default;

                if (request.Default == true)
                {
                    // Set all other addresses to not default
                    var existingDefault = _context.DeliveryAddresses
                        .Where(d => d.CustomerId == customerId && d.Default == true && d.Id != deliveryAddress.Id)
                        .ToList();

                    foreach (var address in existingDefault)
                    {
                        address.Default = false;
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            return deliveryAddress;
        }

    }
}
