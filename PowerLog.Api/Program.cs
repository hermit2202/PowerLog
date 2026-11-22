using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PowerLog.Api.Handlers;
using PowerLog.Api.Providers;
using PowerLog.Core.Contracts.Common;
using PowerLog.Core.Contracts.IData;
using PowerLog.Core.Contracts.IServices;
using PowerLog.Core.Models;
using PowerLog.Core.Services;
using PowerLog.Infrastructure;
using PowerLog.Infrastructure.Persistence;
using PowerLog.Core.Services.AutoMapper;
using PowerLog.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PowerLogContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        .UseSnakeCaseNamingConvention());

builder.Services.AddScoped<IReader, Reader>();
builder.Services.AddScoped<IWriter, Writer>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IDateTimeProvider, DateTimeProvider>();
builder.Services.AddScoped<IIdentityProvider, IdentityProvider>();

builder.Services.AddScoped<IWorkoutRepository, WorkoutRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IExerciseRepository, ExerciseRepository>();
builder.Services.AddScoped<IPersonalRecordRepository, PersonalRecordRepository>();
builder.Services.AddScoped<IWorkoutExerciseRepository, WorkoutExerciseRepository>();
builder.Services.AddScoped<ISetRepository, SetRepository>();

builder.Services.AddScoped<IWorkoutService, WorkoutService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IExerciseService, ExerciseService>();
builder.Services.AddScoped<IPersonalRecordService, PersonalRecordService>();
builder.Services.AddScoped<AuthService>();

builder.Services.AddAutoMapper(cfg => cfg.AddMaps(typeof(MappingProfile)));

builder.Services.AddControllers();

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var securityKey = builder.Configuration["JwtSettings:SecretKey"]
            ?? throw new InvalidOperationException("JwtSettings:SecretKey not configured");
        var key = Encoding.UTF8.GetBytes(securityKey);

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "PowerLog API",
        Version = "v1"
    });
});

var app = builder.Build();

app.UseExceptionHandler();

app.UseCors("AllowAll");

if (app.Environment.IsDevelopment())
{
    // Seed данных
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<PowerLogContext>();

        if (!context.Users.Any())
        {
            var testUserId = Guid.Parse("00000000-0000-0000-0000-000000000001");
            context.Users.Add(new User
            {
                UserId = testUserId,
                UserName = "TestUser",
                Email = "test@example.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"),
            });
            context.SaveChanges();
        }
    }

    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
