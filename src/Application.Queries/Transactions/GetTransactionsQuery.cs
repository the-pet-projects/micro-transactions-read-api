namespace PetProjects.MicroTransactionsApi.Application.Queries.Transactions
{
    using PetProjects.Framework.Cqrs.Queries;
    using PetProjects.MicroTransactionsApi.Application.Dto.Transactions;

    public class GetTransactionsQuery : IQuery<TransactionsPageDto>
    {
        public GetTransactionsQuery(string pageToken)
        {
            this.PageToken = pageToken;
        }

        public string PageToken { get; }
    }
}