namespace PetProjects.MicroTransactionsApi.Data.ReadModelRepositories
{
    using Cassandra;

    public class CassandraSettings
    {
        public ConsistencyLevel TransactionsReadConsistencyLevel { get; set; }
    }
}