namespace PetProjects.MicroTransactionsApi.Application.Queries.Transactions
{
    using System;
    using PetProjects.Framework.Cqrs.Queries;
    using PetProjects.MicroTransactionsApi.Application.Dto;

    public class GetTransactionByIdQuery : IQuery<TransactionByIdDto>
    {
        public GetTransactionByIdQuery(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; }
    }
}