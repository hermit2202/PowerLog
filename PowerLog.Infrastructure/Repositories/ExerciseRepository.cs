using Microsoft.EntityFrameworkCore;
using PowerLog.Core.Interfaces;
using PowerLog.Core.Models;

namespace PowerLog.Infrastructure.Repositories
{
    public class ExerciseRepository : IRepository<Exercise>
    {
        private readonly PowerLogContext db;

        public ExerciseRepository(PowerLogContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<Exercise>> GetAllAsync()
        {
            return await db.Exercises.ToListAsync();
        }

        public async Task<Exercise?> GetByIdAsync(Guid id)
        {
            return await db.Exercises.FindAsync(id);
        }

        public async Task CreateAsync(Exercise exercise)
        {
            db.Exercises.Add(exercise);
            await db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Exercise exercise)
        {
            db.Entry(exercise).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            Exercise? exercise = await db.Exercises.FindAsync(id);
            if (exercise == null)
            {
                throw new Exception("Ошибка: упражнение не найдено");
            }
            db.Exercises.Remove(exercise);
            await db.SaveChangesAsync();
        }
    }
}
