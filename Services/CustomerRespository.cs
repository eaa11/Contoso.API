using Contoso.API.Persistence.DbContexts;
using Contoso.API.Persistence.Domain;
using Microsoft.EntityFrameworkCore;

namespace Contoso.API.Services
{
    public class CustomerRespository : ICustomerRepository
    {
        protected readonly ContosoDbContext _context;

        public CustomerRespository(ContosoDbContext context)
        {
            _context = context;
        }

        public async Task<Address?> GetAddressForCustomerAsync(int customerId, int addressId)
        {
            return await _context.Addresses
                .Where(c => c.CustomerId == customerId && c.Id == addressId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Address>> GetAddressesForCustomerAsync(int customerId)
        {
            return await _context.Addresses
               .Where(c => c.CustomerId == customerId)
               .ToListAsync();
        }

        public async Task<Customer?> GetCustomerAsync(int customerId, bool includeAddress)
        {
            if (includeAddress)
            {
                return await _context.Customers
                    .Include(a => a.Addresses)
                    .Where(a => a.Id == customerId)
                    .FirstOrDefaultAsync();
            }
            return await _context.Customers.Where(c => c.Id == customerId)
                    .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync()
        {
            return await _context.Customers.OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<bool> HasCustomersAsync(int customerId)
        {
            return await _context.Customers.AnyAsync(c => c.Id == customerId);
        }

        public void AddCustomer(Customer customer)
        {
            _context.Add(customer);
        }

        public async Task AddAddressForCustomerAsync(int customerId, Address address)
        {
            var customer = await GetCustomerAsync(customerId, false);

            if (customer != null)
            {
                customer.Addresses.Add(address);
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public void DeleteAddressForCustomer(Address address)
        {
            _context.Addresses.Remove(address);
        }

        public void DeleteCustomer(Customer customer)
        {
            _context.Customers.Remove(customer);
        }
    }
}