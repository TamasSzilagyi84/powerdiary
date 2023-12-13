namespace Chat.Infrastructure.Repositories
{
    using Domain.Contracts;
    using Domain.Models;

    using Dtos.ChatMessage;
    using Dtos.Enums;

    using Microsoft.EntityFrameworkCore;

    using Shared.Repository.EfBaseRepository;

    public class ChatMessageRepository : EfRepositoryBase<ChatMessage>, IChatMessageRepository
    {
        private readonly ChatMessageContext _context;

        public ChatMessageRepository(ChatMessageContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<IList<ChatMessageAggregationDto>> AggregateStatisticsAsync(
            ChatMessageAggregationRequestDto request, 
            CancellationToken cancellationToken)
        {
            IQueryable<ChatMessageAggregationDto> query = request.AggregationType switch
            {
                ChatMessageAggregationType.Yearly => this._context.ChatMessages
                    .GroupBy(i => new {i.Created.Year, i.Type})
                    .OrderByDescending(i => i.Key.Year)
                    .ThenBy(i => i.Key.Type)
                    .Select(i => new ChatMessageAggregationDto()
                    {
                        Created = new DateTime(i.Key.Year, 1, 1), Type = i.Key.Type, Count = i.Count()
                    }),
                ChatMessageAggregationType.Monthly => this._context.ChatMessages
                    .GroupBy(i => new {i.Created.Year, i.Created.Month, i.Type})
                    .OrderByDescending(i => i.Key.Year)
                    .ThenByDescending(i => i.Key.Month)
                    .ThenBy(i => i.Key.Type)
                    .Select(i => new ChatMessageAggregationDto()
                    {
                        Created = new DateTime(i.Key.Year, i.Key.Month, 1), Type = i.Key.Type, Count = i.Count()
                    }),
                ChatMessageAggregationType.Daily => this._context.ChatMessages
                    .GroupBy(i => new {i.Created.Year, i.Created.Month, i.Created.Day, i.Type})
                    .OrderByDescending(i => i.Key.Year)
                    .ThenByDescending(i => i.Key.Month)
                    .ThenByDescending(i => i.Key.Day)
                    .ThenBy(i => i.Key.Type)
                    .Select(i => new ChatMessageAggregationDto()
                    {
                        Created = new DateTime(i.Key.Year, i.Key.Month, i.Key.Day),
                        Type = i.Key.Type,
                        Count = i.Count()
                    }),
                ChatMessageAggregationType.Hourly => this._context.ChatMessages
                    .GroupBy(i => new
                    {
                        i.Created.Year,
                        i.Created.Month,
                        i.Created.Day,
                        i.Created.Hour,
                        i.Type
                    })
                    .OrderByDescending(i => i.Key.Year)
                    .ThenByDescending(i => i.Key.Month)
                    .ThenByDescending(i => i.Key.Day)
                    .ThenByDescending(i => i.Key.Hour)
                    .ThenBy(i => i.Key.Type)
                    .Select(i => new ChatMessageAggregationDto()
                    {
                        Created = new DateTime(
                            i.Key.Year, 
                            i.Key.Month, 
                            i.Key.Day, 
                            i.Key.Hour, 
                            0, 
                            0),
                        Type = i.Key.Type,
                        Count = i.Count()
                    }),
                ChatMessageAggregationType.Minutely => this._context.ChatMessages
                    .GroupBy(i => new
                    {
                        i.Created.Year,
                        i.Created.Month,
                        i.Created.Day,
                        i.Created.Hour,
                        i.Created.Minute,
                        i.Type
                    })
                    .OrderByDescending(i => i.Key.Year)
                    .ThenByDescending(i => i.Key.Month)
                    .ThenByDescending(i => i.Key.Day)
                    .ThenByDescending(i => i.Key.Hour)
                    .ThenByDescending(i => i.Key.Minute)
                    .ThenBy(i => i.Key.Type)
                    .Select(i => new ChatMessageAggregationDto()
                    {
                        Created = new DateTime(
                            i.Key.Year, 
                            i.Key.Month, 
                            i.Key.Day, 
                            i.Key.Hour, 
                            i.Key.Minute, 
                            0, 
                            0),
                        Type = i.Key.Type,
                        Count = i.Count()
                    }),
                _ => throw new ArgumentException($"{request.AggregationType} is not an implemented type for aggregation")
            };

            return await query
                //.OrderByDescending(i => i.Created)
                .Skip(request.Page * request.Take)
                .Take(request.Take)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
