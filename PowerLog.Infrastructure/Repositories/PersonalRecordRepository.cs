using Microsoft.EntityFrameworkCore;
using PowerLog.Core.Interfaces;
using PowerLog.Core.Models;

namespace PowerLog.Infrastructure.Repositories
{
    public class PersonalRecordRepository : IRepository<PersonalRecord>
    {
        private readonly PowerLogContext db;

        public PersonalRecordRepository(PowerLogContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<PersonalRecord>> GetAllAsync()
        {
            return await db.PersonalRecords.ToListAsync();
        }

        public async Task<PersonalRecord?> GetByIdAsync(Guid id)
        {
            return await db.PersonalRecords.FindAsync(id);
        }

        public async Task CreateAsync(PersonalRecord personalRecord)
        {
            db.PersonalRecords.Add(personalRecord);
            await db.SaveChangesAsync();
        }

        public async Task UpdateAsync(PersonalRecord personalRecord)
        {
            db.Entry(personalRecord).State = EntityState.Modified;
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            PersonalRecord? personalRecord = await db.PersonalRecords.FindAsync(id);
            if (personalRecord == null)
            {
                throw new Exception("Ошибка: рекорд не найден");
            }
            db.PersonalRecords.Remove(personalRecord);
            await db.SaveChangesAsync();
        }
    }
}
