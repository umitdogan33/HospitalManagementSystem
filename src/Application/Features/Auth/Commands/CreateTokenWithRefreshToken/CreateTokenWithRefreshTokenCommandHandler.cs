using Application.Common.Dependency;
using Application.Common.Exceptions;
using Application.Common.Utilities;
using Application.Features.Auth.Dtos;
using Application.Features.Auth.Rules;
using Application.Repositories.EntityFramework;
using Application.Services;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.CreateTokenWithRefreshToken
{
    public class CreateTokenWithRefreshTokenCommandHandler : IRequestHandler<CreateTokenWithRefreshTokenCommand, CreatedTokenWithRefreshToken>
    {
        private readonly IServiceFactory _serviceFactory;
        private readonly IUserRepository _repository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IHttpContextAccessor _accessor;
        private readonly ITokenHelper _tokenHelper;

        public CreateTokenWithRefreshTokenCommandHandler(IServiceFactory factory)
        {
            _serviceFactory = factory;
            _repository = _serviceFactory.CreateService<IUserRepository>();
            _refreshTokenRepository = _serviceFactory.CreateService<IRefreshTokenRepository>();
            _accessor = _serviceFactory.CreateService<IHttpContextAccessor>();
            _tokenHelper = _serviceFactory.CreateService<ITokenHelper>();
        }

        public async Task<CreatedTokenWithRefreshToken> Handle(CreateTokenWithRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var remoteIpAddress = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();

            var refreshTokenData = await _refreshTokenRepository.GetAsync(p => p.UserId == request.UserId && p.Client == request.Client && p.RefreshTokenValue == request.RefreshToken && p.IpAddress == remoteIpAddress, include: m => m.Include(c => c.User));

             AuthBusinessRules.ThereIsData(refreshTokenData);

            AuthBusinessRules.MatchIpAddress(refreshTokenData.IpAddress, remoteIpAddress);

           var createdToken =  _tokenHelper.CreateRefreshTokenWithStillClient(refreshTokenData.User, remoteIpAddress,refreshTokenData.Client);


            await _refreshTokenRepository.DeleteAsync(refreshTokenData);
            await _refreshTokenRepository.AddAsync(createdToken);

            return new CreatedTokenWithRefreshToken
            {
                Client = createdToken.Client,
                RefreshTokenValue = createdToken.RefreshTokenValue,
                Token = createdToken.Token,
                TokenExpiration = createdToken.TokenExpiration
                
            };
        }
    }
}
