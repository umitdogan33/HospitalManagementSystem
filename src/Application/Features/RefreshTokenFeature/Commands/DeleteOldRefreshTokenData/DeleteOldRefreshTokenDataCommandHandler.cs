using Application.Common.Dependency;
using Application.Features.RefreshTokenFeature.Dtos;
using Application.Repositories.EntityFramework;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.RefreshTokenFeature.Commands.DeleteOldRefreshTokenData
{
    public class DeleteOldRefreshTokenDataCommandHandler : IRequestHandler<DeleteOldRefreshTokenDataCommand, DeletedRefreshTokenData>
    {
        private readonly IServiceFactory _serviceFactory;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public DeleteOldRefreshTokenDataCommandHandler(IServiceFactory serviceFactory)
        {
            _serviceFactory=serviceFactory;
            _refreshTokenRepository = _serviceFactory.CreateService<IRefreshTokenRepository>();
        }

        public async Task<DeletedRefreshTokenData> Handle(DeleteOldRefreshTokenDataCommand request, CancellationToken cancellationToken)
        {
            var data = await _refreshTokenRepository.GetAllAsync(p=>p.RefreshTokenExpiration < DateTime.UtcNow);
            foreach (var item in data)
            {
                await _refreshTokenRepository.DeleteAsync(item);
            }

            return new DeletedRefreshTokenData {Success = true };
        }
    }
}
