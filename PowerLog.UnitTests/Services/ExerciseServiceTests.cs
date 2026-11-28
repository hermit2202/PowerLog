using AutoMapper;
using NSubstitute;
using PowerLog.Core.Contracts.Common;
using PowerLog.Core.Contracts.IData;
using PowerLog.Core.Exceptions;
using PowerLog.Core.Models;
using PowerLog.Core.Services;
using Xunit;

namespace PowerLog.UnitTests.Services;

public class ExerciseServiceTests
{
    private readonly ExerciseService sut;
    private readonly IExerciseRepository repository = Substitute.For<IExerciseRepository>();
    private readonly IUnitOfWork unitOfWork = Substitute.For<IUnitOfWork>();
    private readonly IMapper mapper = Substitute.For<IMapper>();
    private readonly IDateTimeProvider dateTimeProvider = Substitute.For<IDateTimeProvider>();

    public ExerciseServiceTests()
    {
        sut = new ExerciseService(repository, unitOfWork,
            mapper, dateTimeProvider);
    }

    [Fact]
    public async Task DeleteAsync_SystemExercise_ThrowsForbidden()
    {
        // Arrange
        var exerciseId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        var exercise = new Exercise{ ExerciseId = exerciseId, Name = "Тестовое упражнение"};
        repository.GetAvailableByIdAsync(exerciseId, userId, Arg.Any<CancellationToken>()).Returns(exercise);

        // Act & Assert
        await Assert.ThrowsAsync<ForbiddenException>(() => sut.DeleteAsync(exerciseId, userId));

        repository.DidNotReceive().Delete(Arg.Any<Exercise>());
        repository.DidNotReceive().Update(Arg.Any<Exercise>());
        await unitOfWork.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task DeleteAsync_ExerciseNotFound_ThrowsNotFound()
    {
        // Arrange
        var exerciseId = Guid.NewGuid();
        var userId = Guid.NewGuid();
        repository.GetAvailableByIdAsync(exerciseId, userId, Arg.Any<CancellationToken>()).Returns((Exercise?)null);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => sut.DeleteAsync(exerciseId, userId));

        repository.DidNotReceive().Delete(Arg.Any<Exercise>());
        repository.DidNotReceive().Update(Arg.Any<Exercise>());
        await unitOfWork.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task DeleteAsync_UnusedExercise_DeletesPhysically()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var exerciseId = Guid.NewGuid();
        var exercise = new Exercise{ ExerciseId =  exerciseId, UserId =  userId, Name = "Тестовое упражнение"};
        repository.GetAvailableByIdAsync(exerciseId, userId, Arg.Any<CancellationToken>()).Returns(exercise);
        repository.IsUsedAsync(exerciseId, Arg.Any<CancellationToken>()).Returns(false);

        //Act
        await sut.DeleteAsync(exerciseId, userId);

        // Assert
        repository.Received().Delete(exercise);
        repository.DidNotReceive().Update(Arg.Any<Exercise>());
        await unitOfWork.Received().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task DeleteAsync_UsedExercise_SoftDeletes()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var exerciseId = Guid.NewGuid();
        var date = new DateTime(2026, 11, 28, 12, 0, 0, DateTimeKind.Utc);
        var exercise = new Exercise{ ExerciseId =  exerciseId, UserId =  userId, Name = "Тестовое упражнение"};
        repository.GetAvailableByIdAsync(exerciseId, userId, Arg.Any<CancellationToken>()).Returns(exercise);
        repository.IsUsedAsync(exerciseId, Arg.Any<CancellationToken>()).Returns(true);
        dateTimeProvider.UtcNow.Returns(date);

        //Act
        await sut.DeleteAsync(exerciseId, userId);

        // Assert
        repository.Received().Update(exercise);
        repository.DidNotReceive().Delete(Arg.Any<Exercise>());
        Assert.Equal(date, exercise.DeletedAt);
        Assert.True(exercise.IsDeleted);
        await unitOfWork.Received().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task UpdateAsync_ExerciseNotFound_ThrowsNotFound()
    {
        // Arrange

        //Act

        // Assert
    }

    [Fact]
    public async Task UpdateAsync_SystemExercise_ThrowsForbidden()
    {
        // Arrange

        // Act & Assert

    }

    [Fact]
    public async Task UpdateAsync_NameAlreadyExists_ThrowsConflict()
    {
        // Arrange

        // Act & Assert

    }

    [Fact(Skip = "TODO")]
    public async Task UpdateAsync_ValidData_UpdatesAndSaves()
    {
        // Arrange

        //Act

        // Assert
    }
}
