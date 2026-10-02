using Microsoft.EntityFrameworkCore;
using PowerLog.Core.Interfaces;
using PowerLog.Core.Models;

namespace PowerLog.Infrastructure.Repositories
{
    public class CoachClientRepository : IRepository<CoachClient>
    {
        private readonly PowerLogContext db;

        public CoachClientRepository(PowerLogContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<CoachClient>> GetAllAsync()
        {
            return await db.CoachClients.ToListAsync();
        }

        public async Task<CoachClient?> GetByIdAsync(Guid id)
        {
            return await db.CoachClients.FindAsync(id);
        }

        public async Task CreateAsync(CoachClient entity)
        {
            db.CoachClients.Add(entity);
            await db.SaveChangesAsync();
        }

        public async Task UpdateAsync(CoachClient entity)
        {
            db.Entry(entity).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            CoachClient? coachClient = await db.CoachClients.FindAsync(id);

            if (coachClient == null)
            {
                throw new Exception("Запись не найдена");
            }

            db.CoachClients.Remove(coachClient);
            await db.SaveChangesAsync();
        }
    }

}
