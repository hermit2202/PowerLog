using PowerLog.Core.DTOs.Set;
using PowerLog.Core.DTOs.Workout;
using PowerLog.Core.DTOs.WorkoutExercise;
using PowerLog.Core.Interfaces;
using PowerLog.Core.Models;

namespace PowerLog.Core.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly IRepository<Workout> repository;

        public WorkoutService(IRepository<Workout> repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<WorkoutDto>> GetAllAsync(Guid userId)
        {
            var workouts = (await repository.GetAllAsync())
                .Where(u => u.UserId == userId);

            var dtos = workouts.Select(w => new WorkoutDto
            {
                WorkoutId = w.WorkoutId,
                UserId = w.UserId,
                Planned = w.Planned,
                Actual = w.Actual,
                Exercises = w.WorkoutExercises?.Select(e => new WorkoutExerciseDto
                {
                    ExerciseId = e.ExerciseId,
                    WorkoutExerciseId = e.WorkoutExerciseId,
                    WorkoutId = e.WorkoutId,
                    Order = e.Order,
                    Sets = e.Sets?.Select(s => new SetDto
                    {
                        SetId = s.SetId,
                        Weight = s.Weight,
                        Reps = s.Reps,
                        Rpe = s.Rpe,
                    }).ToList() ?? new List<SetDto>(),
                }).ToList() ?? new List<WorkoutExerciseDto>(),
            });

            return dtos;
        }

        public async Task<WorkoutDto> GetByIdAsync(Guid id)
        {
            var workout = await repository.GetByIdAsync(id);
            if (workout == null)
            {
                throw new Exception("Такая тренировка не найдена.");
            }

            var dto = new WorkoutDto
            {
                WorkoutId = workout.WorkoutId,
                UserId = workout.UserId,
                Planned = workout.Planned,
                Actual = workout.Actual,
                Exercises = workout.WorkoutExercises?.Select(e => new WorkoutExerciseDto
                {
                    ExerciseId = e.ExerciseId,
                    WorkoutExerciseId = e.WorkoutExerciseId,
                    WorkoutId = e.WorkoutId,
                    Order = e.Order,
                    Sets = e.Sets?.Select(s => new SetDto
                    {
                        SetId = s.SetId,
                        Weight = s.Weight,
                        Reps = s.Reps,
                        Rpe = s.Rpe,
                    }).ToList() ?? new List<SetDto>(),
                }).ToList() ?? new List<WorkoutExerciseDto>(),
            };

            return dto;
        }

        public async Task<WorkoutDto> CreateAsync(CreateWorkoutDto dto, Guid userId)
        {
            var workout = new Workout
            {
                UserId = userId,
                Planned = dto.Planned,
                Actual = dto.Actual,
            };

            await repository.CreateAsync(workout);

            var resultDto = new WorkoutDto
            {
                WorkoutId = workout.WorkoutId,
                UserId = workout.UserId,
                Planned = workout.Planned,
                Actual = workout.Actual,
                Exercises = new List<WorkoutExerciseDto>(),
            };

            return resultDto;
        }

        public async Task<WorkoutDto> UpdateAsync(UpdateWorkoutDto dto, Guid id)
        {
            var workout = await repository.GetByIdAsync(id);

            if (workout == null)
            {
                throw new Exception($"Такая тренировка не найдена.");
            }

            workout.Planned = dto.Planned;
            workout.Actual = dto.Actual;

            await repository.UpdateAsync(workout);
            return new WorkoutDto
            {
                WorkoutId = workout.WorkoutId,
                UserId = workout.UserId,
                Planned = workout.Planned,
                Actual = workout.Actual,
                Exercises = new List<WorkoutExerciseDto>(),
            };
        }
        public async Task<bool> DeleteAsync(Guid id)
        {
            var workout = await repository.GetByIdAsync(id);
            if (workout == null)
            {
                return false;
            }
            await repository.DeleteAsync(id);
            return true;
        }

    }
}
