using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WS_Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureLogging(logging =>
                    {
                        /* loggerFactory.AddFile("Logs/app-{Date}.txt");*/
                        //Тоже самое что и в appsettings json
                        /*logging.AddFilter("Microsoft.AspNetCore.SignalR", LogLevel.Debug);*/
                        /*logging.AddFilter("Microsoft.AspNetCore.Http.Connections", LogLevel.Debug);*/
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
