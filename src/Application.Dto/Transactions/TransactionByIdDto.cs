namespace PetProjects.MicroTransactionsApi.Application.Dto.Transactions
{
    using System;
    using System.Collections.Generic;

    public class TransactionByIdDto
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public ICollection<ItemQuantity> Items { get; set; }
    }
}