namespace PetProjects.MicroTransactionsApi.Application.QueryHandlers.Transactions
{
    using System.Threading.Tasks;
    using PetProjects.Framework.Cqrs.Queries;
    using PetProjects.MicroTransactionsApi.Application.Dto.Transactions;
    using PetProjects.MicroTransactionsApi.Application.Queries.Transactions;
    using PetProjects.MicroTransactionsApi.Data.ReadModelRepositories.Transactions;

    public class GetTransactionByIdQueryHandler : IQueryHandlerAsync<GetTransactionByIdQuery, TransactionByIdDto>
    {
        private readonly ITransactionsRepository repo;

        public GetTransactionByIdQueryHandler(ITransactionsRepository repo)
        {
            this.repo = repo;
        }

        public async Task<TransactionByIdDto> HandleAsync(GetTransactionByIdQuery query)
        {
            var transaction = await this.repo.GetAsync(query.Id).ConfigureAwait(false);

            return transaction.ToDto();
        }
    }
}