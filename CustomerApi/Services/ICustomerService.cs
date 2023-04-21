using CustomerApi.Entities;
using CustomerApi.Helpers;

namespace CustomerApi.Services;

public interface ICustomerService
{
    Task<Customer> CreateCustomer(CustomerRequest customer);
    Task<IEnumerable<Customer>> FindAllCustomers();
    Task<IEnumerable<Customer>> FindCustomerByName(string name);
    Task<Customer?> FindCustomerById(int id);
    Task UpdateCustomer(int id, CustomerRequest customerUpdate);
    Task DeleteCustomer(int id);
}