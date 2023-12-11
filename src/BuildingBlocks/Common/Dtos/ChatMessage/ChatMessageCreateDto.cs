namespace Dtos.ChatMessage
{
    using Enums;

    public class ChatMessageCreateDto
    {
        public ChatMessageType Type { get; set; }

        public string? Message { get; set; }

        public Guid CreatedById { get; set; }

        public string CreatedByName { get; set; } = string.Empty;
    }
}
