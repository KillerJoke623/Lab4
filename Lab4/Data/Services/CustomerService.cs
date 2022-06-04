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
    //TODO create DTO of Customer to get rid of input id
    public async Task<Customer> AddCustomer(Customer customer)
    {
        /*
        DataSource.GetInstance()._customers.Add(customer);
        return await Task.FromResult(customer);*/
        Customer nCustomer = new Customer()
        {
            FullName = customer.FullName,
            Grade = customer.Grade,
        };
        var result = _context.Customers.Add(nCustomer);
        await _context.SaveChangesAsync();
        return await Task.FromResult(result.Entity);
    }

    public async Task<Customer> GetCustomer(int id)
    {
        /*
        var result = DataSource.GetInstance()._customers.Find(s => s.ID == id);
        return await Task.FromResult(result);*/
        var result = _context.Customers.FirstOrDefault(sel => sel.ID==id);
    
        return await Task.FromResult(result);
    }

    public async Task<List<Customer>> GetCustomers()
    {
        var result = await _context.Customers.ToListAsync();
        return await Task.FromResult(result);
        
    }

    public async Task<Customer?> UpdateCustomer(int id, Customer newCustomer)
    {
        var customer = DataSource.GetInstance()._customers.FirstOrDefault(se => se.ID == newCustomer.ID);

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