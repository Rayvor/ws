using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using WS_Server.Core.Models;

namespace WS_Server.Services
{
    public class TokenService : ITokenService
    {
        private readonly ILogger<ITokenService> _logger;

        public TokenService(ILogger<TokenService> logger)
        {
            _logger = logger;
        }

        public bool MatchesToken(string camId, string key, string tokenIn)
        {
            //[3]-startime/[2]-endtime/[1]-salt
            var tk = tokenIn.Split("-");
            if (tk.Length > 3)
            {
                string hash = Hash($"{camId}no_check_ip{tk[3]}{tk[2]}{key}{tk[1]}");
                return hash == tk[0];
            }

            return false;
        }        

        public void Convert(List<Cam> cams, string name, string value)
        {
            if (cams == null)
                throw new ArgumentNullException(nameof(cams));

            if (value == null)
                throw new ArgumentNullException(nameof(value));

            _logger.LogTrace(value);

            if (value.Contains('='))
            {
                try
                {
                    var camsParams = value.Split(",");
                    foreach (var camParam in camsParams)
                    {
                        var camT = camParam.Split("=");
                        cams.Add(new Cam
                        {
                            CamId = camT[0],
                            Token = camT[1]
                        });
                    }
                    return;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    return;
                }
            }                

            _logger.LogTrace("нет символа =");

            cams.Add(new Cam
            {
                CamId = name,
                Token = value
            });
        }

        private string Hash(string input)
        {
            using SHA1 sha1 = SHA1.Create();
            var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
            return string.Concat(hash.Select(b => b.ToString("x2")));
        }
    }
}
