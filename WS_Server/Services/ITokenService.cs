using System.Collections.Generic;
using WS_Server.Core.Models;

namespace WS_Server.Services
{
    public interface ITokenService
    {
        void Convert(List<Cam> cams, string name, string value);
        bool MatchesToken(string camId, string key, string tokenIn);
    }
}