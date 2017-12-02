namespace PetProjects.MicroTransactionsApi.Application.QueryServices.Mappings
{
    using PetProjects.MicroTransactionsApi.Application.Dto;
    using PetProjects.MicroTransactionsApi.Domain.Queries.ReadModels;

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