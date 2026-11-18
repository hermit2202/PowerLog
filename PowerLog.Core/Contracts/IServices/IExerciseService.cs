using PowerLog.Core.DTOs.Exercise;

namespace PowerLog.Core.Contracts.IServices
{
    public interface IExerciseService
    {
        Task<IEnumerable<ExerciseDto>> GetAllAsync(Guid userId, CancellationToken cancellationToken = default);
        Task<ExerciseDto> GetByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);
        Task<ExerciseDto> CreateAsync(CreateExerciseDto dto, Guid userId, CancellationToken cancellationToken = default);
        Task<ExerciseDto> UpdateAsync(Guid id, UpdateExerciseDto dto, Guid userId, CancellationToken cancellationToken = default);
        Task DeleteAsync(Guid id, Guid userId, CancellationToken cancellationToken = default);
    }
}
