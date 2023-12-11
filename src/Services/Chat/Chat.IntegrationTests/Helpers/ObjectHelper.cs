namespace Chat.IntegrationTests.Helpers
{
    using System.Net.Http.Json;
    using System.Text.Json;

    using Domain.Models;

    using Dtos.ChatMessage;
    using Dtos.Enums;
    using Shared;

    internal static class ObjectHelper
    {
        internal static ChatMessageCreateDto CreateChatMessageCreateDto()
        {
            return new ChatMessageCreateDto
            {
                CreatedById = Guid.NewGuid(),
                CreatedByName = "createdByName",
                Type = ChatMessageType.Enter
            };
        }

        internal static ChatMessageUpdateDto CreateChatMessageUpdateDto()
        {
            return new ChatMessageUpdateDto
            {
                Id = Const.TestData.ChatMessageId,
                UpdatedById = Guid.NewGuid(),
                UpdatedByName = "UpdatedByName",
                Type = ChatMessageType.Leave
            };
        }

        internal static HttpContent GenerateHttpContent(object obj)
        {
            return JsonContent.Create(obj);
        }
    }
}
