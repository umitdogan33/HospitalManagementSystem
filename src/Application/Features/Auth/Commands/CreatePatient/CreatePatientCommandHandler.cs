using Application.Common.Dependency;
using Application.Common.Utilities;
using Application.Features.Auth.Dtos;
using Application.Features.Auth.Rules;
using Application.Repositories.EntityFramework;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Auth.Commands.CreatePatient;

public class CreatePatientCommandHandler : IRequestHandler<CreatePatientCommand, CreatedUserDto>
{
    //readonly => sadece ctor'da setlenmesi için
    private readonly IServiceFactory _serviceFactory;
    private readonly IUserRepository _userRepository;
    private readonly IPatientRepository _petientRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly ITokenHelper _tokenHelper;
    private readonly IHttpContextAccessor _accessor;
    private readonly IMapper _mapper;

    public CreatePatientCommandHandler(IMapper mapper,IServiceFactory serviceFactory)
    {
        _serviceFactory = serviceFactory;
        _petientRepository = _serviceFactory.CreateService<IPatientRepository>();
        _userRepository = _serviceFactory.CreateService<IUserRepository>();
        _refreshTokenRepository = _serviceFactory.CreateService<IRefreshTokenRepository>();
        _tokenHelper = _serviceFactory.CreateService<ITokenHelper>();
        _accessor = _serviceFactory.CreateService<IHttpContextAccessor>();
        _mapper = mapper;
    }

    public async Task<CreatedUserDto> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
    {
        var findedData =await _userRepository.GetAsync(p=>p.Email == request.Email);

        var remoteIpAddress = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();

        AuthBusinessRules.BeUniqueEmail(findedData);
        HashingHelper.CreatePasswordHash(request.Password, out var passwordHash, out var passwordSalt);
        User user = new()
        {
            Id = Guid.NewGuid().ToString(),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Status = true,
        };

        var createdUser = await _userRepository.AddAsync(user);

        Patient patient = new() {Id=Guid.NewGuid().ToString(), UserId = createdUser.Id};
        var createdPatient = await _petientRepository.AddAsync(patient);
        var token = _tokenHelper.CreateRefreshToken(createdUser, remoteIpAddress);
        await _refreshTokenRepository.AddAsync(token);
        return _mapper.Map<CreatedUserDto>(token);
    }
}
