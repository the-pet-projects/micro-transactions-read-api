namespace PetProjects.MicroTransactionsApi.Application.CommandHandlers.Transactions
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using PetProjects.Framework.Cqrs.Commands;
    using PetProjects.Framework.Kafka.Producer;
    using PetProjects.MicroTransactions.Commands.Transactions.V1;

    using CreateTransactionCommand = PetProjects.MicroTransactionsApi.Application.Commands.Transactions.CreateTransactionCommand;

    public class CreateTransactionCommandHandler : ICommandHandlerWithResponseAsync<CreateTransactionCommand, Guid>
    {
        private readonly IProducer<TransactionCommandV1> producer;
        private readonly ILogger<CreateTransactionCommandHandler> logger;

        public CreateTransactionCommandHandler(IProducer<TransactionCommandV1> producer, ILogger<CreateTransactionCommandHandler> logger)
        {
            this.producer = producer;
            this.logger = logger;
        }

        public async Task<Guid> HandleAsync(CreateTransactionCommand command)
        {
            var cmd = command.ToMtsCommand(Guid.NewGuid());
            this.logger.LogTrace("Generated new command: {command}", cmd);

            await this.producer.ProduceAsync(cmd).ConfigureAwait(false);
            this.logger.LogTrace("Invoked Produce for command: {command}", cmd);

            return cmd.TransactionId;
        }
    }
}