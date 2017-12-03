namespace Presentation.Api.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using PetProjects.Framework.Cqrs.Mediator;
    using PetProjects.MicroTransactionsApi.Application.Dto;
    using PetProjects.MicroTransactionsApi.Application.Queries.Transactions;

    [Route("api/[controller]")]
    public class TransactionsController : Controller
    {
        private readonly ISimpleMediator mediator;

        public TransactionsController(ISimpleMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<TransactionByIdDto> GetAsync(Guid id)
        {
            var query = new GetTransactionByIdQuery(id);

            return await this.mediator.QueryAsync<GetTransactionByIdQuery, TransactionByIdDto>(query).ConfigureAwait(false);
        }
    }
}