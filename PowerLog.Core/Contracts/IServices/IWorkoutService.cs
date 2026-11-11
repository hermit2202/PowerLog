using PowerLog.Core.DTOs.Workout;

namespace PowerLog.Core.Contracts.IServices
{
    public interface IWorkoutService
    {
        Task<IEnumerable<WorkoutDto>> GetAllAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<WorkoutDto> GetByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);
        Task<WorkoutDto> CreateAsync(CreateWorkoutDto dto, Guid userId, CancellationToken cancellationToken = default);
        Task<WorkoutDto> UpdateAsync(UpdateWorkoutDto dto, Guid id, Guid userId, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);
    }
}
