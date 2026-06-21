using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using PowerLog.Core.Interfaces;
using PowerLog.Core.Models;
using PowerLog.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PowerLogContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IRepository<Workout>, WorkoutRepository>();
builder.Services.AddScoped<IRepository<User>, UserRepository>();
builder.Services.AddScoped<IRepository<Exercise>, ExerciseRepository>();
builder.Services.AddScoped<IRepository<PersonalRecord>, PersonalRecordRepository>();
builder.Services.AddScoped<IRepository<WorkoutExercise>, WorkoutExerciseRepository>();
builder.Services.AddScoped<IRepository<Set>, SetRepository>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PowerLog API",
        Version = "v1"
    });
});

var app = builder.Build();

// Seed данных
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<PowerLogContext>();
    context.Database.EnsureCreated();

    if (!context.Users.Any())
    {
        var testUserId = Guid.Parse("00000000-0000-0000-0000-000000000001");
        context.Users.Add(new User
        {
            UserId = testUserId,
            UserName = "TestUser"
        });
        context.SaveChanges();
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
