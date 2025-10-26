using Microsoft.AspNetCore.Mvc;
using Trading.Core.Interfaces;
using Trading.Core.Interfaces.Data;
using Trading.Core.Models;

namespace Trading.API.Controllers
{
    [Route("api/trades")]
    public class TradesController:ControllerBase
    {
        private readonly ITradingService _tradingService;

        public TradesController(ITradingService tradingService)
        {
            _tradingService = tradingService;
        }

        /// <summary>
        /// Lists users of the logged in user
        /// </summary>
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<TradeDetails>>> ListTradesAsync()
        {
            var userId = 1;

            var trades = await _tradingService.ListTradesByUserAsync(userId);
            return Ok(trades);
        }

        [Route("")]
        [HttpPost]
        public async Task<ActionResult> CreateTradeAsync([FromBody] TradeCreationDetails trade)
        {
            var userId = 1;
            trade.UserId = userId;

            var tradeID = await _tradingService.CreateTradeAsync(trade);
            return CreatedAtRoute(string.Empty,tradeID);
        }
    }
}
