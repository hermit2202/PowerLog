using Microsoft.EntityFrameworkCore;
using PowerLog.Core.Contracts.Common;
using PowerLog.Core.Models;

namespace PowerLog.Infrastructure.Persistence
{
    public class PowerLogContext : DbContext
    {
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly IIdentityProvider identityProvider;

        public PowerLogContext(
            DbContextOptions<PowerLogContext> options,
            IDateTimeProvider dateTimeProvider,
            IIdentityProvider identityProvider) : base(options)
        {
            this.identityProvider = identityProvider;
            this.dateTimeProvider = dateTimeProvider;
        }

        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<PersonalRecord> PersonalRecords { get; set; }
        public DbSet<Set> Sets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<WorkoutExercise> WorkoutExercises { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is IAuditableEntity &&
                            (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var entity = (IAuditableEntity)entry.Entity;

                var now = dateTimeProvider.UtcNow;
                var userId = identityProvider.CurrentUserId;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAt = now;
                    entity.CreatedBy = userId;
                }

                entity.UpdatedAt = now;
                entity.UpdatedBy = userId;
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
