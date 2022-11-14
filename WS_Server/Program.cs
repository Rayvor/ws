using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using WS_Server.Core.Models;
using WS_Server.Data;
using WS_Server.Services;

namespace WS_Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var app = CreateWebApplication(args);

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseCors(config => config
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin());

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });

            app.Run();
        }

        public static WebApplication CreateWebApplication(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages();
            builder.Services.AddSignalR();
            builder.Services.AddCors();

            builder.Services.AddScoped<DbContext>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<ForecastQuery>();

            return builder.Build();
        }
    }
}
