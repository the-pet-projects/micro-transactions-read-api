namespace PetProjects.MicroTransactionsApi.Application.QueryHandlers.Transactions
{
    using System.Threading.Tasks;
    using PetProjects.Framework.Cqrs.Queries;
    using PetProjects.MicroTransactionsApi.Application.Dto.Transactions;
    using PetProjects.MicroTransactionsApi.Application.Queries.Transactions;

    public class GetTransactionByIdQueryHandler : IQueryHandlerAsync<GetTransactionByIdQuery, TransactionByIdDto>
    {
        public Task<TransactionByIdDto> HandleAsync(GetTransactionByIdQuery query)
        {
            throw new System.NotImplementedException();
        }
    }
}