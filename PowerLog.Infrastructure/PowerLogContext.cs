using Microsoft.EntityFrameworkCore;
using PowerLog.Core.Models;

namespace PowerLog.Infrastructure
{
    public class PowerLogContext : DbContext
    {
        public PowerLogContext(DbContextOptions<PowerLogContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CoachClient>(entity =>
            {
                entity.HasOne(cc => cc.Coach)
                    .WithMany()
                    .HasForeignKey(cc => cc.CoachId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(cc => cc.Client)
                    .WithMany()
                    .HasForeignKey(cc => cc.ClientId)
                    .OnDelete(DeleteBehavior.NoAction);
            });
        }

        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<PersonalRecord> PersonalRecords { get; set; }
        public DbSet<Set> Sets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<WorkoutExercise> WorkoutExercises { get; set; }
        public DbSet<CoachClient> CoachClients { get; set; }
    }
}
