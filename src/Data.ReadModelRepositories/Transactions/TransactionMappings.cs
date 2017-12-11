namespace PetProjects.MicroTransactionsApi.Data.ReadModelRepositories.Transactions
{
    using Cassandra.Mapping;
    using PetProjects.MicroTransactionsApi.Domain.ReadModel.Transactions;

    public class TransactionMappings : Mappings
    {
        public TransactionMappings()
        {
            this.For<TransactionByIdReadModel>()
                .TableName("transactions")
                .PartitionKey(t => t.Id)
                .Column(t => t.Id, cfg => cfg.WithName("transaction_id"))
                .Column(t => t.EpochTimestamp, cfg => cfg.WithName("timestamp"))
                .Column(t => t.UserId, cfg => cfg.WithName("user_id"))
                .Column(t => t.ItemId, cfg => cfg.WithName("item_id"))
                .Column(t => t.Quantity, cfg => cfg.WithName("quantity"))
                .ExplicitColumns();
        }
    }
}