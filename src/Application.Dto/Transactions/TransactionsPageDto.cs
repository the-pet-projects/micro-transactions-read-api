namespace PetProjects.MicroTransactionsApi.Application.Dto.Transactions
{
    using System.Collections.Generic;

    public class TransactionsPageDto
    {
        public string NextPageToken { get; set; }

        public ICollection<TransactionByIdDto> Data { get; set; }
    }
}