using Application.Appointments.Commands;
using Application.Auth.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;

namespace WebApI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private IMediator MediatorObject => HttpContext.RequestServices.GetService<IMediator>()!;

        public UserController()
        {

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Post([FromBody] LoginCommand command)
        {
            var result = await MediatorObject.Send(command);
            return RequestResponse.ReturnResponse(result);
        }

        [HttpPost("Registration")]
        public async Task<IActionResult> Put([FromBody] RegisrtrationCommand command)
        {
            var result = await MediatorObject.Send(command);
            return RequestResponse.ReturnResponse(result);
        }
    }
}
