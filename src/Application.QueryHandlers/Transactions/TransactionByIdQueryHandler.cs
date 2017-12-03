namespace PetProjects.MicroTransactionsApi.Application.QueryHandlers.Transactions
{
    using System.Threading.Tasks;
    using PetProjects.Framework.Cqrs.Queries;
    using PetProjects.MicroTransactionsApi.Application.Dto;
    using PetProjects.MicroTransactionsApi.Application.Queries.Transactions;

    public class TransactionByIdQueryHandler : IQueryHandlerAsync<GetTransactionByIdQuery, TransactionByIdDto>
    {
        public Task<TransactionByIdDto> HandleAsync(GetTransactionByIdQuery query)
        {
            throw new System.NotImplementedException();
        }
    }
}