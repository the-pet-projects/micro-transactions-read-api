namespace PetProjects.MicroTransactionsApi.Presentation.Api.V1.Controllers
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;
    using PetProjects.Framework.Cqrs.Mediator;
    using PetProjects.MicroTransactionsApi.Application.Commands.Transactions;
    using PetProjects.MicroTransactionsApi.Application.Dto;
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
        [HttpGet("{id:guid}", Name = "TransactionsGetById")]
        [ProducesResponseType(typeof(TransactionByIdDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAsync([FromRoute] Guid id)
        {
            var query = new GetTransactionByIdQuery(id);
            var result = await this.mediator.QueryAsync<GetTransactionByIdQuery, TransactionByIdDto>(query).ConfigureAwait(false);
            return result == null ? this.NotFound() : (IActionResult)this.Ok(result);
        }

        // POST v1/Transactions
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> PostAsync([FromBody] TransactionCreationDto dto)
        {
            var query = new CreateTransactionCommand(dto);
            var result = await this.mediator.RunCommandAsync<CreateTransactionCommand, Guid>(query).ConfigureAwait(false);
            return this.AcceptedAtRoute("TransactionsGetById", new RouteValueDictionary { { "id", result }, { "api-version", this.RouteData.Values["api-version"] } });
        }

        /// GET v1/Transactions?pageToken=asd1241f
        /// <summary>
        /// Perform a paged query to all transactions.
        /// </summary>
        /// <param name="pageToken">The pageToken that was returned by a previous call to this endpoint.</param>
        /// <returns>A paged result of transactions.</returns>
        /// <response code="200">The successfully retrieved transactions.</response>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResultDto<TransactionByIdDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAsync([FromQuery] string pageToken = null)
        {
            var query = new GetTransactionsQuery(pageToken);
            var result = await this.mediator.QueryAsync<GetTransactionsQuery, PagedResultDto<TransactionByIdDto>>(query).ConfigureAwait(false);

            return result == null ? this.NotFound() : (IActionResult)this.Ok(result);
        }
    }
}