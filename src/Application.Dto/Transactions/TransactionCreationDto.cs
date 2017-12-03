namespace PetProjects.MicroTransactionsApi.Application.Dto.Transactions
{
    using System;
    using System.Collections.Generic;

    public class TransactionCreationDto
    {
        public Guid UserId { get; set; }

        public ICollection<ItemQuantity> Items { get; set; }
    }
}