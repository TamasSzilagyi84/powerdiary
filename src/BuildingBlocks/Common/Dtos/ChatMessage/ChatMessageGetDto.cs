namespace Dtos.ChatMessage
{
    using System.Text.Json.Serialization;

    using Dtos.Enums;

    public class ChatMessageGetDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("type")]
        public ChatMessageType Type { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("created")]
        public DateTime Created { get; set; }

        [JsonPropertyName("createdById")]
        public Guid CreatedById { get; set; }

        [JsonPropertyName("createdByName")] 
        public string CreatedByName { get; set; }

        [JsonPropertyName("updated")]
        public DateTime? Updated { get; set; }

        [JsonPropertyName("updatedById")]
        public Guid? UpdatedById { get; set; }

        [JsonPropertyName("updatedByName")]
        public string? UpdatedByName { get; set; }
    }
}
