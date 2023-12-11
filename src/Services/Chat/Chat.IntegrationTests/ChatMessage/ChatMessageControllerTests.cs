namespace Chat.IntegrationTests.ChatMessage
{
    using Application;

    using Helpers;

    using Shared;

    public class ChatMessageControllerTests
        : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;

        public ChatMessageControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            this._factory = factory;
        }

        [Theory]
        [InlineData("/api/ChatMessages", "Create")]
        [InlineData("/api/ChatMessages", "Update")]
        [InlineData("/api/ChatMessages", "GetById")]
        [InlineData("/api/ChatMessages?page=0&take=10", "List")]
        [InlineData("/api/ChatMessages/statistics", "Statistics")]
        [InlineData("/api/ChatMessages", "Delete")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url, string endpoint)
        {
            // Arrange
            HttpClient client = this._factory.CreateClient();

            // Act
            HttpResponseMessage? response = endpoint switch
            {
                "Create" => await client.PostAsync(url,
                    ObjectHelper.GenerateHttpContent(ObjectHelper.CreateChatMessageCreateDto())),
                "Update" => await client.PutAsync($"{url}/{Guid.NewGuid()}",
                    ObjectHelper.GenerateHttpContent(ObjectHelper.CreateChatMessageUpdateDto())),
                "GetById" => await client.GetAsync($"{url}/{Const.TestData.ChatMessageId}"),
                "List" => await client.GetAsync($"{url}"),
                "Statistics" => await client.GetAsync($"{url}"),
                "Delete" => await client.DeleteAsync($"{url}/{Const.TestData.SecondChatMessageId}"),
                _ => null
            };

            // Assert
            Assert.NotNull(response);
            response.EnsureSuccessStatusCode();
        }
    }
}
