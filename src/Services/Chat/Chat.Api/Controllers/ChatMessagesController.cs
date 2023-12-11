namespace Chat.Api.Controllers
{
    using Domain.Contracts;

    using Dtos.ChatMessage;
    using Dtos.Enums;

    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class ChatMessagesController : ControllerBase
    {
        private readonly IChatMessageDomainService _chatMessageDomainService;

        public ChatMessagesController(IChatMessageDomainService chatMessageDomainService)
        {
            this._chatMessageDomainService = chatMessageDomainService;
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(ChatMessageCreateDto request, CancellationToken cancellationToken)
        {
            Guid id = await this._chatMessageDomainService.AddAsync(request, cancellationToken).ConfigureAwait(false);

            return this.Created(nameof(this.Get), id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, ChatMessageUpdateDto request, CancellationToken cancellationToken)
        {
            await this._chatMessageDomainService.UpdateAsync(request, cancellationToken).ConfigureAwait(false);

            return this.Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            ChatMessageGetDto chatMessage = await this._chatMessageDomainService.GetByIdAsync(id, cancellationToken).ConfigureAwait(false);

            return this.Ok(chatMessage);
        }

        [HttpGet]
        public async Task<IActionResult> List([FromQuery] int page, [FromQuery] int take, CancellationToken cancellationToken)
        {
            IList<ChatMessageListDto> chatMessages = await this._chatMessageDomainService.ListAsync(page, take, cancellationToken).ConfigureAwait(false);

            return this.Ok(chatMessages);
        }

        [HttpGet("statistics")]
        public async Task<IActionResult> Statistics([FromQuery] int page, [FromQuery] int take, [FromQuery] ChatMessageAggregationType type, CancellationToken cancellationToken)
        {
            IList<ChatMessageAggregationDto> chatMessages = await this._chatMessageDomainService
                .AggregateStatisticsAsync(new ChatMessageAggregationRequestDto
                {
                    Page = page,
                    Take = take,
                    AggregationType = type
                }, cancellationToken)
                .ConfigureAwait(false);

            return this.Ok(chatMessages);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await this._chatMessageDomainService.DeleteAsync(id, cancellationToken).ConfigureAwait(false);

            return this.Ok();
        }
    }
}
