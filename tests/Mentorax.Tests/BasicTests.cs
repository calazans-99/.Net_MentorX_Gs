
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Mentorax.Tests
{
    public class BasicTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public BasicTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Get_Health_Returns_OK()
        {
            var client = _factory.CreateClient();
            var response = await client.GetAsync("/health/ready");
            response.StatusCode.Should().BeOneOf(HttpStatusCode.OK, HttpStatusCode.ServiceUnavailable);
        }
    }
}
