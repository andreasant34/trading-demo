using MediatR;
using Microsoft.AspNetCore.Mvc;
using Trading.API.Queries;
using Trading.Core.Models;

namespace Trading.API.Controllers
{
    [Route("api/securities")]
    public class SecuritiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SecuritiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<SecurityDetails>>> ListSecuritiesAsync()
        {
            var securities = await _mediator.Send(new ListSecuritiesQuery { });
            return Ok(securities);
        }
    }
}
