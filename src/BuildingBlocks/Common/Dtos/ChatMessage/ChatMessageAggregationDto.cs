namespace Dtos.ChatMessage
{
    using System.Text.Json.Serialization;

    using Enums;

    public class ChatMessageAggregationDto
    {
        [JsonPropertyName("created")]
        public DateTime Created { get; set; }

        [JsonPropertyName("type")]
        public ChatMessageType Type { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }
    }
}
