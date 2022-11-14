using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WS_Server.Core.Models;
using Dapper;
using WS_Server.Services;

namespace WS_Server.Data
{
    public class DbContext : IDisposable
    {
        private readonly MySqlConnection _connection;
        private readonly ILogger<DbContext> _logger;
        private readonly ITokenService _tokenService;

        public DbContext(
            IConfiguration configuration,
            ILogger<DbContext> logger,
            ITokenService tokenService)
        {
            var connectionString = configuration.GetConnectionString("MySql");
            _connection = new MySqlConnection(connectionString);
            _logger = logger;
            _tokenService = tokenService;
        }

        public async Task<int> CheckCams(List<Cam> cams)
        {
            string commandTxtCams = string.Join(", ", cams.Select(x => $"'{x.CamId}'"));

            string sqlCommand = @$"
                SELECT cam_id as CamId, key as Key 
                FROM wp_cams
                WHERE cam_id IN @commandTxtCams";

            var camDbList = (await _connection.QueryAsync<CamDb>(sqlCommand, new { commandTxtCams })).ToList();

            for (int i = 0; i < cams.Count; i++)
            {
                if (!_tokenService.MatchesToken(camDbList[i].CamId, camDbList[i].Key, cams[i].Token))
                    return 403;
            }

            return 200;
        }

        public void Dispose() => _connection?.Dispose();
    }
}
