namespace Shared
{
    public static class Const
    {
        public static class ConfigurationKeys
        {
            public static class ServiceUrls
            {
                public const string ChatMessageService = "Services:ChatMessage";
            }
        }

        public static class RequestHeaders
        {
            public const string CorrelationId = "X-Correlation-Id";
        }

        public static class ErrorMessages
        {
            public const string ServerError =
                "An error has occured while handling your request. Please contact our technical department referring to this correlation id";
        }

        public static class ValidationMessages
        {
            public const string InvalidLength = "Lenght must be between {0} and {1}";
        }

        public static class TestData
        {
            public static Guid ChatMessageId = new ("8ec1d533-07ee-4df0-91a1-4f734d7bbf68");
            public static Guid SecondChatMessageId = new ("8ec1d533-07ee-4df0-91a1-4f734d7bbf61");
        }
    }
}
