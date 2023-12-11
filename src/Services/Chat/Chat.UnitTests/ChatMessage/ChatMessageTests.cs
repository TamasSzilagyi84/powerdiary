namespace Chat.UnitTests.ChatMessage
{
    using Chat.Domain.Models;

    using Dtos.ChatMessage;
    using Dtos.Enums;

    public class ChatMessageTests
    {
        [Fact]
        public void Ctor_CreatedByNameIsNull_ThrowsArgumentNullException()
        {
            // Arrange
            ChatMessageCreateDto createDto = new ChatMessageCreateDto
            {
                CreatedById = Guid.NewGuid(),
                CreatedByName = null,
                Type = ChatMessageType.Enter
            };

            // Act
            Action actual = () => new ChatMessage(createDto);

            // Assert
            Assert.Throws<ArgumentNullException>(actual);
        }

        [Fact]
        public void Ctor_CreatedByNameIsEmptyString_ThrowsArgumentNullException()
        {
            // Arrange
            ChatMessageCreateDto createDto = new ChatMessageCreateDto
            {
                CreatedById = Guid.NewGuid(),
                CreatedByName = string.Empty,
                Type = ChatMessageType.Enter
            };

            // Act
            Action actual = () => new ChatMessage(createDto);

            // Assert
            Assert.Throws<ArgumentNullException>(actual);
        }

        [Fact]
        public void Ctor_CreatedByNameIsEmptySpace_ThrowsArgumentNullException()
        {
            // Arrange
            ChatMessageCreateDto createDto = new ChatMessageCreateDto
            {
                CreatedById = Guid.NewGuid(),
                CreatedByName = " ",
                Type = ChatMessageType.Enter
            };

            // Act
            Action actual = () => new ChatMessage(createDto);

            // Assert
            Assert.Throws<ArgumentNullException>(actual);
        }

        [Fact]
        public void Ctor_WithValidValues_CreatesObject()
        {
            // Arrange
            DateTime utcNow = DateTime.UtcNow;
            Guid createdById = Guid.NewGuid();
            string createdByName = "Creator";
            ChatMessageType type = ChatMessageType.Enter;

            ChatMessageCreateDto createDto = new ChatMessageCreateDto
            {
                CreatedById = createdById,
                CreatedByName = createdByName,
                Type = type
            };

            // Act
            ChatMessage actual = new(createDto);

            // Assert
            Assert.Equal(createdById, actual.CreatedById);
            Assert.Equal(createdByName, actual.CreatedByName);
            Assert.Equal(type, actual.Type);
            Assert.True(actual.Created > utcNow);
        }

        [Theory]
        [InlineData(ChatMessageType.Enter, "comment", "Creator entered the room")]
        [InlineData(ChatMessageType.Leave, "comment", "Creator left the room")]
        [InlineData(ChatMessageType.HighFive, "comment", "Creator high fived another user")]
        [InlineData(ChatMessageType.Comment, "comment", "comment")]
        public void Ctor_WithDifferentTypes_CreatesCorrectMessage(ChatMessageType type, string comment, string expected)
        {
            // Arrange
            DateTime utcNow = DateTime.UtcNow;
            Guid createdById = Guid.NewGuid();
            string createdByName = "Creator";

            ChatMessageCreateDto createDto = new ChatMessageCreateDto
            {
                CreatedById = createdById,
                CreatedByName = createdByName,
                Type = type,
                Message = comment
            };

            // Act
            ChatMessage actual = new(createDto);

            // Assert
            Assert.Equal(expected, actual.Message);
        }
    }
}