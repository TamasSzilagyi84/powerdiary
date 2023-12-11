namespace Chat.UnitTests.ChatMessage
{
    using AutoMapper;

    using Chat.Domain.Contracts;
    using Chat.Domain.Models;
    using Chat.Domain.Services;

    using Dtos.ChatMessage;
    using Dtos.Enums;

    using Moq;

    public class ChatMessageDomainServiceTests
    {
        private readonly IChatMessageDomainService _chatMessageDomainService;

        private readonly Mock<IChatMessageRepository> _mockChatMessageRepository;

        private readonly Mock<IMapper> _mockMapper;

        public ChatMessageDomainServiceTests()
        {
            this._mockMapper = new Mock<IMapper>();
            this._mockChatMessageRepository = new Mock<IChatMessageRepository>();
            this._chatMessageDomainService =
                new ChatMessageDomainService(this._mockChatMessageRepository.Object, this._mockMapper.Object);
        }

        [Fact]
        public async Task UpdateAsync_WithValidId_UpdatesChatMessageAndSaveChanges()
        {
            // Arrange
            Guid chatMessageId = Guid.NewGuid();
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

            ChatMessage chatMessage = new (createDto);

            ChatMessageUpdateDto updateDto = new ChatMessageUpdateDto
            {
                Id = chatMessageId,
                UpdatedById = Guid.NewGuid(),
                UpdatedByName = "Updating User",
                Type = ChatMessageType.Leave
            };

            this._mockChatMessageRepository
                .Setup(i => i.GetByIdAsync(chatMessageId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(chatMessage);

            this._mockMapper
                .Setup(i => i.Map<ChatMessageUpdateDto, ChatMessage>(updateDto, chatMessage))
                .Returns(chatMessage);

            // Act
            await this._chatMessageDomainService.UpdateAsync(updateDto, It.IsAny<CancellationToken>());

            // Assert
            this._mockChatMessageRepository.Verify(i => i.UpdateAsync(chatMessage, It.IsAny<CancellationToken>()));
        }
    }
}
