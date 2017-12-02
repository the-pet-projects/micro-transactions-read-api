namespace PetProjects.MicroTransactionsApi.Domain.Queries
{
    using System;
    using PetProjects.Framework.Cqrs.Queries;
    using PetProjects.MicroTransactionsApi.Domain.Queries.ReadModels;

    public class GetTransactionByIdQuery : IQuery<TransactionByIdReadModel>
    {
        public GetTransactionByIdQuery(Guid id)
        {
            this.Id = id;
        }

        public Guid Id { get; }
    }
}