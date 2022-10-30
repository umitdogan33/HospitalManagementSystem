using Application.Common.Utilities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController:ControllerBase
    {
        private readonly IMediator _mediator;

        public ImagesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public IActionResult Create([FromForm] IFormFile image) {
            FileHelper.Upload(image);
            return Ok("EZ");
        }


    }
}
