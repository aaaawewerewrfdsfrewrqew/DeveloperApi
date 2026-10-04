# DeveloperApi

ASP.NET Core Web API (.NET 8) для сутності **Developer** з CRUD-операціями.

## Архітектура

```
Controller → Service → Repository → DbContext (EF Core)
```

- **Controllers/DevelopersController.cs** — HTTP endpoints, виклик валідаторів, обробка помилок.
- **Services/DeveloperService.cs** — бізнес-логіка (перевірка унікальності email тощо).
- **Repositories/DeveloperRepository.cs** — доступ до даних через EF Core.
- **Data/AppDbContext.cs** — DbContext.
- **Data/DbSeeder.cs** — наповнення БД тестовими даними при старті.
- **Entities/Developer.cs** — доменна сутність.
- **Dtos/** — `DeveloperDto` (вихідний), `CreateDeveloperDto`, `UpdateDeveloperDto`.
- **Mapping/MappingProfile.cs** — AutoMapper-профіль Entity ↔ DTO.
- **Validators/** — FluentValidation-валідатори для Create/Update.

## Endpoints

| Метод  | Маршрут                  | Опис                          |
|--------|---------------------------|-------------------------------|
| GET    | /api/developers            | Отримати всіх розробників     |
| GET    | /api/developers/{id}       | Отримати розробника за Id     |
| POST   | /api/developers             | Створити розробника           |
| PUT    | /api/developers/{id}       | Оновити розробника            |
| DELETE | /api/developers/{id}       | Видалити розробника           |

## Запуск у Visual Studio

1. Відкрийте `DeveloperApi.sln` у Visual Studio 2022 (17.8+), потрібен встановлений **.NET 8 SDK**.
2. Visual Studio автоматично відновить NuGet-пакети при відкритті (або ПКМ на солюшені → **Restore NuGet Packages**).
3. Натисніть **F5** (профіль `DeveloperApi`) — відкриється Swagger UI за адресою на кшталт `https://localhost:7050/swagger`.

За замовчуванням у `appsettings.json` увімкнена **In-Memory база даних** (`"UseInMemoryDatabase": true`), тож проєкт запускається без встановлення SQL Server. База при старті наповнюється 4 тестовими розробниками через `DbSeeder`.

### Перехід на реальну БД (SQL Server LocalDB)

1. У `appsettings.json` встановіть `"UseInMemoryDatabase": false`.
2. За потреби скоригуйте `ConnectionStrings:DefaultConnection`.
3. Виконайте в Package Manager Console:
   ```
   Add-Migration InitialCreate
   Update-Database
   ```

## Валідація

Валідація виконується через **FluentValidation** (`CreateDeveloperDtoValidator`, `UpdateDeveloperDtoValidator`), які викликаються в контролері перед зверненням до сервісу. При помилках повертається `400 Bad Request` у форматі `ValidationProblemDetails`. При спробі створити/оновити розробника з email, який вже зайнятий іншим записом, повертається `409 Conflict`.

## Пакети NuGet

- `Microsoft.EntityFrameworkCore.InMemory`, `Microsoft.EntityFrameworkCore.SqlServer`, `Microsoft.EntityFrameworkCore.Design`
- `AutoMapper.Extensions.Microsoft.DependencyInjection`
- `FluentValidation`
- `Swashbuckle.AspNetCore`
