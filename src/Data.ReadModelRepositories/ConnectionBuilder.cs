namespace PetProjects.MicroTransactionsApi.Data.ReadModelRepositories
{
    using Cassandra;
    using Cassandra.Data.Linq;
    using Cassandra.Mapping;
    using PetProjects.MicroTransactionsApi.Data.ReadModelRepositories.Transactions;
    using PetProjects.MicroTransactionsApi.Domain.ReadModel.Transactions;

    public static class ConnectionBuilder
    {
        public static IConnection BuildConnection(CassandraConfiguration config)
        {
            var cluster = Cluster.Builder()
                .WithDefaultKeyspace(config.Keyspace)
                .AddContactPoints(config.ContactPoints)
                .Build();

            var mapConfig = new MappingConfiguration();
            mapConfig.Define<TransactionMappings>();

            var session = cluster.ConnectAndCreateDefaultKeyspaceIfNotExists(config.ReplicationParameters);

            var table = new Table<TransactionByIdReadModel>(session, mapConfig);
            table.CreateIfNotExists();

            return new Connection(cluster, session, mapConfig);
        }
    }
}