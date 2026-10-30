# PowerLog

REST API для дневника тренировок по пауэрлифтингу: тренировки, упражнения, подходы и личные рекорды.
Учебно-портфолио проект, разрабатываемый самостоятельно на **C# / ASP.NET Core**.

> **Статус:** в активной разработке. Ниже честно указано, что уже работает, а что ещё нет ([см. статус](#статус-проекта)).

## Что показывает этот проект

| Тема | Как реализовано |
|---|---|
| Многослойная архитектура | `Api` → `Core` ← `Infrastructure`, зависимости направлены внутрь, бизнес-логика не знает про EF Core |
| Работа с БД | EF Core 10 (Code First), SQL Server, миграции, аудит-поля (`CreatedAt/By`, `UpdatedAt/By`) заполняются автоматически в `SaveChangesAsync` |
| Паттерны доступа к данным | Repository + Unit of Work; чтение (`IReader`, `AsNoTracking`) отделено от записи (`IWriter`) |
| Аутентификация | JWT Bearer, пароли хешируются BCrypt |
| Тестируемость | Зависимости от времени и текущего пользователя вынесены в `IDateTimeProvider` и `IIdentityProvider` |
| Контракты API | DTO отделены от доменных моделей, маппинг через AutoMapper, валидация через Data Annotations, документация в Swagger |

## Стек

- .NET 10, ASP.NET Core Web API (контроллеры)
- Entity Framework Core 10 + SQL Server
- JWT Bearer (`Microsoft.AspNetCore.Authentication.JwtBearer`), BCrypt.Net-Next
- AutoMapper, Swashbuckle (Swagger UI)

## Архитектура

```
PowerLog.Api             Контроллеры, Program.cs (DI, JWT, CORS, Swagger), провайдеры времени и пользователя
PowerLog.Core            Модели, DTO, интерфейсы репозиториев и сервисов, сервисы с бизнес-логикой, профиль маппинга
PowerLog.Infrastructure  DbContext, репозитории, Reader/Writer/UnitOfWork, миграции EF Core
```

Поток запроса: `Controller → Service → Repository → DbContext`.

### Модель данных

```mermaid
erDiagram
    User ||--o{ Workout : "проводит"
    User ||--o{ PersonalRecord : "устанавливает"
    Exercise ||--o{ PersonalRecord : "относится к"
    Workout ||--o{ WorkoutExercise : "включает"
    Exercise ||--o{ WorkoutExercise : "выполняется как"
    WorkoutExercise ||--o{ Set : "состоит из"
```

Тип упражнения (`ExerciseType`): `Squat`, `BenchPress`, `DeadLift`, `Accessory`.

## Быстрый старт

**Требования:** .NET 10 SDK, SQL Server (LocalDB на Windows или контейнер), `dotnet-ef`.

```bash
git clone https://github.com/hermit2202/PowerLog.git
cd PowerLog

# 1. (опционально) своя строка подключения и JWT-ключ (не короче 32 символов)
export ConnectionStrings__DefaultConnection="Server=localhost,1433;Database=PowerLogDb;User Id=sa;Password=<пароль>;TrustServerCertificate=True"
export JwtSettings__SecretKey="<случайная строка не короче 32 символов>"

# 2. создать БД и применить миграции (нужно до первого запуска)
dotnet tool install --global dotnet-ef
dotnet ef database update --project PowerLog.Infrastructure --startup-project PowerLog.Api

# 3. запустить API
dotnet run --project PowerLog.Api
```

Swagger UI (в режиме Development): http://localhost:5152/swagger

SQL Server в контейнере, если нет LocalDB:

```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=<пароль>" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest
```

Без переменных окружения приложение использует значения из `appsettings.json` (LocalDB и ключ-заглушка только для локальной разработки).

### Пример использования

```bash
# регистрация
curl -X POST http://localhost:5152/api/users/register \
  -H "Content-Type: application/json" \
  -d '{"userName":"athlete","email":"athlete@example.com","password":"strongpass1"}'

# вход (в ответе поле jwtToken)
curl -X POST http://localhost:5152/api/users/login \
  -H "Content-Type: application/json" \
  -d '{"email":"athlete@example.com","password":"strongpass1"}'

# создание упражнения (type: 0 Squat, 1 BenchPress, 2 DeadLift, 3 Accessory)
curl -X POST http://localhost:5152/api/exercises \
  -H "Authorization: Bearer <jwtToken>" -H "Content-Type: application/json" \
  -d '{"name":"Приседания со штангой","type":0}'
```

## API

Все эндпоинты, кроме регистрации и входа, требуют заголовок `Authorization: Bearer <token>`.

| Ресурс | Методы |
|---|---|
| `/api/users` | `POST /register`, `POST /login`, `GET /{id}` |
| `/api/exercises` | `GET`, `GET /{id}`, `POST`, `PUT /{id}`, `DELETE /{id}` |
| `/api/workouts` | `GET`, `GET /{id}`, `POST`, `PUT /{id}`, `DELETE /{id}` |
| `/api/personalrecords` | `GET ?exerciseId=`, `GET /{id}`, `POST`, `PUT /{id}`, `DELETE /{id}` |

## Статус проекта

**Реализовано**
- Регистрация, вход, JWT-аутентификация
- CRUD для упражнений, тренировок и личных рекордов
- Миграции БД, автоматический аудит изменений

**В работе**
- API для упражнений внутри тренировки и подходов (сущности и репозитории готовы, сервисов и эндпоинтов пока нет)
- Проверка владельца данных на всех эндпоинтах
- Единая обработка ошибок и валидация

**Пока не реализовано**
- Тесты, Docker-образ, CI
- Расчёт статистики (тоннаж, прогресс)

## Планы

1. Подходы и упражнения в тренировке, статистика: тоннаж, оценка 1ПМ, прогресс по неделям
2. Тесты (xUnit, интеграционные на `WebApplicationFactory`), Docker Compose, GitHub Actions
3. Роли «спортсмен / тренер»: тренер назначает тренировки и видит данные подопечного
4. Дневник питания
5. Клиент: Telegram-бот / Mini App

## Автор

Студент 3 курса ПКГХ (Санкт-Петербург), специальность «Информационные системы и программирование».
GitHub: [hermit2202](https://github.com/hermit2202)
