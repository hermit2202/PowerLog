using AutoMapper;
using PowerLog.Core.Contracts.Common;
using PowerLog.Core.Contracts.IData;
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
        private readonly IExerciseRepository exerciseRepository;
        private readonly IDateTimeProvider dateTimeProvider;

        public PersonalRecordService(
            IPersonalRecordRepository personalRecordRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IExerciseRepository exerciseRepository,
            IDateTimeProvider dateTimeProvider)
        {
            this.personalRecordRepository = personalRecordRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.exerciseRepository = exerciseRepository;
            this.dateTimeProvider = dateTimeProvider;
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
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            var personalRecord = await personalRecordRepository.GetByIdAsync(id, userId, cancellationToken);

            if (personalRecord == null)
            {
                throw new KeyNotFoundException("Персональный рекорд не найден");
            }

            return mapper.Map<PersonalRecordDto>(personalRecord);
        }

        public async Task<PersonalRecordDto> CreatePersonalRecordAsync(
            CreatePersonalRecordDto dto,
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            var exercise = await exerciseRepository.GetAvailableByIdAsync(dto.ExerciseId, userId, cancellationToken);
            if (exercise == null)
            {
                throw new KeyNotFoundException("Нет доступного упражнения с таким Id");
            }

            var personalRecord = mapper.Map<PersonalRecord>(dto);
            personalRecord.UserId = userId;
            personalRecord.RecordDate = dateTimeProvider.UtcNow;

            personalRecordRepository.Add(personalRecord);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<PersonalRecordDto>(personalRecord);
        }

        public async Task<PersonalRecordDto> UpdatePersonalRecordAsync(
            UpdatePersonalRecordDto dto,
            Guid id,
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            var personalRecord = await personalRecordRepository.GetByIdAsync(id, userId, cancellationToken);

            if (personalRecord == null)
            {
                throw new KeyNotFoundException("Персональный рекорд не найден.");
            }

            mapper.Map(dto, personalRecord);

            personalRecordRepository.Update(personalRecord);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<PersonalRecordDto>(personalRecord);
        }

        public async Task<bool> DeletePersonalRecordAsync(
            Guid id,
            Guid userId,
            CancellationToken cancellationToken = default)
        {
            var personalRecord = await personalRecordRepository.GetByIdAsync(id, userId, cancellationToken);

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
