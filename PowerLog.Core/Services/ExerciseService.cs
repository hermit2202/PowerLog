using PowerLog.Core.DTOs.Exercise;
using PowerLog.Core.DTOs.PersonalRecord;
using PowerLog.Core.Interfaces;
using PowerLog.Core.Models;

namespace PowerLog.Core.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly IRepository<Exercise> exerciseRepository;
        private readonly IRepository<PersonalRecord> personalRecordRepository;

        public ExerciseService(IRepository<Exercise> exerciseRepository, IRepository<PersonalRecord> personalRecordRepository)
        {
            this.exerciseRepository = exerciseRepository;
            this.personalRecordRepository = personalRecordRepository;
        }

        public async Task<IEnumerable<ExerciseDto>> GetAllExerciseAsync()
        {
            var exercises = await exerciseRepository.GetAllAsync();

            return exercises.Select(e => new ExerciseDto
            {
                ExerciseId = e.ExerciseId,
                Name = e.Name,
                Type = e.Type,
                Description = e.Description,
            });
        }

        public async Task<ExerciseDto> GetExerciseByIdAsync(Guid id)
        {
            var exercise = await exerciseRepository.GetByIdAsync(id);
            if (exercise == null)
            {
                throw new Exception("Упражнение не найдено.");
            }

            var dto = new ExerciseDto
            {
                ExerciseId = exercise.ExerciseId,
                Name = exercise.Name,
                Type = exercise.Type,
                Description = exercise.Description,
            };

            return dto;
        }

        public async Task<ExerciseDto> CreateExerciseAsync(CreateExerciseDto dto)
        {
            var exercise = new Exercise
            {
                Name = dto.Name,
                Type = dto.Type,
                Description = dto.Description,
            };

            await exerciseRepository.CreateAsync(exercise);

            return new ExerciseDto
            {
                ExerciseId = exercise.ExerciseId,
                Name = exercise.Name,
                Type = exercise.Type,
                Description = exercise.Description,
            };
        }

        public async Task<ExerciseDto> UpdateExerciseAsync(UpdateExerciseDto dto, Guid id)
        {
            var exercise = await exerciseRepository.GetByIdAsync(id);
            if (exercise == null)
            {
                throw new Exception("Упражнение не найдено.");
            }

            exercise.Name = dto.Name;
            exercise.Type = dto.Type;
            exercise.Description = dto.Description;

            await exerciseRepository.UpdateAsync(exercise);

            return new ExerciseDto
            {
                ExerciseId = exercise.ExerciseId,
                Name = exercise.Name,
                Type = exercise.Type,
                Description = exercise.Description,
            };
        }

        public async Task<bool> DeleteExerciseAsync(Guid id)
        {
            var exercise = await exerciseRepository.GetByIdAsync(id);
            if (exercise == null)
            {
                return false;
            }

            await exerciseRepository.DeleteAsync(id);

            return true;
        }


        public async Task<IEnumerable<PersonalRecordDto>> GetAllPersonalRecordsAsync(Guid userId, Guid exerciseId)
        {
            var personalRecords = (await personalRecordRepository.GetAllAsync())
                .Where(p => p.UserId == userId && p.ExerciseId == exerciseId);

            return personalRecords.Select(p => new PersonalRecordDto
            {
                PersonalRecordId = p.PersonalRecordId,
                UserId = userId,
                ExerciseId = p.ExerciseId,
                Weight = p.Weight,
                Reps = p.Reps,
                Rpe = p.Rpe,
                RecordDate = p.RecordDate,
            });
        }

        public async Task<PersonalRecordDto> GetPersonalRecordByIdAsync(Guid id)
        {
            var personalRecord = await personalRecordRepository.GetByIdAsync(id);
            if (personalRecord == null)
            {
                throw new Exception("Персональный рекорд не найден.");
            }

            return new PersonalRecordDto
            {
                PersonalRecordId = personalRecord.PersonalRecordId,
                UserId = personalRecord.UserId,
                ExerciseId = personalRecord.ExerciseId,
                Weight = personalRecord.Weight,
                Reps = personalRecord.Reps,
                Rpe = personalRecord.Rpe,
                RecordDate = personalRecord.RecordDate,
            };
        }

        public async Task<PersonalRecordDto> CreatePersonalRecordAsync(CreatePersonalRecordDto dto, Guid userId)
        {
            var personalRecord = new PersonalRecord
            {
                UserId = userId,
                ExerciseId = dto.ExerciseId,
                Weight = dto.Weight,
                Reps = dto.Reps,
                Rpe = dto.Rpe,
                RecordDate = DateTime.UtcNow,
            };

            await personalRecordRepository.CreateAsync(personalRecord);

            return new PersonalRecordDto
            {
                PersonalRecordId = personalRecord.PersonalRecordId,
                UserId = personalRecord.UserId,
                ExerciseId = personalRecord.ExerciseId,
                Weight = personalRecord.Weight,
                Reps = personalRecord.Reps,
                Rpe = personalRecord.Rpe,
                RecordDate = personalRecord.RecordDate,
            };
        }

        public async Task<PersonalRecordDto> UpdatePersonalRecordAsync(UpdatePersonalRecordDto dto, Guid id)
        {
            var personalRecord = await personalRecordRepository.GetByIdAsync(id);
            if (personalRecord == null || id != personalRecord.PersonalRecordId)
            {
                throw new Exception("Персональный рекорд не найден.");
            }

            personalRecord.ExerciseId = dto.ExerciseId;
            personalRecord.Weight = dto.Weight;
            personalRecord.Reps = dto.Reps;
            personalRecord.Rpe = dto.Rpe;

            await personalRecordRepository.UpdateAsync(personalRecord);

            return new PersonalRecordDto
            {
                PersonalRecordId = personalRecord.PersonalRecordId,
                ExerciseId = personalRecord.ExerciseId,
                UserId = personalRecord.UserId,
                Weight = personalRecord.Weight,
                Rpe = personalRecord.Rpe,
                Reps = personalRecord.Reps,
                RecordDate = personalRecord.RecordDate
            };
        }

        public async Task<bool> DeletePersonalRecordAsync(Guid id)
        {
            var personalRecord = await personalRecordRepository.GetByIdAsync(id);
            if (personalRecord == null)
            {
                return false;
            }
            await personalRecordRepository.DeleteAsync(id);
            return true;
        }
    }
}
