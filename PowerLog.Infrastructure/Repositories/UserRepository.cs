using Microsoft.EntityFrameworkCore;
using PowerLog.Core.Interfaces;
using PowerLog.Core.Models;

namespace PowerLog.Infrastructure.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly PowerLogContext db;

        public UserRepository(PowerLogContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await db.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await db.Users.FindAsync(id);
        }

        public async Task CreateAsync(User user)
        {
            db.Users.Add(user);
            await db.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            db.Entry(user).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            User? user = await db.Users.FindAsync(id);
            if (user == null)
            {
                throw new Exception("Ошибка: пользователь не найден");
            }
            db.Users.Remove(user);
            await db.SaveChangesAsync();
        }

    }
}
