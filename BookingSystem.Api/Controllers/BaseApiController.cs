using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public abstract class BaseApiController<T> : ControllerBase where T : class
    {
        protected readonly IMediator Mediator;
        protected readonly ILogger Logger;

        public BaseApiController(IMediator mediator, ILogger<T> logger)
        {
            Mediator = mediator;
            Logger = logger;
        }
    }
}
