using AutoMapper;
using PowerLog.Core.Contracts.Data;
using PowerLog.Core.Contracts.IServices;
using PowerLog.Core.DTOs.PersonalRecord;
using PowerLog.Core.Models;

namespace PowerLog.Core.Services
{
    public class PersonalRecordService : IPersonalRecordService
    {
        private readonly IPersonalRecordRepository personalRecordRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public PersonalRecordService(
            IPersonalRecordRepository personalRecordRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.personalRecordRepository = personalRecordRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<PersonalRecordDto>> GetAllPersonalRecordsAsync(
            Guid userId,
            Guid exerciseId,
            CancellationToken cancellationToken = default)
        {
            var personalRecords = await personalRecordRepository.GetByUserIdAndExerciseIdAsync(
                userId, exerciseId, cancellationToken);

            return mapper.Map<IEnumerable<PersonalRecordDto>>(personalRecords);
        }

        public async Task<PersonalRecordDto> GetPersonalRecordByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var personalRecord = await personalRecordRepository.GetByIdAsync(id, cancellationToken);

            if (personalRecord == null)
            {
                throw new Exception("Персональный рекорд не найден.");
            }

            return mapper.Map<PersonalRecordDto>(personalRecord);
        }

        public async Task<PersonalRecordDto> CreatePersonalRecordAsync(
            CreatePersonalRecordDto dto,
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            var personalRecord = mapper.Map<PersonalRecord>(dto);
            personalRecord.UserId = userId;
            personalRecord.RecordDate = DateTime.UtcNow;

            personalRecordRepository.Add(personalRecord);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<PersonalRecordDto>(personalRecord);
        }

        public async Task<PersonalRecordDto> UpdatePersonalRecordAsync(
            UpdatePersonalRecordDto dto,
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var personalRecord = await personalRecordRepository.GetByIdAsync(id, cancellationToken);

            if (personalRecord == null)
            {
                throw new Exception("Персональный рекорд не найден.");
            }

            mapper.Map(dto, personalRecord);

            personalRecordRepository.Update(personalRecord);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<PersonalRecordDto>(personalRecord);
        }

        public async Task<bool> DeletePersonalRecordAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var personalRecord = await personalRecordRepository.GetByIdAsync(id, cancellationToken);

            if (personalRecord == null)
            {
                return false;
            }

            personalRecordRepository.Delete(personalRecord);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
