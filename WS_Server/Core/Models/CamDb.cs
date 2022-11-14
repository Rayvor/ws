using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace WS_Server.Core.Models
{
    public class CamDb
    {
        public string cam_id { get; set; }
        public string key { get; set; }

        public bool matchesToken(string tokenIn, ILogger _logger)
        {
            //_logger.LogInformation(cam_id);
            var tk = tokenIn.Split("-");//[3]-startime/[2]-endtime/[1]-salt
            string hash = Hash($"{cam_id}no_check_ip{tk[3]}{tk[2]}{key}{tk[1]}");
            //_logger.LogInformation($"{hash} : {tk[0]}");
            if (hash == tk[0])
                return true;
            else
                return false;
        }
        private string Hash(string input)
        {
            using (SHA1 sha1 = SHA1.Create())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                return String.Concat(hash.Select(b => b.ToString("x2")));
            }
        }
    }
}
