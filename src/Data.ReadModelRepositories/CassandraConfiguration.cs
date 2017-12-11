namespace PetProjects.MicroTransactionsApi.Data.ReadModelRepositories
{
    using System.Collections.Generic;

    public class CassandraConfiguration
    {
        public string[] ContactPoints { get; set; }

        public string Keyspace { get; set; }

        public Dictionary<string, string> ReplicationParameters { get; set; }
    }
}