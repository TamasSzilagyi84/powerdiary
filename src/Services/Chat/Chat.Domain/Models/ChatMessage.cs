namespace Chat.Domain.Models
{
    using Dtos.ChatMessage;
    using Dtos.Enums;

    using Shared.Entity;

    public class ChatMessage : EntityBase, ILogCreatedBy, ILogUpdatedBy
    {
        #region ctor
        
        private ChatMessage() { }

        public ChatMessage(ChatMessageCreateDto createDto)
        {
            this.Type = createDto.Type;
            this.Message = createDto.Type == ChatMessageType.Comment 
                ? createDto.Message 
                : this.GenerateMessageForSystemEvent(
                    createDto.CreatedByName, 
                    createDto.Type);
            this.CreatedById = createDto.CreatedById;
            this.CreatedByName = !string.IsNullOrWhiteSpace(createDto.CreatedByName) 
                ? createDto.CreatedByName 
                : throw new ArgumentNullException(nameof(createDto.CreatedByName));
            this.Created = DateTime.UtcNow;
        }

        #endregion

        public ChatMessageType Type { get; set; }

        public string? Message { get; set; }

        public DateTime Created { get; private set; }

        public Guid CreatedById { get; private set; }

        public string CreatedByName { get; private set; }

        public DateTime? Updated { get; set; }

        public Guid? UpdatedById { get; set; }

        public string? UpdatedByName { get; set; }

        #region methods

        private string GenerateMessageForSystemEvent(string createdByName, ChatMessageType type)
        {
            return type switch
            {
                ChatMessageType.Enter => $"{createdByName} entered the room",
                ChatMessageType.Leave => $"{createdByName} left the room",
                ChatMessageType.HighFive => $"{createdByName} high fived another user",
                _ => throw new ArgumentException($"{type} is not supported to generate a system message")
            };
        }

        #endregion
    }
}
