namespace Dtos.ChatMessage
{
    using Enums;

    public class ChatMessageAggregationRequestDto
    {
        public int Page { get; set; }

        public int Take { get; set; }

        public ChatMessageAggregationType AggregationType { get; set; }
    }
}
