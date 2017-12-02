namespace PetProjects.MicroTransactionsApi.Application.QueryServices.Services
{
    using System;
    using System.Threading.Tasks;
    using PetProjects.MicroTransactionsApi.Application.Dto;

    public interface ITransactionsQueryService
    {
        Task<TransactionByIdDto> GetTransactionByIdAsync(Guid id);
    }
}