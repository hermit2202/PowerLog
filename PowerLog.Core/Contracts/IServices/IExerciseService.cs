using PowerLog.Core.DTOs.Exercise;

namespace PowerLog.Core.Contracts.IServices
{
    public interface IExerciseService
    {
        Task<IEnumerable<ExerciseDto>> GetAllExerciseAsync(CancellationToken cancellationToken = default);
        Task<ExerciseDto> GetExerciseByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<ExerciseDto> CreateExerciseAsync(CreateExerciseDto dto, CancellationToken cancellationToken = default);
        Task<ExerciseDto> UpdateExerciseAsync(UpdateExerciseDto dto, Guid id, CancellationToken cancellationToken = default);
        Task<bool> DeleteExerciseAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
