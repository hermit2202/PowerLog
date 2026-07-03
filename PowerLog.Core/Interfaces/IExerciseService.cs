using PowerLog.Core.DTOs.Exercise;
using PowerLog.Core.DTOs.PersonalRecord;

namespace PowerLog.Core.Interfaces
{
    public interface IExerciseService
    {
        Task<IEnumerable<ExerciseDto>> GetAllExerciseAsync();
        Task<ExerciseDto> GetExerciseByIdAsync(Guid id);
        Task<ExerciseDto> CreateExerciseAsync(CreateExerciseDto dto);
        Task<ExerciseDto> UpdateExerciseAsync(UpdateExerciseDto dto, Guid id);
        Task<bool> DeleteExerciseAsync(Guid id);

        Task<IEnumerable<PersonalRecordDto>> GetAllPersonalRecordsAsync(Guid userId, Guid exerciseId);
        Task<PersonalRecordDto> GetPersonalRecordByIdAsync(Guid id);
        Task<PersonalRecordDto> CreatePersonalRecordAsync(CreatePersonalRecordDto dto, Guid userId);
        Task<PersonalRecordDto> UpdatePersonalRecordAsync(UpdatePersonalRecordDto dto, Guid id);
        Task<bool> DeletePersonalRecordAsync(Guid id);
    }
}
