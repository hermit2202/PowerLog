using AutoMapper;
using PowerLog.Core.DTOs.Exercise;
using PowerLog.Core.DTOs.PersonalRecord;
using PowerLog.Core.DTOs.Set;
using PowerLog.Core.DTOs.User;
using PowerLog.Core.DTOs.Workout;
using PowerLog.Core.DTOs.WorkoutExercise;
using PowerLog.Core.Models;

namespace PowerLog.Core.Services.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Workout, WorkoutDto>()
            .ForMember(dest => dest.Exercises, opt => opt.MapFrom(src => src.WorkoutExercises));
        CreateMap<WorkoutExercise, WorkoutExerciseDto>()
            .ForMember(dest => dest.Sets, opt => opt.MapFrom(src => src.Sets));
        CreateMap<Set, SetDto>();
        CreateMap<CreateWorkoutDto, Workout>()
            .ForMember(dest => dest.WorkoutId, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.WorkoutExercises, opt => opt.Ignore());
        CreateMap<UpdateWorkoutDto, Workout>()
            .ForMember(dest => dest.WorkoutId, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.WorkoutExercises, opt => opt.Ignore());

        CreateMap<User, UserDto>();
        CreateMap<RegisterDto, User>()
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());

        CreateMap<Exercise, ExerciseDto>();
        CreateMap<CreateExerciseDto, Exercise>()
            .ForMember(dest => dest.ExerciseId, opt => opt.Ignore());
        CreateMap<UpdateExerciseDto, Exercise>()
            .ForMember(dest => dest.ExerciseId, opt => opt.Ignore());

        CreateMap<PersonalRecord, PersonalRecordDto>();
        CreateMap<CreatePersonalRecordDto, PersonalRecord>()
            .ForMember(dest => dest.PersonalRecordId, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.RecordDate, opt => opt.Ignore());
        CreateMap<UpdatePersonalRecordDto, PersonalRecord>()
            .ForMember(dest => dest.PersonalRecordId, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.RecordDate, opt => opt.Ignore());
    }
}
