
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;

namespace WebApplicationLibrary
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .UseSerilog((hostingContext, loggerConfiguration) =>
        {
            loggerConfiguration
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console(new CompactJsonFormatter())
                .WriteTo.File(new CompactJsonFormatter(), "logs/serilog.json", rollingInterval: RollingInterval.Day)
                .ReadFrom.Configuration(hostingContext.Configuration);
        })
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
            webBuilder.UseUrls("http://localhost:5000", "https://localhost:5001");
        });
    }
}
