using AutoMapper;
using DeveloperApi.Dtos;
using DeveloperApi.Entities;
using DeveloperApi.Exceptions;
using DeveloperApi.Repositories;

namespace DeveloperApi.Services;

public class DeveloperService : IDeveloperService
{
    private readonly IDeveloperRepository _repository;
    private readonly IMapper _mapper;

    public DeveloperService(IDeveloperRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<DeveloperDto>> GetAllAsync()
    {
        var developers = await _repository.GetAllAsync();
        return _mapper.Map<List<DeveloperDto>>(developers);
    }

    public async Task<DeveloperDto?> GetByIdAsync(int id)
    {
        var developer = await _repository.GetByIdAsync(id);
        return developer is null ? null : _mapper.Map<DeveloperDto>(developer);
    }

    public async Task<DeveloperDto> CreateAsync(CreateDeveloperDto dto)
    {
        if (await _repository.ExistsByEmailAsync(dto.Email))
        {
            throw new ConflictException($"Розробник з email '{dto.Email}' вже існує.");
        }

        var developer = _mapper.Map<Developer>(dto);
        developer.IsActive = true;

        var created = await _repository.AddAsync(developer);
        return _mapper.Map<DeveloperDto>(created);
    }

    public async Task<DeveloperDto?> UpdateAsync(int id, UpdateDeveloperDto dto)
    {
        var existing = await _repository.GetByIdAsync(id);
        if (existing is null)
        {
            return null;
        }

        if (await _repository.ExistsByEmailAsync(dto.Email, id))
        {
            throw new ConflictException($"Розробник з email '{dto.Email}' вже існує.");
        }

        var developer = _mapper.Map<Developer>(dto);
        developer.Id = id;

        var updated = await _repository.UpdateAsync(developer);
        return updated ? _mapper.Map<DeveloperDto>(developer) : null;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}
