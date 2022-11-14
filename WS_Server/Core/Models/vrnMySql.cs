using Microsoft.Extensions.Logging;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace WS_Server.Core.Models
{
    public static class vrnMySql
    {
        private static readonly string connectionDB = "server=*;user=*;database=*;password=*;";
        public static async Task<int> checkCams(List<Cam> cams, ILogger _logger)
        {
            List<CamDb> camDbList = new List<CamDb>();
            string commandTxtCams = string.Join(", ", cams.Select(x=> $"'{x.cam_id}'"));
            string sqlCommand = $"SELECT `cam_id`, `key` FROM wp_cams WHERE cam_id IN({commandTxtCams})";
            using (MySqlConnection conect = new MySqlConnection(connectionDB))
            {
                await conect.OpenAsync();
                MySqlCommand commandExec = new MySqlCommand(sqlCommand, conect);
                using (var resulExecut = await commandExec.ExecuteReaderAsync()) 
                {
                    while (resulExecut.Read())
                    {
                        camDbList.Add(new CamDb()
                        {
                            cam_id = resulExecut["cam_id"].ToString(),
                            key = resulExecut["key"].ToString()
                        });
                    }
                }
            }
            for(int i = 0; i<cams.Count; i++)
            {
                if (camDbList[i].matchesToken(cams[i].token,_logger) == false)
                {
                    return 403;
                }
            }
            return 200;
        }

    }
}
