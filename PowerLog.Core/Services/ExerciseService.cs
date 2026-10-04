using AutoMapper;
using PowerLog.Core.Contracts.Data;
using PowerLog.Core.DTOs.Exercise;
using PowerLog.Core.Interfaces;
using PowerLog.Core.Models;

namespace PowerLog.Core.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly IExerciseRepository exerciseRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ExerciseService(
            IExerciseRepository exerciseRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            this.exerciseRepository = exerciseRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ExerciseDto>> GetAllExerciseAsync(CancellationToken cancellationToken = default)
        {
            var exercises = await exerciseRepository.GetAllAsync(cancellationToken);
            return mapper.Map<IEnumerable<ExerciseDto>>(exercises);
        }

        public async Task<ExerciseDto> GetExerciseByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var exercise = await exerciseRepository.GetByIdAsync(id, cancellationToken);

            if (exercise == null)
            {
                throw new Exception("Упражнение не найдено.");
            }

            return mapper.Map<ExerciseDto>(exercise);
        }

        public async Task<ExerciseDto> CreateExerciseAsync(CreateExerciseDto dto, CancellationToken cancellationToken = default)
        {
            var exercise = mapper.Map<Exercise>(dto);

            exerciseRepository.Add(exercise);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<ExerciseDto>(exercise);
        }

        public async Task<ExerciseDto> UpdateExerciseAsync(UpdateExerciseDto dto, Guid id, CancellationToken cancellationToken = default)
        {
            var exercise = await exerciseRepository.GetByIdAsync(id, cancellationToken);

            if (exercise == null)
            {
                throw new Exception("Упражнение не найдено.");
            }

            mapper.Map(dto, exercise);

            exerciseRepository.Update(exercise);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return mapper.Map<ExerciseDto>(exercise);
        }

        public async Task<bool> DeleteExerciseAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var exercise = await exerciseRepository.GetByIdAsync(id, cancellationToken);

            if (exercise == null)
            {
                return false;
            }

            exerciseRepository.Delete(exercise);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
