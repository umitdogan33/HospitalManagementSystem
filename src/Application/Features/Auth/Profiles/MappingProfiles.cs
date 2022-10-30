using Application.Common.Paging;
using Application.Features.Auth.Commands.CreatePatient;
using Application.Features.Auth.Commands.LoginUserCommand;
using Application.Features.Auth.Dtos;
using Application.Features.Auth.Models;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Auth.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreatePatientCommandHandler, CreatedUserDto>().ReverseMap();
        CreateMap<RefreshToken, CreatedUserDto>().ReverseMap();
        CreateMap<LoginUserCommand, LoginedUserDto>().ReverseMap();
        CreateMap<RefreshToken, LoginedUserDto>().ReverseMap();

        CreateMap<User, UserListDto>()
            .ForMember(c => c.OperationClaims,
                opt => opt.MapFrom(c => c.UserOperationClaims.Select(x => x.OperationClaim))).ReverseMap();
        CreateMap<IPaginate<User>, UserListModel>().ReverseMap();
    }
}