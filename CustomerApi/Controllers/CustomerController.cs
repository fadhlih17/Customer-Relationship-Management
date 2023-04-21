using CustomerApi.Entities;
using CustomerApi.Helpers;
using CustomerApi.Services;
using CustomerApi.Services.Impl;
using Microsoft.AspNetCore.Mvc;

namespace CustomerApi.Controllers;

[ApiController]
[Route("/api/customers")]
public class CustomerController : ControllerBase
{
    private ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer([FromBody] CustomerRequest customerRequest)
    {
        var customer = await _customerService.CreateCustomer(customerRequest);
        return Created("/customers", customer);
    }

    [HttpGet]
    public async Task<IActionResult> FindAllCustomers([FromQuery] string? firstName)
    {
        var customers = await _customerService.FindAllCustomers();
        if (firstName != null)
        {
            customers = await _customerService.FindCustomerByName(firstName);
        }

        return Ok(customers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> FindCustomerById(int id)
    {
        var customer = await _customerService.FindCustomerById(id);
        return Ok(customer);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerRequest customer)
    {
        await _customerService.UpdateCustomer(id, customer);
        return Ok("Update Customer Successfully");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        await _customerService.DeleteCustomer(id);
        return Ok("Delete Customer Successfully");
    }

}