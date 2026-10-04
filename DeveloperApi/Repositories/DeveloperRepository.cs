using DeveloperApi.Data;
using DeveloperApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeveloperApi.Repositories;

public class DeveloperRepository : IDeveloperRepository
{
    private readonly AppDbContext _context;

    public DeveloperRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Developer>> GetAllAsync()
    {
        return await _context.Developers
            .AsNoTracking()
            .OrderBy(d => d.Id)
            .ToListAsync();
    }

    public async Task<Developer?> GetByIdAsync(int id)
    {
        return await _context.Developers
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<Developer> AddAsync(Developer developer)
    {
        _context.Developers.Add(developer);
        await _context.SaveChangesAsync();
        return developer;
    }

    public async Task<bool> UpdateAsync(Developer developer)
    {
        var existing = await _context.Developers.FirstOrDefaultAsync(d => d.Id == developer.Id);
        if (existing is null)
        {
            return false;
        }

        existing.FirstName = developer.FirstName;
        existing.LastName = developer.LastName;
        existing.Email = developer.Email;
        existing.Specialization = developer.Specialization;
        existing.Level = developer.Level;
        existing.Salary = developer.Salary;
        existing.HireDate = developer.HireDate;
        existing.IsActive = developer.IsActive;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _context.Developers.FirstOrDefaultAsync(d => d.Id == id);
        if (existing is null)
        {
            return false;
        }

        _context.Developers.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsByEmailAsync(string email, int? excludeId = null)
    {
        return await _context.Developers
            .AsNoTracking()
            .AnyAsync(d => d.Email == email && (excludeId == null || d.Id != excludeId));
    }
}
