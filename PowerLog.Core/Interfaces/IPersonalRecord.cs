using PowerLog.Core.DTOs.PersonalRecord;

namespace PowerLog.Core.Interfaces
{
    public interface IPersonalRecordService
    {
        Task<IEnumerable<PersonalRecordDto>> GetAllPersonalRecordsAsync(Guid userId, Guid exerciseId, CancellationToken cancellationToken = default);
        Task<PersonalRecordDto> GetPersonalRecordByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<PersonalRecordDto> CreatePersonalRecordAsync(CreatePersonalRecordDto dto, Guid userId, CancellationToken cancellationToken = default);
        Task<PersonalRecordDto> UpdatePersonalRecordAsync(UpdatePersonalRecordDto dto, Guid id, CancellationToken cancellationToken = default);
        Task<bool> DeletePersonalRecordAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
