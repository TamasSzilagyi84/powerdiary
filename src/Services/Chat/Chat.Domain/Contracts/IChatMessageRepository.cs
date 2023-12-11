namespace Chat.Domain.Contracts
{
    using Dtos.ChatMessage;

    using Models;

    using Shared.Repository.EfBaseRepository;

    public interface IChatMessageRepository : IEfRepositoryBase<ChatMessage>
    {
        Task<IList<ChatMessageAggregationDto>> AggregateStatisticsAsync(
            ChatMessageAggregationRequestDto request,
            CancellationToken cancellationToken);
    }
}
