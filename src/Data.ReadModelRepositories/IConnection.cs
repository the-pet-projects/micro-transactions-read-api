namespace PetProjects.MicroTransactionsApi.Data.ReadModelRepositories
{
    using System;
    using Cassandra;
    using Cassandra.Mapping;

    public interface IConnection : IDisposable
    {
        ISession Session { get; }

        IMapper Mapper { get; }
    }
}