namespace IntegrationTests.Dtos
{
    using System;

    public class TransactionByIdDto
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid ItemId { get; set; }

        public int Quantity { get; set; }
    }
}