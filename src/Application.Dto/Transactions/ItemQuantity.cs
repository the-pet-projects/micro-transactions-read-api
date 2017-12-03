namespace PetProjects.MicroTransactionsApi.Application.Dto.Transactions
{
    using System;

    public class ItemQuantity
    {
        public Guid ItemId { get; set; }

        public int Quantity { get; set; }
    }
}