namespace PetProjects.MicroTransactionsApi.Infrastructure.Configuration
{
    using Microsoft.Extensions.DependencyInjection;
    using PetProjects.Framework.Consul.Store;
    using PetProjects.Framework.Kafka.Configurations.Producer;
    using PetProjects.Framework.Kafka.Producer;
    using PetProjects.MicroTransactions.Commands.Transactions.V1;
    using PetProjects.MicroTransactionsApi.Infrastructure.CrossCutting.Bus;

    internal static class InfrastructureDependenciesExtensions
    {
        internal static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddSingleton<IProducerConfiguration>(sp =>
                {
                    var store = sp.GetRequiredService<IStringKeyValueStore>();
                    return new ProducerConfiguration(
                        store.GetAndConvertValue<string>("ApplicationKafkaConfiguration/ClientId"),
                        store.GetAndConvertValue<string>("ApplicationKafkaConfiguration/Brokers"));
                })
                .AddSingleton<EnvironmentContext>(sp =>
                {
                    var store = sp.GetRequiredService<IStringKeyValueStore>();
                    return new EnvironmentContext
                    {
                        Environment = store.GetAndConvertValue<string>("ApplicationKafkaConfiguration/Environment")
                    };
                })
                .AddProducers();
        }

        internal static IServiceCollection AddProducers(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddSingleton<IProducer<TransactionCommandV1>, TransactionCommandProducer>();
        }
    }
}