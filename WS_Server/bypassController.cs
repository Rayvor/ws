using Lextm.SharpSnmpLib;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WS_Server.Hubs.Telnet;
using static System.Net.Mime.MediaTypeNames;
using System.Net.Http.Json;
using System.Net;
using System.Linq;
using WS_Server.Core.Models;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WS_Server
{
    static class ExtensionHelpers
    {
        public static async Task<string> GetRawBodyAsync(this HttpRequest request, Encoding encoding = null)
        {
            if (!request.Body.CanSeek)
            {
                request.EnableBuffering();
            }
            request.Body.Position = 0;
            var reader = new StreamReader(request.Body, encoding ?? Encoding.UTF8);
            var body = await reader.ReadToEndAsync().ConfigureAwait(false);
            request.Body.Position = 0;
            return body;
        }
    }


    [Route("api/bypass")]
    [ApiController]
    public class bypassController : ControllerBase
    {
        private readonly ILogger _logger = Log.CreateLogger<bypassController>();
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ForecastQery qery)
        {
            //_logger.LogInformation("----------------Connected--------------------");
            try
            {
                if (qery.cams.Count > 0)
                {
                    return StatusCode(await vrnMySql.checkCams(qery.cams, _logger));
                }
                return StatusCode(403);
            }
            catch(Exception ex)
            {
                _logger.LogWarning(ex.Message);
                return StatusCode(403);
            }
        }
       
    }
}