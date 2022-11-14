using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using WS_Server.Core.Models;
using WS_Server.Data;

namespace WS_Server
{
    [Route("api/[controller]")]
    [ApiController]
    public class BypassController : ControllerBase
    {
        private readonly ILogger<BypassController> _logger;
        private readonly DbContext _dbContext;

        public BypassController(
            ILogger<BypassController> logger,
            DbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ForecastQuery query)
        {
            _logger.LogInformation("----------------Connected--------------------");

            try
            {
                if (query.Cams.Any())
                {
                    var result = await _dbContext.CheckCams(query.Cams);
                    return StatusCode(result);
                }

                return Ok();
            }
            catch(Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return Forbid();
            }
        }
       
    }
}