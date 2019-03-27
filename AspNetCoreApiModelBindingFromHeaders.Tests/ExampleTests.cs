using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace AspNetCoreApiModelBindingFromHeaders.Tests
{
    public class ExampleTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Startup> _factory;

        public ExampleTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;

            _client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task SingleMixedComplexType_Maps_From_Header_And_Body()
        {
            _client.DefaultRequestHeaders.Add("HeaderName", "HeaderValue");

            var request = new { bodyName = "BodyValue" };

            var responseMessage = await _client.PostAsJsonAsync("api/example/single-complex-type-body-and-header", request);
            var result = await responseMessage.Content.ReadAsAsync<Mixed>();

            Assert.Equal("HeaderValue", result.HeaderName);
            Assert.Equal("BodyValue", result.BodyName);
        }

        [Fact]
        public async Task SeparateComplexTypes_MapsCorrectly()
        {
            _client.DefaultRequestHeaders.Add("HeaderName", "HeaderValue");

            var request = new { bodyName = "BodyValue" };

            var responseMessage = await _client.PostAsJsonAsync("api/example/separate-complex-type-for-body-and-header", request);
            var result = await responseMessage.Content.ReadAsAsync<SeparateTypesResponse>();

            Assert.Equal("HeaderValue", result.HeaderOnly.HeaderName);
            Assert.Equal("BodyValue", result.BodyOnly.BodyName);
        }
    }
}
