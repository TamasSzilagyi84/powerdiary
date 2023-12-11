using System.Reflection;

using Chat.Domain.Models;
using Chat.Infrastructure;

using Dtos.ChatMessage;
using Dtos.Enums;

using Microsoft.EntityFrameworkCore;

Console.WriteLine($"Migration started at {DateTime.Now}");

await using ChatMessageContext context = new (new DbContextOptionsBuilder<ChatMessageContext>()
    .UseSqlServer("Data Source=host.docker.internal,5434;Initial Catalog=Chat;User ID=SA;TrustServerCertificate=True;User Id=SA;Password=Pass@word;")
    .Options);

Console.WriteLine("DbContext successfully created");

List<ChatMessage> chatMessages = new ();

ChatMessageType[] types = new[]
{
        ChatMessageType.Enter,
        ChatMessageType.Leave,
        ChatMessageType.HighFive,
        ChatMessageType.Comment
    };

int lowerBound = 0;
int upperBound = 4;
Random random = new();


for (int i = 0; i < 10000000; i++)
{
    int randomNumber = random.Next(lowerBound, upperBound);

    ChatMessage chatMessage = new(new ChatMessageCreateDto
    {
        CreatedById = Guid.NewGuid(),
        CreatedByName = "Test user",
        Type = types[randomNumber],
        Message = randomNumber == 3 ? $"Random message text {i}" : null
    });

    chatMessages.Add(chatMessage);
}

Console.WriteLine("Chat messages has been generated");

DateTime dateTimeOffset = DateTime.UtcNow;

for (int i = 0; i < 10000000; i++)
{
    ChatMessage chatMessage = chatMessages[i];
    SetPrivateDateTimePropertyValue(chatMessage, nameof(ChatMessage.Created), dateTimeOffset.AddSeconds(-i));
}

Console.WriteLine("All date time properties has been updated");

for (int i = 1; i <= 1000; i++)
{
    // EF Core is way too slow to do a bulk 1M insert
    IEnumerable<ChatMessage> tempChatMessage = chatMessages.Skip((i - 1) * 10000).Take(10000);
    await context.ChatMessages.AddRangeAsync(tempChatMessage);
    await context.SaveChangesAsync();
    context.ChangeTracker.Clear();
    Console.WriteLine($"{i * 10000} has been saved");
}

Console.WriteLine($"Migration finished at {DateTime.Now}");

void SetPrivateDateTimePropertyValue(ChatMessage member, string propName, DateTime newValue)
{
    PropertyInfo? propertyInfo = typeof(ChatMessage).GetProperty(propName);
    if (propertyInfo == null) return;
    propertyInfo.SetValue(member, newValue);
}