using Microsoft.EntityFrameworkCore;
using PowerLog.Core.Interfaces;
using PowerLog.Core.Models;

namespace PowerLog.Infrastructure
{
    public class WorkoutRepository : IRepository<Workout>
    {
        private readonly PowerLogContext db;
        public WorkoutRepository(PowerLogContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<Workout>> GetAllAsync()
        {
            return await db.Workouts.ToListAsync();
        }

        public async Task<Workout?> GetByIdAsync(Guid id)
        {
            return await db.Workouts.FindAsync(id);
        }

        public async Task CreateAsync(Workout workout)
        {
            db.Workouts.Add(workout);
            await db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Workout workout)
        {
            db.Entry(workout).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            Workout workout = await db.Workouts.FindAsync(id);
            if (workout == null)
            {
                throw new Exception("Ошибка: тренировка не найдена");
            }
            db.Workouts.Remove(workout);
            await db.SaveChangesAsync();
        }
    }
}
