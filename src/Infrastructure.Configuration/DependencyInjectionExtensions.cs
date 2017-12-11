namespace PetProjects.MicroTransactionsApi.Infrastructure.Configuration
{
    using System;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using Microsoft.Extensions.Logging;
    using PetProjects.Framework.Consul;
    using PetProjects.Framework.Consul.Store;
    using PetProjects.Framework.Logging.Producer;
    using Serilog.Events;

    public static class DependencyInjectionExtensions
    {
        public static IStringKeyValueStore GetConfigurationKeyValueStore(IConfiguration configuration)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddPetProjectConsulServices(configuration, true);
            serviceCollection.AddLogging(builder => builder.AddConsole());
            serviceCollection.TryAddSingleton<ILogger>(sp => sp.GetRequiredService<ILoggerFactory>().CreateLogger("No category"));

            using (var tempProvider = serviceCollection.BuildServiceProvider())
            {
                using (var scopedProvider = tempProvider.CreateScope())
                {
                    return scopedProvider.ServiceProvider.GetRequiredService<IStringKeyValueStore>();
                }
            }
        }

        public static ILoggingBuilder SetupLogging(this ILoggingBuilder loggingBuilder, IStringKeyValueStore configStore)
        {
            var kafkaConfig = new KafkaConfiguration
            {
                Brokers = configStore.GetAndConvertValue<string>("Logging/KafkaConfiguration/Brokers").Split(','),
                Topic = configStore.GetAndConvertValue<string>("Logging/KafkaConfiguration/Topic")
            };

            var sinkConfig = new PeriodicSinkConfiguration
            {
                BatchSizeLimit = configStore.GetAndConvertValue<int>("Logging/BatchSizeLimit"),
                Period = TimeSpan.FromMilliseconds(configStore.GetAndConvertValue<int>("Logging/PeriodMs"))
            };

            var logLevel = configStore.GetAndConvertValue<LogEventLevel>("Logging/LogLevel");
            var logType = configStore.GetAndConvertValue<string>("Logging/LogType");

            return loggingBuilder.AddPetProjectLogging(logLevel, sinkConfig, kafkaConfig, logType, true).AddConsole().AddDebug();
        }

        public static IServiceCollection ConfigureDependencies(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.TryAddSingleton<ILogger>(sp => sp.GetRequiredService<ILoggerFactory>().CreateLogger("No category"));
            serviceCollection.AddPetProjectConsulServices(configuration, true);

            return serviceCollection
                .ConfigureApplicationServices()
                .ConfigureDataServices()
                .ConfigureInfrastructureServices();
        }
    }
}