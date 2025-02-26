using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Domain.Enums;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;

/// <summary>
/// Profile for mapping between Application and API CreateUser responses
/// </summary>
public class CreateUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateUser feature
    /// </summary>
    public CreateUserProfile()
    {
        CreateMap<CreateUserRequest, CreateUserCommand>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => EnumValidatorUtil.ConvertToEnum<UserStatus>(src.Status)))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => EnumValidatorUtil.ConvertToEnum<UserRole>(src.Role)));

        CreateMap<CreateUserResult, CreateUserResponse>();
    }

}