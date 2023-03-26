using EFC_.Contexts;
using EFC_.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EFC_.Services;

internal class CaseService
{
    private readonly DataContext _context = new();

    public async Task CreateAsync(CaseEntity cases)
    {
        await _context.AddAsync(cases);
        await _context.SaveChangesAsync();
    }
    public async Task<IEnumerable<CaseEntity>> GetAllAsync()
    {
        return await _context.Cases
            .Include(x => x.Customer)
            .ToListAsync();
    }


    public async Task<CaseEntity> GetAsync(Expression<Func<CaseEntity, bool>> predicate)
    {
        var cases = await _context.Cases
            .Include(x => x.Customer)
            .FirstOrDefaultAsync(predicate);

        return cases!;
    }

    public async Task UpdateStatusAsync(int caseId, string newStatus)
    {
        var _cases = await _context.Cases.FindAsync(caseId);
        if (_cases != null)
        {
            _cases.Status = newStatus;
            _context.Update(_cases);
            await _context.SaveChangesAsync();

        }
    }
}
