using Microsoft.AspNetCore.Mvc;
using Lab4.Data.Models;
using Lab4.Data.Services;

namespace Lab4.Controllers;

[Route("api/[controller]")]
[ApiController]

public class CustomerController : ControllerBase
{
    private readonly CustomerService _contextCu;

    public CustomerController(CustomerService context)
    {
        _contextCu = context;
    }
    
    //Get customers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> GetCustomer()
    {
        return await _contextCu.GetCustomers();
    }
    
    //Get customer by ID
    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> GetCustomer(int id)
    {
        var customer = await _contextCu.GetCustomer(id);

        if (customer == null)
        {
            return NotFound();
        }

        return customer;
    }
    
    //Put change customer
    [HttpPut("{id}")]
    public async Task<ActionResult<Customer>> PutCustomer(int id, [FromBody] Customer customer)
    {
        var result = await _contextCu.UpdateCustomer(id, customer);
        if (result==null)
        {
            return BadRequest();
        }

        return Ok(result);
    }
    
    //Post add customer
    [HttpPost]
    public async Task<ActionResult<Customer>> PostCustomer([FromBody] Customer customer)
    {
        var result = await _contextCu.AddCustomer(customer);
        if (result==null)
        {
            BadRequest();
        }

        return Ok(result);
    }
    
    //Delete customer
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        var customer = await _contextCu.DeleteCustomer(id);
        if (customer)
        {
            return Ok();
        }

        return BadRequest();
    }
}