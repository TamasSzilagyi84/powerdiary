namespace Web.HttpAggregator.Controllers
{
    using Contracts;

    using Dtos.ChatMessage;
    using Dtos.Enums;

    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class ChatMessagesController : ControllerBase
    {
        private readonly IChatMessageService _chatMessageService;

        public ChatMessagesController(IChatMessageService chatMessageService)
        {
            this._chatMessageService = chatMessageService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(ChatMessageCreateDto request, CancellationToken cancellationToken)
        {
            Guid id = await this._chatMessageService.AddAsync(request, cancellationToken).ConfigureAwait(false);

            return this.Created(nameof(this.Get), id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, ChatMessageUpdateDto request, CancellationToken cancellationToken)
        {
            await this._chatMessageService.UpdateAsync(request, cancellationToken).ConfigureAwait(false);

            return this.Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            ChatMessageGetDto chatMessage = await this._chatMessageService.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);

            return this.Ok(chatMessage);
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] int page, [FromQuery] int take, CancellationToken cancellationToken)
        {
            IList<ChatMessageListDto> chatMessages = await this._chatMessageService.ListAsync(page, take, cancellationToken).ConfigureAwait(false);

            return this.Ok(chatMessages);
        }

        [HttpGet("statistics")]
        public async Task<IActionResult> Statistics([FromQuery] int page, [FromQuery] int take, [FromQuery] ChatMessageAggregationType type, CancellationToken cancellationToken)
        {
            IList<ChatMessageAggregationDto> chatMessages = await this._chatMessageService
                .AggregateStatisticsAsync(
                    page,
                    take,
                    type,
                    cancellationToken)
                .ConfigureAwait(false);

            return this.Ok(chatMessages);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await this._chatMessageService.DeleteAsync(id, cancellationToken).ConfigureAwait(false);

            return this.Ok();
        }
    }
}
