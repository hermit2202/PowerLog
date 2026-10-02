using Microsoft.EntityFrameworkCore;
using PowerLog.Core.Interfaces;
using PowerLog.Core.Models;

namespace PowerLog.Infrastructure.Repositories
{
    public class SetRepository : IRepository<Set>
    {
        private readonly PowerLogContext db;

        public SetRepository(PowerLogContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<Set>> GetAllAsync()
        {
            var sets = await db.Sets
                .Include(s => s.WorkoutExercise)
                .ToListAsync();
            return sets;
        }

        public async Task<Set?> GetByIdAsync(Guid id)
        {
            var set = await db.Sets
                .Include(s => s.WorkoutExercise)
                .FirstOrDefaultAsync(s => s.SetId == id);
            return set;
        }

        public async Task CreateAsync(Set set)
        {
            db.Sets.Add(set);
            await db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Set set)
        {
            db.Entry(set).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var set = await db.Sets.FirstOrDefaultAsync(s => s.SetId == id);
            if (set == null)
            {
                throw new Exception("Ошибка: подход не найден");
            }
            db.Sets.Remove(set);
            await db.SaveChangesAsync();
        }
    }
}
