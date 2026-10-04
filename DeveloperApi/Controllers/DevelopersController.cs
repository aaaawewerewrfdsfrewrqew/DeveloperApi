using DeveloperApi.Dtos;
using DeveloperApi.Exceptions;
using DeveloperApi.Services;
using DeveloperApi.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class DevelopersController : ControllerBase
{
    private readonly IDeveloperService _service;
    private readonly IValidator<CreateDeveloperDto> _createValidator;
    private readonly IValidator<UpdateDeveloperDto> _updateValidator;

    public DevelopersController(
        IDeveloperService service,
        IValidator<CreateDeveloperDto> createValidator,
        IValidator<UpdateDeveloperDto> updateValidator)
    {
        _service = service;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    /// <summary>Отримати список усіх розробників.</summary>
    [HttpGet]
    [ProducesResponseType(typeof(List<DeveloperDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<DeveloperDto>>> GetAll()
    {
        var developers = await _service.GetAllAsync();
        return Ok(developers);
    }

    /// <summary>Отримати розробника за Id.</summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(DeveloperDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DeveloperDto>> GetById(int id)
    {
        var developer = await _service.GetByIdAsync(id);
        if (developer is null)
        {
            return NotFound(new { message = $"Розробника з Id={id} не знайдено." });
        }

        return Ok(developer);
    }

    /// <summary>Створити нового розробника.</summary>
    [HttpPost]
    [ProducesResponseType(typeof(DeveloperDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<DeveloperDto>> Create([FromBody] CreateDeveloperDto dto)
    {
        var validationResult = await _createValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState);
            return ValidationProblem(ModelState);
        }

        try
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (ConflictException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    /// <summary>Оновити дані розробника за Id.</summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(DeveloperDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<DeveloperDto>> Update(int id, [FromBody] UpdateDeveloperDto dto)
    {
        var validationResult = await _updateValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState);
            return ValidationProblem(ModelState);
        }

        try
        {
            var updated = await _service.UpdateAsync(id, dto);
            if (updated is null)
            {
                return NotFound(new { message = $"Розробника з Id={id} не знайдено." });
            }

            return Ok(updated);
        }
        catch (ConflictException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    /// <summary>Видалити розробника за Id.</summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted)
        {
            return NotFound(new { message = $"Розробника з Id={id} не знайдено." });
        }

        return NoContent();
    }
}
