namespace PetProjects.MicroTransactionsApi.Application.Commands.Transactions
{
    using System;
    using PetProjects.Framework.Cqrs.Commands;
    using PetProjects.MicroTransactionsApi.Application.Dto.Transactions;

    public class CreateTransactionCommand : ICommand<Guid>
    {
        public CreateTransactionCommand(TransactionCreationDto requestDto)
        {
            this.RequestDto = requestDto;
        }

        public TransactionCreationDto RequestDto { get; }
    }
}