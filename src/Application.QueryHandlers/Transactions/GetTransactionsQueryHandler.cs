namespace PetProjects.MicroTransactionsApi.Application.QueryHandlers.Transactions
{
    using System.Threading.Tasks;
    using PetProjects.Framework.Cqrs.Queries;
    using PetProjects.MicroTransactionsApi.Application.Dto;
    using PetProjects.MicroTransactionsApi.Application.Dto.Transactions;
    using PetProjects.MicroTransactionsApi.Application.Queries;
    using PetProjects.MicroTransactionsApi.Application.Queries.Transactions;
    using PetProjects.MicroTransactionsApi.Data.ReadModelRepositories.Transactions;

    public class GetTransactionsQueryHandler : IQueryHandlerAsync<GetTransactionsQuery, PagedResultDto<TransactionByIdDto>>
    {
        private readonly ITransactionsRepository repo;

        public GetTransactionsQueryHandler(ITransactionsRepository repo)
        {
            this.repo = repo;
        }

        public async Task<PagedResultDto<TransactionByIdDto>> HandleAsync(GetTransactionsQuery query)
        {
            var result = await this.repo.GetPageAsync(query.PageSize, query.PageToken).ConfigureAwait(false);
            return result.ToPagedResultDto(model => model.ToDto());
        }
    }
}