namespace PetProjects.MicroTransactionsApi.Application.CommandHandlers.Transactions
{
    using System;
    using System.Threading.Tasks;
    using PetProjects.Framework.Cqrs.Commands;
    using PetProjects.MicroTransactionsApi.Application.Commands.Transactions;

    public class CreateTransactionCommandHandler : ICommandHandlerWithResponseAsync<CreateTransactionCommand, Guid>
    {
        public Task<Guid> HandleAsync(CreateTransactionCommand command)
        {
            throw new NotImplementedException();
        }
    }
}