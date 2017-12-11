namespace IntegrationTests.Dtos
{
    using System;

    public class TransactionCreationDto
    {
        public Guid UserId { get; set; }

        public Guid ItemId { get; set; }

        public int Quantity { get; set; }
    }
}