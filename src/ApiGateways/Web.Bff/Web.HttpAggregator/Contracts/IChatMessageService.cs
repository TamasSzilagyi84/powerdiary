namespace Web.HttpAggregator.Contracts
{
    using Dtos.ChatMessage;
    using Dtos.Enums;

    public interface IChatMessageService
    {
        Task<Guid> AddAsync(ChatMessageCreateDto createDto, CancellationToken cancellationToken);

        Task UpdateAsync(ChatMessageUpdateDto updateDto, CancellationToken cancellationToken);

        Task<ChatMessageGetDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<IList<ChatMessageListDto>> ListAsync(int page, int take, CancellationToken cancellationToken);

        Task<IList<ChatMessageAggregationDto>> AggregateStatisticsAsync(
            int page, 
            int take, 
            ChatMessageAggregationType type, 
            CancellationToken cancellationToken);

        Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    }
}
