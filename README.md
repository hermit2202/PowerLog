# PowerLog
Дневник тренировок для пауэрлифтинга

## Основные возможности:
- Запись своих тренировок
- Фиксация рекордов
- Расчёт необходимой статистики (тонаж, прогресс и т. д.)

## Планируемое ближайшее расширение:
- Добавление дневника питания.
- Возможность получение тренировок на прямую от тренера.
- Доступ тренера к всем данным своего подопечного.
- Создание клиентской части (телеграмм бот / телеграмм апп).

graph TD
    subgraph Client
        A[Frontend / Telegram Bot / Mobile App]
    end

    subgraph "PowerLog.Api (Presentation)"
        B[Middleware: CORS, JWT Auth, Authorization]
        C[Controllers: Workouts, Users, Exercises, PersonalRecords]
    end

    subgraph "PowerLog.Core (Business Logic)"
        D[Services: IWorkoutService, IUserService, etc.]
        E[Domain Models: Workout, User, Exercise, Set]
        F[DTOs: CreateWorkoutDto, WorkoutDto, etc.]
        G[Contracts: IReader, IWriter, IUnitOfWork]
    end

    subgraph "PowerLog.Infrastructure (Data Access)"
        H[UnitOfWork]
        I[Специализированные репозитории: WorkoutRepository, SetRepository, etc.]
        J[BaseWriteRepository<T> / Reader]
        K[(SQL Server Database)]
        L[PowerLogContext (EF Core)]
    end

    A -->|HTTP Request + JWT| B
    B -->|Проверенный запрос| C
    C -->|1. Извлекает UserId из JWT Claims| C
    C -->|2. Вызывает метод сервиса + CancellationToken| D
    D -->|3. Бизнес-логика и валидация| E
    D -->|4. Запрашивает данные через контракты| G
    G -->|5. Делегирует Unit of Work| H
    H -->|6. Координирует транзакцию| I
    I -->|Наследует реализацию| J
    J -->|7. Генерирует SQL через EF Core| L
    L <-->|8. Чтение/Запись| K
    
    K -->|9. Возвращает Domain Models| I
    I -->|10. Возвращает результат| H
    H -->|11. Возвращает результат| D
    D -->|12. AutoMapper| F
    F -->|13. HTTP Response (JSON)| A

    classDef api fill:#e1f5fe,stroke:#01579b,stroke-width:2px;
    classDef core fill:#fff3e0,stroke:#e65100,stroke-width:2px;
    classDef infra fill:#e8f5e9,stroke:#1b5e20,stroke-width:2px;
    
    class B,C api;
    class D,E,F,G core;
    class H,I,J,K,L infra;
