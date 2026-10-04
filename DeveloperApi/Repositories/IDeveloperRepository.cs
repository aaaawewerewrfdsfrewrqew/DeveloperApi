using DeveloperApi.Entities;

namespace DeveloperApi.Repositories;

public interface IDeveloperRepository
{
    Task<List<Developer>> GetAllAsync();
    Task<Developer?> GetByIdAsync(int id);
    Task<Developer> AddAsync(Developer developer);
    Task<bool> UpdateAsync(Developer developer);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsByEmailAsync(string email, int? excludeId = null);
}
