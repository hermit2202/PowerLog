using Microsoft.EntityFrameworkCore;
using PowerLog.Core.Models;

namespace PowerLog.Infrastructure
{
    public class PowerLogContext : DbContext
    {
        public PowerLogContext(DbContextOptions<PowerLogContext> options) : base(options)
        {

        }

        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<PersonalRecord> PersonalRecords { get; set; }
        public DbSet<Set> Sets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<WorkoutExercise> WorkoutExercises { get; set; }
    }
}
