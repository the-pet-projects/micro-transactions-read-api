namespace PetProjects.MicroTransactionsApi.Infrastructure.Configuration
{
    using System;
    using System.Collections.Generic;
    using Cassandra;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;
    using PetProjects.Framework.Consul.Store;
    using PetProjects.MicroTransactionsApi.Data.ReadModelRepositories;
    using PetProjects.MicroTransactionsApi.Data.ReadModelRepositories.Transactions;

    internal static class DataDependenciesExtensions
    {
        internal static IServiceCollection ConfigureDataServices(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddTransient<CassandraSettings>(sp =>
                {
                    var store = sp.GetRequiredService<IStringKeyValueStore>();
                    return new CassandraSettings
                    {
                        TransactionsReadConsistencyLevel = (ConsistencyLevel)Enum.Parse(typeof(ConsistencyLevel), store.GetAndConvertValue<string>("CassandraConfiguration/TransactionsReadConsistencyLevel"))
                    };
                })
                .AddSingleton<CassandraConfiguration>(sp =>
                {
                    var store = sp.GetRequiredService<IStringKeyValueStore>();
                    return new CassandraConfiguration
                    {
                        ContactPoints = store.GetAndConvertValue<string>("CassandraConfiguration/ContactPoints").Split(','),
                        Keyspace = store.GetAndConvertValue<string>("CassandraConfiguration/Keyspace"),
                        ReplicationParameters = JsonConvert.DeserializeObject<Dictionary<string, string>>(store.GetAndConvertValue<string>("CassandraConfiguration/ReplicationParameters"))
                    };
                })
                .AddSingleton<IConnection>(sp => ConnectionBuilder.BuildConnection(sp.GetRequiredService<CassandraConfiguration>()))
                .AddTransient<ITransactionsRepository, TransactionsRepository>();
        }
    }
}