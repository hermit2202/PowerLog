using Microsoft.EntityFrameworkCore;
using PowerLog.Core.Interfaces;
using PowerLog.Core.Models;

namespace PowerLog.Infrastructure.Repositories
{
    public class WorkoutExerciseRepository : IRepository<WorkoutExercise>
    {
        private readonly PowerLogContext db;

        public WorkoutExerciseRepository(PowerLogContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<WorkoutExercise>> GetAllAsync()
        {
            var workoutExercises = await db.WorkoutExercises
                .Include(we => we.Exercise)
                .Include(we => we.Sets)
                .ToListAsync();
            return workoutExercises;
        }

        public async Task<WorkoutExercise?> GetByIdAsync(Guid id)
        {
            var workoutExercise = await db.WorkoutExercises
                .Include(we => we.Exercise)
                .Include(we => we.Sets)
                .FirstOrDefaultAsync(we => we.WorkoutExerciseId == id);
            return workoutExercise;
        }

        public async Task CreateAsync(WorkoutExercise workoutExercise)
        {
            db.WorkoutExercises.Add(workoutExercise);
            await db.SaveChangesAsync();
        }

        public async Task UpdateAsync(WorkoutExercise workoutExercise)
        {
            db.Entry(workoutExercise).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var workoutExercise = await db.WorkoutExercises
                .Include(we => we.Sets)
                .FirstOrDefaultAsync(we => we.WorkoutExerciseId == id);
            if (workoutExercise == null)
            {
                throw new Exception("Ошибка: упражнение тренировки не найдено");
            }

            if (workoutExercise.Sets != null)
            {
                db.Sets.RemoveRange(workoutExercise.Sets);
            }

            db.WorkoutExercises.Remove(workoutExercise);
            await db.SaveChangesAsync();
        }
    }
}
