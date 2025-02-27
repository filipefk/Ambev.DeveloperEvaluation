using Ambev.DeveloperEvaluation.Application.Users.UpdateUser;
using Ambev.DeveloperEvaluation.Domain.Enums;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.UpdateUser;

/// <summary>
/// Profile for mapping between Application and API UpdateUser responses
/// </summary>
public class UpdateUserProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for UpdateUser feature
    /// </summary>
    public UpdateUserProfile()
    {
        CreateMap<UpdateUserRequest, UpdateUserCommand>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => EnumValidatorUtil.ConvertToEnum<UserStatus>(src.Status)))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => EnumValidatorUtil.ConvertToEnum<UserRole>(src.Role)));

        CreateMap<UpdateUserResult, UpdateUserResponse>();
    }

}
