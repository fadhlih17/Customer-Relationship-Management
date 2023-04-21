using CustomerApi.Entities;

namespace CustomerApi.Repositories;

public interface ICustomerRepository
{
    Task<Customer> CreateCustomer(Customer customer);
    Task<IEnumerable<Customer>> FindAllCustomer();
    Task<IEnumerable<Customer>> FindCustomerByName(String name);
    Task<Customer?> FindCustomerById(int id);    
    void UpdateCustomer(Customer customer);
    void DeleteCustomer(Customer customer);
}