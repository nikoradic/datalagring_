using EFC_.Contexts;
using EFC_.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EFC_.Services;

internal class CustomerService
{
    private readonly DataContext _context = new();


    public async Task CreateAsync(CustomerEntity customer)
    {
        var _entity = await _context.Customers.FirstOrDefaultAsync(x => x.Email == customer.Email);
        if (_entity == null)
        {
            await _context.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

    }
    public async Task<CustomerEntity> GetAsync(Expression<Func<CustomerEntity, bool>> predicate)
    {

        var _entity = await _context.Customers.FirstOrDefaultAsync(predicate);
        return _entity!;
    }


    public async Task<IEnumerable<CustomerEntity>> GetAllAsync()
    {
        return await _context.Customers.ToListAsync();
    }

}

