using Application.Features.RefreshTokenFeature.Commands.DeleteOldRefreshTokenData;
using Infrastructure.Email;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefreshTokenController : ControllerBase
    {
        private IMediator _mediator;

        public RefreshTokenController(IMediator mediator)
        {
            _mediator=mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> DeleteOldData(DeleteOldRefreshTokenDataCommand command) {
            var data =await _mediator.Send(command);
            return Ok(data);
        }
    }
}
