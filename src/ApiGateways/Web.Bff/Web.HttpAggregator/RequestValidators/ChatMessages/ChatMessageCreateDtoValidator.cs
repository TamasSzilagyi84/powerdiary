namespace Web.HttpAggregator.RequestValidators.ChatMessages
{
    using Dtos.ChatMessage;

    using FluentValidation;

    using Shared;

    public class ChatMessageCreateDtoValidator : AbstractValidator<ChatMessageCreateDto>
    {
        public ChatMessageCreateDtoValidator()
        {
            this.RuleFor(x => x.CreatedByName)
                .Length(0, 25)
                .WithMessage(string.Format(Const.ValidationMessages.InvalidLength, 0, 25));
        }
    }
}
