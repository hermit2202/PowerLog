using PowerLog.Core.DTOs.Workout;

namespace PowerLog.Core.Interfaces
{
    public interface IWorkoutService
    {
        Task<IEnumerable<WorkoutDto>> GetAllAsync(Guid userId);
        Task<WorkoutDto> GetByIdAsync(Guid id);
        Task<WorkoutDto> CreateAsync(CreateWorkoutDto dto, Guid userId);
        Task<WorkoutDto> UpdateAsync(UpdateWorkoutDto dto, Guid id);
        Task<bool> DeleteAsync(Guid id);
    }
}
