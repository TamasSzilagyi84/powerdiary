namespace Chat.Domain.Mappers
{
    using AutoMapper;

    using Dtos.ChatMessage;

    using Models;

    public class ChatMessageProfiles : Profile
    {
        public ChatMessageProfiles()
        {
            this.CreateMap<ChatMessage, ChatMessageGetDto>();

            this.CreateMap<ChatMessage, ChatMessageListDto>();

            this.CreateMap<ChatMessageUpdateDto, ChatMessage>()
                .ForMember(dest =>dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest =>dest.Message, opt => opt.MapFrom(src => src.Message))
                .ForMember(dest =>dest.UpdatedById, opt => opt.MapFrom(src => src.UpdatedById))
                .ForMember(dest =>dest.UpdatedByName, opt => opt.MapFrom(src => src.UpdatedByName));
        }
    }
}
