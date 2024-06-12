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

            return Ok(response.Data.DailySlots);
        }
    }
}