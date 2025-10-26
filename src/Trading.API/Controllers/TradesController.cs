using MediatR;
using Microsoft.AspNetCore.Mvc;
using Trading.API.Commands;
using Trading.API.Queries;

namespace Trading.API.Controllers
{
    [Route("api/trades")]
    public class TradesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TradesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Lists users of the logged in user
        /// </summary>
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<ListTradesDto>>> ListTradesAsync()
        {
            var trades = await _mediator.Send(new ListTradesQuery
            {
                UserId = 1 //TODO
            });

            return Ok(trades);
        }

        [Route("")]
        [HttpPost]
        public async Task<ActionResult<int>> CreateTradeAsync([FromBody] CreateTradeCommand trade)
        {
            var tradeID = await _mediator.Send(trade);
            return CreatedAtRoute(string.Empty, tradeID);
        }
    }
}
