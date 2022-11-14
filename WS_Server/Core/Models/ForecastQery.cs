using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;

namespace WS_Server.Core.Models
{
    public class ForecastQery
    {
        [FromQuery]
        public string name { get; set; }
        public string Token;
        public List<Cam> cams = new List<Cam>();

        [FromQuery]
        public string token
        {
            get => Token;
            set
            {
                Token = value;
                ConvertTokens(value);
            }
        }

        private void ConvertTokens(string value)
        {
            try
            {
                Trace.TraceInformation(value);
                if (value.Contains("=")) // есть ли символ = если да то это логика для dvr окна
                {
                    //Trace.TraceInformation("есть ли символ =");
                    var camsParams = value.Split(",");
                    foreach (var camParam in camsParams)
                    {
                        var camT = camParam.Split("=");
                        cams.Add(new Cam()
                        {
                            cam_id = camT[0],
                            token = camT[1]
                        });
                    }
                }
                else
                {
                    //Trace.TraceInformation("нет символа =");
                    cams.Add(new Cam()
                    {
                        cam_id = name,
                        token = value
                    });
                }
            }
            catch { }
        }
    }
}
