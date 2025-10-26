using Microsoft.AspNetCore.Mvc;
using Trading.Core.Interfaces.Data;
using Trading.Core.Models;

namespace Trading.API.Controllers
{
    [Route("api/securities")]
    public class SecuritiesController:ControllerBase
    {
        private readonly ISecurityRepository _securityRepository;

        public SecuritiesController(ISecurityRepository securityRepository)
        {
            _securityRepository = securityRepository;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<SecurityDetails>>> ListSecuritiesAsync()
        {
            var securities = await _securityRepository.ListSecuritiesAsync();
            return Ok(securities);
        }
    }
}
