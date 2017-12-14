namespace IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;

    [TestClass]
    public class SampleGetTests
    {
        [TestMethod]
        public async Task GetSampleTest_Key_ReturnsKeyId()
        {
            // Arrange
            const string key = "mysamplekey";

            using (var httpClient = new HttpClient())
            {
                // Act
                var act = await httpClient.GetAsync($"{AppSettings.Current.ApiEndpoint}/v1/samples/{key}").ConfigureAwait(false);
                var result = JsonConvert.DeserializeObject<Dictionary<string, Guid>>(await act.Content.ReadAsStringAsync().ConfigureAwait(false));

                // Assert
                Assert.AreEqual(HttpStatusCode.OK, act.StatusCode);
                Assert.IsNotNull(act.Content);
                Assert.IsInstanceOfType(result[key], typeof(Guid));
            }
        }
    }
}