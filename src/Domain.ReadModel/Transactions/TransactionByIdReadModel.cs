namespace PetProjects.MicroTransactionsApi.Domain.ReadModel.Transactions
{
    using System;

    public class TransactionByIdReadModel
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public long EpochTimestamp { get; set; }

        public int Quantity { get; set; }

        public Guid ItemId { get; set; }
    }
}