using CustomerApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerApi.Repositories.Impl;

public class CustomerRepository : ICustomerRepository
{
    private AppDbContext _context;

    public CustomerRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Customer> CreateCustomer(Customer customer)
    {
        var valueTask = await _context.AddAsync(customer);
        return valueTask.Entity;
    }

    public async Task<IEnumerable<Customer>> FindAllCustomer()
    {
        return await _context.Customers.OrderBy(c => c.Id).ToListAsync();
    }

    public async Task<IEnumerable<Customer>> FindCustomerByName(string name)
    { 
        var customers =  _context.Customers.AsQueryable();
        return await customers.Where(customer => EF.Functions.Like(customer.FirstName.ToLower(), $"%{name.ToLower()}%")).ToListAsync();
    }

    public async Task<Customer?> FindCustomerById(int id)
    {
        return await _context.Customers.FindAsync(id);
    }
    public void UpdateCustomer(Customer customer)
    {
        _context.Customers.Update(customer);
    }

    public void DeleteCustomer(Customer customer)
    {
        _context.Remove(customer);
    }
}