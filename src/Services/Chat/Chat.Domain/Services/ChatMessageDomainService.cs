namespace Chat.Domain.Services
{
    using AutoMapper;

    using Chat.Domain.Contracts;

    using Dtos.ChatMessage;

    using Models;

    public class ChatMessageDomainService : IChatMessageDomainService
    {
        private readonly IChatMessageRepository _chatMessageRepository;
        private readonly IMapper _mapper;

        public ChatMessageDomainService(
            IChatMessageRepository chatMessageRepository,
            IMapper mapper)
        {
            this._chatMessageRepository = chatMessageRepository;
            this._mapper = mapper;
        }

        public async Task<Guid> AddAsync(ChatMessageCreateDto createDto, CancellationToken cancellationToken)
        {
            return await this._chatMessageRepository.AddAsync(new ChatMessage(createDto), cancellationToken).ConfigureAwait(false);
        }

        public async Task UpdateAsync(ChatMessageUpdateDto updateDto, CancellationToken cancellationToken)
        {
            ChatMessage chatMessage = await this._chatMessageRepository.GetByIdAsync(updateDto.Id, cancellationToken).ConfigureAwait(false);
            ChatMessage updatedChatMessage = this._mapper.Map<ChatMessageUpdateDto, ChatMessage>(updateDto, chatMessage);
            updatedChatMessage.Updated = DateTime.UtcNow;

            await this._chatMessageRepository.UpdateAsync(updatedChatMessage, cancellationToken).ConfigureAwait(false);
        }

        public async Task<ChatMessageGetDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            ChatMessage chatMessage = await this._chatMessageRepository.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);

            return this._mapper.Map<ChatMessageGetDto>(chatMessage);
        }

        public async Task<IList<ChatMessageListDto>>ListAsync(int page, int take, CancellationToken cancellationToken)
        {
            IList<ChatMessage> chatMessages = await this._chatMessageRepository
                .ListAsync(i => i.Created, page, take, cancellationToken)
                .ConfigureAwait(false);

            return this._mapper.Map<IList<ChatMessageListDto>>(chatMessages);
        }

        public async Task<IList<ChatMessageAggregationDto>> AggregateStatisticsAsync(ChatMessageAggregationRequestDto request, CancellationToken cancellationToken)
        {
            return await this._chatMessageRepository
                .AggregateStatisticsAsync(request, cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            await this._chatMessageRepository.DeleteAsync(id, cancellationToken).ConfigureAwait(false);
        }
    }
}
