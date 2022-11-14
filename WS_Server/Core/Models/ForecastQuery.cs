using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WS_Server.Services;

namespace WS_Server.Core.Models
{
    public class ForecastQuery
    {
        private readonly ITokenService _tokenConverterService;
        private string _token;

        public ForecastQuery(ITokenService tokenConverterService)
        {
            _tokenConverterService = tokenConverterService;
        }

        public List<Cam> Cams { get; private set; } = new List<Cam>();

        [FromQuery]
        public string Name { get; set; }

        [FromQuery]
        public string Token { get => _token; set => _tokenConverterService.Convert(Cams, Name, value); }
    }
}
