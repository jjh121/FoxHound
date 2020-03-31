using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;

namespace FoxHound.Web
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            IConfigurationRoot currentConfiguration = GetCurrentConfiguration().Build();
            Log.Logger = CreateLogger(currentConfiguration);

            try
            {
                Log.Information("Starting FoxHound");
                CreateHostBuilder(args).Build().Run();
                Log.Information("FoxHound has stopped");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to start FoxHound");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration(config => config.AddLocalAppSettings());
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog();

        private static IConfigurationBuilder GetCurrentConfiguration()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: false, reloadOnChange: true)
                .AddLocalAppSettings()
                .AddEnvironmentVariables();

            return configuration;
        }

        private static IConfigurationBuilder AddLocalAppSettings(this IConfigurationBuilder builder)
        {
            return builder.AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true);
        }

        private static ILogger CreateLogger(IConfigurationRoot currentConfiguration)
        {
            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(currentConfiguration)
                .CreateLogger();

            return logger;
        }
    }
}