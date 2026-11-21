using AutoMapper;
using PowerLog.Core.Contracts.IData;
using PowerLog.Core.Contracts.IServices;
using PowerLog.Core.DTOs.Workout;
using PowerLog.Core.Exceptions;
using PowerLog.Core.Extensions;
using PowerLog.Core.Models;

namespace PowerLog.Core.Services
{
    public class WorkoutService : IWorkoutService
    {
        private readonly IWorkoutRepository workoutRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public WorkoutService(
            IWorkoutRepository workoutRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.workoutRepository = workoutRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<WorkoutDto>> GetAllAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var workouts = await workoutRepository.GetByUserIdAsync(userId, cancellationToken);
            return mapper.Map<IEnumerable<WorkoutDto>>(workouts);
        }

        public async Task<WorkoutDto> GetByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken = default)
        {
            var workout = await workoutRepository.GetByIdWithDetailsAsync(id, userId, cancellationToken);

            if (workout == null)
            {
                throw new NotFoundException("Такая тренировка не найдена.");
            }

            return mapper.Map<WorkoutDto>(workout);
        }

        public async Task<WorkoutDto> CreateAsync(CreateWorkoutDto dto, Guid userId, CancellationToken cancellationToken = default)
        {
            var workout = mapper.Map<Workout>(dto);
            workout.UserId = userId;
            workout.Planned = workout.Planned.ToUtc();
            workout.Actual = workout.Actual?.ToUtc();

            workoutRepository.Add(workout);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<WorkoutDto>(workout);
        }

        public async Task<WorkoutDto> UpdateAsync(UpdateWorkoutDto dto, Guid id, Guid userId, CancellationToken cancellationToken = default)
        {
            var workout = await workoutRepository.GetByIdWithDetailsAsync(id, userId, cancellationToken);

            if (workout == null)
            {
                throw new NotFoundException("Такая тренировка не найдена.");
            }

            mapper.Map(dto, workout);
            workout.Planned = workout.Planned.ToUtc();
            workout.Actual = workout.Actual?.ToUtc();

            workoutRepository.Update(workout);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<WorkoutDto>(workout);
        }

        public async Task<bool> DeleteAsync(Guid id, Guid userId, CancellationToken cancellationToken = default)
        {
            var workout = await workoutRepository.GetByIdWithDetailsAsync(id, userId, cancellationToken);

            if (workout == null)
            {
                return false;
            }

            workoutRepository.Delete(workout);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
