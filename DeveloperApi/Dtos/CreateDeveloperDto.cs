using DeveloperApi.Enums;

namespace DeveloperApi.Dtos;

public class CreateDeveloperDto
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Specialization { get; set; } = string.Empty;
    public DeveloperLevel Level { get; set; }
    public decimal Salary { get; set; }
    public DateTime HireDate { get; set; }
}
