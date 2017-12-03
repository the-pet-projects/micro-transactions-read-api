namespace PetProjects.MicroTransactionsApi.Application.QueryHandlers.Transactions
{
    using PetProjects.MicroTransactionsApi.Application.Dto;
    using PetProjects.MicroTransactionsApi.Domain.ReadModel.Transactions;

    internal static class TransactionMappings
    {
        public static TransactionByIdDto ToDto(this TransactionByIdReadModel model)
        {
            return new TransactionByIdDto
            {
                Id = model.Id
            };
        }
    }
}