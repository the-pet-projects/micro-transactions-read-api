namespace PetProjects.MicroTransactionsApi.Application.QueryServices.Handlers
{
    using PetProjects.Framework.Cqrs.Queries;
    using PetProjects.MicroTransactionsApi.Domain.Queries;
    using PetProjects.MicroTransactionsApi.Domain.Queries.ReadModels;

    public interface ITransactionsQueriesHandlers : IQueryHandlerAsync<GetTransactionByIdQuery, TransactionByIdReadModel>
    {
    }
}