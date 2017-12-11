namespace IntegrationTests
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using IntegrationTests.Dtos;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;
    using PetProjects.MicroTransactions.Commands.Transactions.V1;

    [TestClass]
    public class TransactionPostTests
    {
        [TestMethod]
        public async Task PostTransaction_ValidRequest_ConsumerReceivesCommand()
        {
            using (var httpClient = new HttpClient())
            {
                // Arrange
                var dto = this.GenerateTransactionCreationDto();
                
                // Act
                var act = await this.PostTransactionAsync(httpClient, dto).ConfigureAwait(false);

                // Assert
                Assert.AreEqual(HttpStatusCode.Accepted, act.StatusCode);
                var newId = Guid.Parse(act.Headers.Location.Segments.Last());
                Utils.AddTransactionToCleanup(newId);

                var actCmd = await Utils.AssertWithRetry<CreateTransactionCommand>(() =>
                {
                    var cmd = CommandConsumer.Consumer.GetConsumedCommand(newId);
                    Assert.IsNotNull(cmd);
                    return Task.FromResult(cmd);
                });

                AssertCreateTransactionCommand(dto, newId, actCmd);
            }
        }

        [TestMethod]
        public async Task PostTransaction_ValidRequest_ReturnsAccepted()
        {
            using (var httpClient = new HttpClient())
            {
                // Arrange
                var dto = this.GenerateTransactionCreationDto();

                // Act
                var act = await this.PostTransactionAsync(httpClient, dto).ConfigureAwait(false);

                // Assert
                Assert.AreEqual(HttpStatusCode.Accepted, act.StatusCode);

                // Cleanup
                var newId = Guid.Parse(act.Headers.Location.Segments.Last());
                Utils.AddTransactionToCleanup(newId);
            }
        }

        private static void AssertCreateTransactionCommand(TransactionCreationDto expected, Guid expectedId, CreateTransactionCommand act)
        {
            Assert.AreEqual(expectedId, act.TransactionId);
            Assert.AreEqual(expected.UserId, act.UserId);
            Assert.AreEqual(expected.Quantity, act.Quantity);
            Assert.AreEqual(expected.ItemId, act.ItemId);
        }

        private Task<HttpResponseMessage> PostTransactionAsync(HttpClient client, TransactionCreationDto dto)
        {
            var content = new StringContent(JsonConvert.SerializeObject(dto), Encoding.UTF8, "application/json");

            return client.PostAsync($"{ AppSettings.Current.ApiEndpoint }/v1/Transactions", content);
        }

        private TransactionCreationDto GenerateTransactionCreationDto()
        {
            var itemId = Guid.NewGuid();
            var quantity = 2;
            var userId = Guid.NewGuid();

            return new TransactionCreationDto
            {
                UserId = userId,
                Quantity = quantity,
                ItemId = itemId
            };
        }
    }
}