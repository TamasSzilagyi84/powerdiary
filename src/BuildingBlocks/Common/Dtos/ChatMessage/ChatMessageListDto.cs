namespace Dtos.ChatMessage
{
    using System.Text.Json.Serialization;

    using Dtos.Enums;

    public class ChatMessageListDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("type")]
        public ChatMessageType Type { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("created")]
        public DateTime Created { get; set; }
    }
}
