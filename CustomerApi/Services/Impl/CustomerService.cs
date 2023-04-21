using CustomerApi.Entities;
using CustomerApi.Exceptions;
using CustomerApi.Helpers;
using CustomerApi.Repositories;

namespace CustomerApi.Services.Impl;

public class CustomerService : ICustomerService
{
    private IPersistence _persistence;
    private ICustomerRepository _repository;
    private ILogger<CustomerService> _logger;

    public CustomerService(IPersistence persistence, ICustomerRepository repository, ILogger<CustomerService> logger)
    {
        _persistence = persistence;
        _repository = repository;
        _logger = logger;
    }

    public async Task<Customer> CreateCustomer(CustomerRequest customerReq)
    {
        _logger.LogInformation("Create Customer");
        var customer = new Customer
        {
            FirstName = customerReq.FirstName,
            LastName = customerReq.LastName,
            Email = customerReq.Email,
            HomeAddress = customerReq.HomeAddress
        };
        var customerSave = new Customer();
        try
        {
            customerSave = await _repository.CreateCustomer(customer);
            await _persistence.SaveChangesAsync();
            _logger.LogInformation("Create Customer Successfully");
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

        return customerSave;
    }

    public async Task<IEnumerable<Customer>> FindAllCustomers()
    {
        _logger.LogInformation("Find All Customer");
        IEnumerable<Customer> customers = new List<Customer>();
        try
        {
            customers = await _repository.FindAllCustomer();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        return customers;
    }

    public async Task<IEnumerable<Customer>> FindCustomerByName(string name)
    {
        _logger.LogInformation("Find Customer with first name");
        IEnumerable<Customer> customers = new List<Customer>();
        try
        {
            customers = await _repository.FindCustomerByName(name);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        return customers;
    }

    public async Task<Customer?> FindCustomerById(int id)
    {
        _logger.LogInformation("Find Customer By Id");
        Customer? customer = await _repository.FindCustomerById(id);
        if (customer is null) throw new NotFoundException("Customer Not Found");
        _logger.LogInformation("Find Customer Successfully");
        return customer;
    }

    public async Task UpdateCustomer(int id, CustomerRequest customerUpdate)
    {
        _logger.LogInformation("Update Customer");
        var findCustomerById = await FindCustomerById(id);
        try
        {
            findCustomerById.Email = customerUpdate.Email;
            findCustomerById.HomeAddress = customerUpdate.HomeAddress;
            findCustomerById.FirstName = customerUpdate.FirstName;
            findCustomerById.LastName = customerUpdate.LastName;
            _repository.UpdateCustomer(findCustomerById);
            await _persistence.SaveChangesAsync();
            _logger.LogInformation("Update Customer Successfully");
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task DeleteCustomer(int id)
    {
        _logger.LogInformation("Delete Customer");
        var findCustomerById = await FindCustomerById(id);
        _repository.DeleteCustomer(findCustomerById);
        await _persistence.SaveChangesAsync();
        _logger.LogInformation("Delete Customer Successfully");
    }
}