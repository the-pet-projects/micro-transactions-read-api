namespace PetProjects.MicroTransactionsApi.Application.CommandHandlers.Transactions
{
    using System;
    using PetProjects.Framework.Kafka.Contracts.Utils;
    using PetProjects.MicroTransactions.Commands.Transactions.V1;

    public static class TransactionCommandMappings
    {
        public static CreateTransactionCommand ToMtsCommand(this Commands.Transactions.CreateTransactionCommand cmd, Guid newId)
        {
            return new CreateTransactionCommand
            {
                ItemId = cmd.RequestDto.ItemId,
                Quantity = cmd.RequestDto.Quantity,
                TransactionId = newId,
                UserId = cmd.RequestDto.UserId,
                Timestamp = new Timestamp()
            };
        }
    }
}