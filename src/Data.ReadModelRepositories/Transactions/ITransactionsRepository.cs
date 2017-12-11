namespace PetProjects.MicroTransactionsApi.Data.ReadModelRepositories.Transactions
{
    using System;
    using System.Threading.Tasks;
    using PetProjects.MicroTransactionsApi.Domain.ReadModel.Transactions;
    using PetProjects.MicroTransactionsApi.Infrastructure.CrossCutting.Paging;

    public interface ITransactionsRepository
    {
        Task<TransactionByIdReadModel> GetAsync(Guid transactionId);

        Task<PagedResult<TransactionByIdReadModel>> GetPageAsync(int pageSize, string pageToken = null);
    }
}