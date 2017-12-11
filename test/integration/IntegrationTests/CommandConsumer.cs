namespace IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.Logging.Abstractions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PetProjects.Framework.Kafka.Configurations.Consumer;
    using PetProjects.Framework.Kafka.Consumer;
    using PetProjects.MicroTransactions.Commands.Transactions.V1;

    internal class CommandConsumer : Consumer<TransactionCommandV1>
    {
        private static readonly Lazy<CommandConsumer> LazyConsumer = CommandConsumer.BuildConsumer();

        public static CommandConsumer Consumer => CommandConsumer.LazyConsumer.Value;

        private static Lazy<CommandConsumer> BuildConsumer()
        {
            return new Lazy<CommandConsumer>(() =>
            {
                var consumer = new CommandConsumer();
                consumer.StartConsuming();
                return consumer;
            });
        }

        public CommandConsumer() : base(
            new TransactionCommandsTopicV1(AppSettings.Current.KafkaTopicEnvironment),
            new ConsumerConfiguration(
                "mts-transactions-api-integrationtests",
                "mts-transactions-api-integrationtests",
                AppSettings.Current.KafkaBrokers.Split(',')),
            NullLogger.Instance,
            false)
        {
            this.Receive<CreateTransactionCommand>(this.Handle);
        }

        public IDictionary<Guid, ICollection<CreateTransactionCommand>> ConsumedCommands = new Dictionary<Guid, ICollection<CreateTransactionCommand>>();

        public CreateTransactionCommand GetConsumedCommand(Guid transactionId)
        {
            Assert.IsTrue(this.ConsumedCommands.ContainsKey(transactionId), $"CommandId {transactionId} has not been consumed.");
            var cmds = this.ConsumedCommands[transactionId];
            Assert.AreEqual(1, cmds.Count);
            return cmds.Single();
        }

        private void Handle(CreateTransactionCommand cmd)
        {
            if (!this.ConsumedCommands.TryGetValue(cmd.TransactionId, out var cmdList))
            {
                cmdList = new List<CreateTransactionCommand>();
                this.ConsumedCommands.Add(cmd.TransactionId, cmdList);
            }

            cmdList.Add(cmd);
        }
    }
}