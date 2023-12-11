namespace Chat.Infrastructure
{
    using Chat.Domain.Models;

    using Dtos.ChatMessage;
    using Dtos.Enums;

    using Shared;

    public static class TestDbInitializer
    {
        public static async Task Initialize(ChatMessageContext context)
        {
            if (context.ChatMessages.Any())
            {
                return;   // DB has been seeded
            }

            List<ChatMessage> chatMessages = new ();

            ChatMessage bobEnters = new (new ChatMessageCreateDto
            {
                CreatedById = Guid.NewGuid(),
                CreatedByName = "Bob",
                Type = ChatMessageType.Enter
            });
            bobEnters.Id = Const.TestData.ChatMessageId;

            chatMessages.Add(bobEnters);

            ChatMessage kateEnters = new (new ChatMessageCreateDto
            {
                CreatedById = Guid.NewGuid(),
                CreatedByName = "Kate",
                Type = ChatMessageType.Enter
            });
            kateEnters.Id = Const.TestData.SecondChatMessageId;

            chatMessages.Add(kateEnters);

            ChatMessage bobComment = new (new ChatMessageCreateDto
            {
                CreatedById = Guid.NewGuid(),
                CreatedByName = "Bob",
                Type = ChatMessageType.Comment,
                Message = "Hey Kate, high five?"
            });

            chatMessages.Add(bobComment);

            ChatMessage kateHighFive = new (new ChatMessageCreateDto
            {
                CreatedById = Guid.NewGuid(),
                CreatedByName = "Kate",
                Type = ChatMessageType.HighFive
            });

            chatMessages.Add(kateHighFive);

            ChatMessage bobLeave = new (new ChatMessageCreateDto
            {
                CreatedById = Guid.NewGuid(),
                CreatedByName = "Bob",
                Type = ChatMessageType.Leave
            });

            chatMessages.Add(bobLeave);

            ChatMessage kateComment = new (new ChatMessageCreateDto
            {
                CreatedById = Guid.NewGuid(),
                CreatedByName = "Kate",
                Type = ChatMessageType.Comment,
                Message = "Typical :D"
            });

            chatMessages.Add(kateComment);

            ChatMessage kateLeave = new (new ChatMessageCreateDto
            {
                CreatedById = Guid.NewGuid(),
                CreatedByName = "Kate",
                Type = ChatMessageType.Leave
            });

            chatMessages.Add(kateLeave);

            context.ChatMessages.AddRange(chatMessages);
            await context.SaveChangesAsync();
        }
    }
}
