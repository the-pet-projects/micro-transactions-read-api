namespace IntegrationTests
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Cassandra;
    using IntegrationTests.Dtos;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;
    using PetProjects.Framework.Kafka.Contracts.Utils;

    [TestClass]
    public class TransactionGetTests
    {
        [TestMethod]
        public async Task GetTransactionById_IdExists_ReturnsTransactionDto()
        {
            // Arrange
            var expectedDto = await this.GenerateTransactionAsync(true).ConfigureAwait(false);

            using (var httpClient = new HttpClient())
            {
                // Act
                var act = await this.GetTransactionByIdAsync(httpClient, expectedDto.Id).ConfigureAwait(false);
                var result = JsonConvert.DeserializeObject<TransactionByIdDto>(await act.Content.ReadAsStringAsync().ConfigureAwait(false));

                // Assert
                AssertTransaction(expectedDto, result);
            }
        }

        [TestMethod]
        public async Task GetTransactionById_IdExists_ReturnsOk()
        {
            // Arrange
            var expectedDto = await this.GenerateTransactionAsync(true).ConfigureAwait(false);

            using (var httpClient = new HttpClient())
            {
                // Act
                var act = await this.GetTransactionByIdAsync(httpClient, expectedDto.Id).ConfigureAwait(false);

                // Assert
                Assert.AreEqual(HttpStatusCode.OK, act.StatusCode);
            }
        }

        [TestMethod]
        public async Task GetTransactionById_IdDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            var expectedDto = await this.GenerateTransactionAsync(false).ConfigureAwait(false);

            using (var httpClient = new HttpClient())
            {
                // Act
                var act = await this.GetTransactionByIdAsync(httpClient, expectedDto.Id).ConfigureAwait(false);

                // Assert
                Assert.AreEqual(HttpStatusCode.NotFound, act.StatusCode);
            }
        }

        private Task<HttpResponseMessage> GetTransactionByIdAsync(HttpClient client, Guid id)
        {
            return client.GetAsync($"{ AppSettings.Current.ApiEndpoint }/v1/Transactions/{ id.ToString() }");
        }

        private async Task<TransactionByIdDto> GenerateTransactionAsync(bool insertInDatabase)
        {
            var transactionId = Utils.GenerateTransactionId();
            var itemId = Guid.NewGuid();
            var quantity = 2;
            var timestamp = new Timestamp().UnixTimeEpochTimestamp;
            var userId = Guid.NewGuid();

            if (insertInDatabase)
            {
                var st = new SimpleStatement(
                    "INSERT INTO transactions(transaction_id, item_id, quantity, timestamp, user_id) VALUES(?, ?, ?, ?, ?); ",
                    transactionId,
                    itemId,
                    quantity,
                    timestamp,
                    userId).SetConsistencyLevel(ConsistencyLevel.LocalQuorum);
                await CassandraConnection.Session.ExecuteAsync(st).ConfigureAwait(false);
            }

            return new TransactionByIdDto
            {
                Id = transactionId,
                UserId = userId,
                Quantity = quantity,
                ItemId = itemId
            };
        }

        private static void AssertTransaction(TransactionByIdDto expected, TransactionByIdDto act)
        {
            Assert.AreEqual(expected.Id, act.Id);
            Assert.AreEqual(expected.UserId, act.UserId);
            Assert.AreEqual(expected.Quantity, act.Quantity);
            Assert.AreEqual(expected.ItemId, act.ItemId);
        }
    }
}