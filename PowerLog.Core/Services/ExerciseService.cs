using AutoMapper;
using PowerLog.Core.Contracts.Common;
using PowerLog.Core.Contracts.IData;
using PowerLog.Core.Contracts.IServices;
using PowerLog.Core.DTOs.Exercise;
using PowerLog.Core.Exceptions;
using PowerLog.Core.Models;

namespace PowerLog.Core.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly IExerciseRepository exerciseRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IDateTimeProvider dateTimeProvider;

        public ExerciseService(
            IExerciseRepository exerciseRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IDateTimeProvider dateTimeProvider)
        {
            this.exerciseRepository = exerciseRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.dateTimeProvider = dateTimeProvider;
        }

        public async Task<IEnumerable<ExerciseDto>> GetAllAsync(Guid userId, CancellationToken cancellationToken = default)
        {
            var exercises = await exerciseRepository.GetAvailableAsync(userId, cancellationToken);
            return mapper.Map<IEnumerable<ExerciseDto>>(exercises);
        }

        public async Task<ExerciseDto> GetByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken = default)
        {
            var exercise = await exerciseRepository.GetAvailableByIdAsync(id, userId, cancellationToken);

            if (exercise == null)
            {
                throw new NotFoundException("Нет доступного упражнения с таким Id");
            }

            return mapper.Map<ExerciseDto>(exercise);
        }

        public async Task<ExerciseDto> CreateAsync(CreateExerciseDto dto, Guid userId, CancellationToken cancellationToken = default)
        {
            var existsName = await exerciseRepository.NameExistsAsync(dto.Name, userId, null, cancellationToken);
            if (existsName)
            {
                throw new ConflictException("Упражнение с таким названием уже существует");
            }

            var exercise = mapper.Map<Exercise>(dto);

            exercise.UserId = userId;
            exercise.Name = dto.Name.Trim();

            exerciseRepository.Add(exercise);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<ExerciseDto>(exercise);
        }

        public async Task<ExerciseDto> UpdateAsync(Guid id, UpdateExerciseDto dto, Guid userId, CancellationToken cancellationToken = default)
        {
            var exercise = await GetOwnedAsync(id, userId, cancellationToken);

            var existsName = await exerciseRepository.NameExistsAsync(dto.Name, userId, id, cancellationToken);

            if (existsName)
            {
                throw new ConflictException("Упражнение с таким названием уже существует");
            }

            mapper.Map(dto, exercise);

            exercise.Name = dto.Name.Trim();

            exerciseRepository.Update(exercise);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<ExerciseDto>(exercise);
        }

        public async Task DeleteAsync(Guid id, Guid userId, CancellationToken cancellationToken = default)
        {
            var exercise = await GetOwnedAsync(id, userId, cancellationToken);

            var isUsed = await exerciseRepository.IsUsedAsync(id, cancellationToken);
            if (isUsed)
            {
                exercise.IsDeleted = true;
                exercise.DeletedAt = dateTimeProvider.UtcNow;
                exerciseRepository.Update(exercise);
            }
            else
            {
                exerciseRepository.Delete(exercise);
            }
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        private async Task<Exercise> GetOwnedAsync(Guid id, Guid userId, CancellationToken cancellationToken)
        {
            var exercise = await exerciseRepository.GetAvailableByIdAsync(id, userId, cancellationToken);

            if (exercise == null)
            {
                throw new NotFoundException("Нет доступного упражнения с таким Id");
            }

            if (exercise.UserId == null)
            {
                throw new ForbiddenException("Это системное упражнение");
            }

            return exercise;
        }
    }
}
