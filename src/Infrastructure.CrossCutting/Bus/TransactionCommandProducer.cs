namespace PetProjects.MicroTransactionsApi.Infrastructure.CrossCutting.Bus
{
    using PetProjects.Framework.Kafka.Configurations.Producer;
    using PetProjects.Framework.Kafka.Producer;
    using PetProjects.MicroTransactions.Commands.Transactions.V1;

    public class TransactionCommandProducer : Producer<TransactionCommandV1>
    {
        public TransactionCommandProducer(EnvironmentContext env, IProducerConfiguration configuration) : base(new TransactionCommandsTopicV1(env.Environment), configuration)
        {
        }
    }
}