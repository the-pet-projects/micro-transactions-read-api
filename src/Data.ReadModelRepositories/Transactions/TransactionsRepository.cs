namespace PetProjects.MicroTransactionsApi.Data.ReadModelRepositories.Transactions
{
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using Cassandra.Mapping;
    using PetProjects.MicroTransactionsApi.Domain.ReadModel.Transactions;
    using PetProjects.MicroTransactionsApi.Infrastructure.CrossCutting.Paging;

    public class TransactionsRepository : ITransactionsRepository
    {
        private readonly IConnection connection;
        private readonly CassandraSettings settings;

        public TransactionsRepository(IConnection connection, CassandraSettings settings)
        {
            this.connection = connection;
            this.settings = settings;
        }

        public Task<TransactionByIdReadModel> GetAsync(Guid transactionId)
        {
            return this.connection.Mapper.FirstOrDefaultAsync<TransactionByIdReadModel>(Cql
                .New("WHERE transaction_id = ?", transactionId)
                .WithOptions(opt => opt.SetConsistencyLevel(this.settings.TransactionsReadConsistencyLevel)));
        }

        public async Task<PagedResult<TransactionByIdReadModel>> GetPageAsync(int pageSize, string pageToken = null)
        {
            var page = await this.connection.Mapper.FetchPageAsync<TransactionByIdReadModel>(
                pageSize, 
                pageToken == null ? null : Encoding.UTF8.GetBytes(pageToken), 
                string.Empty, 
                new object[0]).ConfigureAwait(false);

            return new PagedResult<TransactionByIdReadModel>(page.PagingState, page);
        }
    }
}