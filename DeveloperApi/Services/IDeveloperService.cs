using DeveloperApi.Dtos;

namespace DeveloperApi.Services;

public interface IDeveloperService
{
    Task<List<DeveloperDto>> GetAllAsync();
    Task<DeveloperDto?> GetByIdAsync(int id);
    Task<DeveloperDto> CreateAsync(CreateDeveloperDto dto);
    Task<DeveloperDto?> UpdateAsync(int id, UpdateDeveloperDto dto);
    Task<bool> DeleteAsync(int id);
}
