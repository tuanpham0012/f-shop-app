using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ShopAppApi.Data;
using ShopAppApi.Helpers;
using ShopAppApi.Helpers.Interfaces;
using ShopAppApi.Request;
using ShopAppApi.Response;
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

        public async Task<PaginatedList<Customer>> GetAll(CustomerRequest request)
        {
            IQueryable<Customer> query = _context.Customers.Select(customer => new Customer
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address,
                Status = customer.Status
            }).AsQueryable().OrderByDescending(q => q.Id);

            if (!String.IsNullOrEmpty(request.Search))
            {
                query = query.Where(q => q.Name.Contains(request.Search) || q.Email.Contains(request.Search) || q.Phone.Contains(request.Search));
            }

            if (request.Status != null)
            {
                query = query.Where(q => q.Status == request.Status);
            }



            var data = await PaginatedList<Customer>.CreateAsync(
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
    }
}
