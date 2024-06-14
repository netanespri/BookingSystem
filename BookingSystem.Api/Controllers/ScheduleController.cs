using BookingSystem.Application.Features.BookAppointments.Commands.BookAppointment;
using BookingSystem.Application.Features.BookAppointments.Queries.GetWeeklySchedule;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Api.Controllers
{
    public class ScheduleController : BaseApiController<ScheduleController>
    {
        public ScheduleController(IMediator mediator, ILogger<ScheduleController> logger) : base(mediator, logger)
        {
        }

        [HttpGet("availability/week/{date}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetWeeklyAvailableSchedule(string date)
        {
            var query = new GetWeeklyScheduleQuery 
            { 
                Date = date
            };

            var response = await Mediator.Send(query);

            if(!response.Succeeded)
            {
                BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost("appointment")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> BookAppointment(BookAppointmentCommand bookAppointmentCommand)
        {
            var response = await Mediator.Send(bookAppointmentCommand);

            if (!response.Succeeded)
            {
                return BadRequest(Constants.BookAppointment.FailureMessage);
            }

            return Ok(Constants.BookAppointment.SuccessfulMessage);          
        }
    }
}