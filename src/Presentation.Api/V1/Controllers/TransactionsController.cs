namespace PetProjects.MicroTransactionsApi.Presentation.Api.V1.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using PetProjects.Framework.Cqrs.Mediator;
    using PetProjects.MicroTransactionsApi.Application.Commands.Transactions;
    using PetProjects.MicroTransactionsApi.Application.Dto.Transactions;
    using PetProjects.MicroTransactionsApi.Application.Queries.Transactions;

    [ApiVersion("1")]
    [Route("v{api-version:apiVersion}/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ISimpleMediator mediator;

        public TransactionsController(ISimpleMediator mediator)
        {
            this.mediator = mediator;
        }

        // GET v1/Transactions/5
        [HttpGet("{id:guid}")]
        public async Task<TransactionByIdDto> GetAsync(Guid id)
        {
            var query = new GetTransactionByIdQuery(id);

            return await this.mediator.QueryAsync<GetTransactionByIdQuery, TransactionByIdDto>(query).ConfigureAwait(false);
        }

        // POST v1/Transactions
        [HttpPost] 
        public async Task<Guid> PostAsync(TransactionCreationDto dto)
        {
            var query = new CreateTransactionCommand(dto);

            return await this.mediator.RunCommandAsync<CreateTransactionCommand, Guid>(query).ConfigureAwait(false);
        }

        /// GET v1/Transactions?pageToken=asd1241f
        /// <summary>
        /// Perform a paged query to all transactions.
        /// </summary>
        /// <param name="pageToken">The pageToken that was returned by a previous call to this endpoint.</param>
        /// <returns>A paged result of transactions.</returns>
        /// <response code="200">The successfully retrieved transactions.</response>
        [HttpGet]
        [ProducesResponseType(typeof(TransactionsPageDto), 200)]
        public async Task<IActionResult> GetAsync([FromQuery] string pageToken = null)
        {
            var query = new GetTransactionsQuery(pageToken);

            return this.Ok(await this.mediator.QueryAsync<GetTransactionsQuery, TransactionsPageDto>(query).ConfigureAwait(false));
        }
    }
}