using Application.Common.Utilities;
using Application.Features.Auth.Dtos;
using Application.Features.Auth.Rules;
using Application.Repositories.EntityFramework;
using Application.Services;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.CreateDoctor
{
    public class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand, CreatedDoctorDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IEmailHelper _emailHelper;
        private IHttpContextAccessor _accessor;

        public CreateDoctorCommandHandler(IUserRepository userRepository, IDoctorRepository doctorRepository,IHttpContextAccessor accessor, IEmailHelper emailHelper)
        {
            _userRepository=userRepository;
            _doctorRepository=doctorRepository;
            _accessor=accessor;
            _emailHelper=emailHelper;
        }

        public async Task<CreatedDoctorDto> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
        {
            var password = Guid.NewGuid().ToString().Substring(1, 6);

            var findedData = await _userRepository.GetAsync(p => p.Email == request.Email);

            var remoteIpAddress = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();

            AuthBusinessRules.BeUniqueEmail(findedData);
            HashingHelper.CreatePasswordHash(password, out var passwordHash, out var passwordSalt);
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

            Doctor doctor = new() {Id=Guid.NewGuid().ToString(), UserId = createdUser.Id,Biography=request.Biography };
            var createdPatient = await _doctorRepository.AddAsync(doctor);
            _emailHelper.SendEmail(user.Email, "registration successful",password);

            return new CreatedDoctorDto { Success=true};
        }
    }
}
