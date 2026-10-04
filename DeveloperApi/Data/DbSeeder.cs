using DeveloperApi.Entities;
using DeveloperApi.Enums;

namespace DeveloperApi.Data;

public static class DbSeeder
{
    public static void Seed(AppDbContext context)
    {
        context.Database.EnsureCreated();

        if (context.Developers.Any())
        {
            return;
        }

        var developers = new List<Developer>
        {
            new()
            {
                FirstName = "Іван",
                LastName = "Петренко",
                Email = "ivan.petrenko@example.com",
                Specialization = "Backend",
                Level = DeveloperLevel.Senior,
                Salary = 55000,
                HireDate = new DateTime(2021, 3, 15),
                IsActive = true
            },
            new()
            {
                FirstName = "Олена",
                LastName = "Коваль",
                Email = "olena.koval@example.com",
                Specialization = "Frontend",
                Level = DeveloperLevel.Middle,
                Salary = 38000,
                HireDate = new DateTime(2022, 6, 1),
                IsActive = true
            },
            new()
            {
                FirstName = "Максим",
                LastName = "Бондаренко",
                Email = "maksym.bondarenko@example.com",
                Specialization = "FullStack",
                Level = DeveloperLevel.Junior,
                Salary = 22000,
                HireDate = new DateTime(2023, 9, 10),
                IsActive = true
            },
            new()
            {
                FirstName = "Софія",
                LastName = "Мельник",
                Email = "sofia.melnyk@example.com",
                Specialization = "Mobile",
                Level = DeveloperLevel.Lead,
                Salary = 68000,
                HireDate = new DateTime(2019, 1, 20),
                IsActive = false
            }
        };

        context.Developers.AddRange(developers);
        context.SaveChanges();
    }
}
