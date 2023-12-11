namespace Dtos
{
    public class ErrorMessageDto
    {
        public string Message { get; set; } = string.Empty;

        public string CorrelationId { get; set; } = string.Empty;

        public string? InnerException { get; set; }
    }
}
