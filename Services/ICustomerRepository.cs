using Contoso.API.Persistence.Domain;

namespace Contoso.API.Services
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetCustomersAsync();
        Task<Customer?> GetCustomerAsync(int customerId, bool includeAddress);
        Task<bool> HasCustomersAsync(int customerId);
        void AddCustomer(Customer customer);
        Task<IEnumerable<Address>> GetAddressesForCustomerAsync(int customerId);
        Task<Address?> GetAddressForCustomerAsync(int customerId, int addressId);
        Task AddAddressForCustomerAsync(int customerId, Address address);
        void DeleteAddressForCustomer(Address address);
        void DeleteCustomer(Customer customer);
        Task<bool> SaveChangesAsync();
    }
}
