using Application.Appointments.Commands;
using Application.Appointments.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;

namespace WebApI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {

        private IMediator MediatorObject => HttpContext.RequestServices.GetService<IMediator>()!;

        public AppointmentController()
        {

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AppointmentAddCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await MediatorObject.Send(command);
            return RequestResponse.ReturnResponse(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] AppointmentUpdateCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await MediatorObject.Send(command);
            return RequestResponse.ReturnResponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var result = await MediatorObject.Send(new AppointmentDeleteCommand() { Id = id });
            return RequestResponse.ReturnResponse(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int pageNumber = 1, int pageSize = 10)
        {
            var result = await MediatorObject.Send(new AppointmentListQuery()
            {
                pageNumber = pageNumber,
                pageSize = pageSize
            });
            return RequestResponse.ReturnResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var result = await MediatorObject.Send(new AppointmentByIdQuery() { Id = id });
            return RequestResponse.ReturnResponse(result);
        }
    }
}
