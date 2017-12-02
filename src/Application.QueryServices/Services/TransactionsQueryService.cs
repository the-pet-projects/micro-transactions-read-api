namespace PetProjects.MicroTransactionsApi.Application.QueryServices.Services
{
    using System;
    using System.Threading.Tasks;
    using PetProjects.Framework.Cqrs.Mediator;
    using PetProjects.MicroTransactionsApi.Application.Dto;
    using PetProjects.MicroTransactionsApi.Application.QueryServices.Mappings;
    using PetProjects.MicroTransactionsApi.Domain.Queries;
    using PetProjects.MicroTransactionsApi.Domain.Queries.ReadModels;

    public class TransactionsQueryService : ITransactionsQueryService
    {
        private readonly ISimpleMediator mediator;

        public TransactionsQueryService(ISimpleMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<TransactionByIdDto> GetTransactionByIdAsync(Guid id)
        {
            var query = new GetTransactionByIdQuery(id);

            var result = await this.mediator.QueryAsync<GetTransactionByIdQuery, TransactionByIdReadModel>(query).ConfigureAwait(false);

            return result.ToDto();
        }
    }
}