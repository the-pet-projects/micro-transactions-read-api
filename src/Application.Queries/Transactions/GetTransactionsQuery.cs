namespace PetProjects.MicroTransactionsApi.Application.Queries.Transactions
{
    using PetProjects.Framework.Cqrs.Queries;
    using PetProjects.MicroTransactionsApi.Application.Dto;
    using PetProjects.MicroTransactionsApi.Application.Dto.Transactions;

    public class GetTransactionsQuery : IQuery<PagedResultDto<TransactionByIdDto>>
    {
        public const int DefaultPageSize = 10;

        public GetTransactionsQuery(string pageToken, int pageSize = DefaultPageSize)
        {
            this.PageToken = pageToken;
            this.PageSize = pageSize;
        }

        public string PageToken { get; }

        public int PageSize { get; }
    }
}