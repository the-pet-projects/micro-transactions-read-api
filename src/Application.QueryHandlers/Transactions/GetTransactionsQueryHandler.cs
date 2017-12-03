namespace PetProjects.MicroTransactionsApi.Application.QueryHandlers.Transactions
{
    using System.Threading.Tasks;
    using PetProjects.Framework.Cqrs.Queries;
    using PetProjects.MicroTransactionsApi.Application.Dto.Transactions;
    using PetProjects.MicroTransactionsApi.Application.Queries.Transactions;

    public class GetTransactionsQueryHandler : IQueryHandlerAsync<GetTransactionsQuery, TransactionsPageDto>
    {
        public Task<TransactionsPageDto> HandleAsync(GetTransactionsQuery query)
        {
            throw new System.NotImplementedException();
        }
    }
}