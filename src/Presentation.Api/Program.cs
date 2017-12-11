namespace PetProjects.MicroTransactionsApi.Presentation.Api
{
    using System.IO;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using PetProjects.MicroTransactionsApi.Infrastructure.Configuration;

    public class Program
    {
        public static void Main(string[] args)
        {
            Program.BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", false, false);
                    config.AddEnvironmentVariables("MTS_APP_SETTINGS_");
                    if (args == null)
                    {
                        return;
                    }

                    config.AddCommandLine(args);
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.SetupLogging(DependencyInjectionExtensions.GetConfigurationKeyValueStore(hostingContext.Configuration));
                })
                .UseDefaultServiceProvider((context, options) => options.ValidateScopes = context.HostingEnvironment.IsDevelopment())
                .UseStartup<Startup>()
                .Build();
        }
    }
}