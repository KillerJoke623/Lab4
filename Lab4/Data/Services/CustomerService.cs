using Lab4.Data.DTO;
using Lab4.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab4.Data.Services;

public class CustomerService
{
    private EducationContext _context;
    public CustomerService(EducationContext context)
    {
        _context = context;
    }
    
    public async Task<Customer> AddCustomer(CustomerDTO customer)
    {
        Customer nCustomer = new Customer()
        {
            FullName = customer.Fullname,
            Grade = customer.Grade,
        };
        var result = _context.Customers.Add(nCustomer);
        await _context.SaveChangesAsync();
        return await Task.FromResult(result.Entity);
    }

    public async Task<Customer> GetCustomer(int id)
    {
        var result = _context.Customers.FirstOrDefault(cu => cu.ID==id);
    
        return await Task.FromResult(result);
    }

    public async Task<List<Customer>> GetCustomers()
    {
        var result = await _context.Customers.ToListAsync();
        return await Task.FromResult(result);
        
    }

    public async Task<Customer?> UpdateCustomer(int id, Customer newCustomer)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(cu => cu.ID == id);;

        if (customer != null)
        {
            customer.FullName = newCustomer.FullName;
            customer.Grade = newCustomer.Grade;
            return customer;
        }
        return null;
    }

    public async Task<bool> DeleteCustomer(int id)
    {
        var customer = await _context.Customers.FirstOrDefaultAsync(se => se.ID == id);
        if (customer != null)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return true;
        }


        return false;
    }
}