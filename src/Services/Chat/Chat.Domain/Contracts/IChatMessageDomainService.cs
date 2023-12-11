namespace Chat.Domain.Contracts
{
    using Dtos.ChatMessage;

    public interface IChatMessageDomainService
    {
        Task<Guid> AddAsync(ChatMessageCreateDto createDto, CancellationToken cancellationToken);

        Task UpdateAsync(ChatMessageUpdateDto updateDto, CancellationToken cancellationToken);

        Task<ChatMessageGetDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<IList<ChatMessageListDto>> ListAsync(int page, int take, CancellationToken cancellationToken);

        Task<IList<ChatMessageAggregationDto>> AggregateStatisticsAsync(
            ChatMessageAggregationRequestDto request, 
            CancellationToken cancellationToken);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
