namespace Dtos.ChatMessage
{
    using Dtos.Enums;

    public class ChatMessageUpdateDto
    {
        public Guid Id { get; set; }

        public ChatMessageType Type { get; set; }

        public string? Message { get; set; }

        public Guid UpdatedById { get; set; }

        public string UpdatedByName { get; set; } = string.Empty;
    }
}
