using Application.Common.Behaviours.Authorization;
using Application.Features.RefreshTokenFeature.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.RefreshTokenFeature.Commands.DeleteOldRefreshTokenData
{
    public class DeleteOldRefreshTokenDataCommand : IRequest<DeletedRefreshTokenData>, ISecuredRequest
    {
        public string[] Roles => new string[] {"Admin","admin","ADMIN"};
    }
}
