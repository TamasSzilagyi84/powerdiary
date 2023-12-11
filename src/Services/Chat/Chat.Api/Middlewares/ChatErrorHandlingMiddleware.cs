namespace Chat.Api.Middlewares
{
    using System.Text.Json;

    using Dtos;

    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Options;

    using Options;

    using Shared;
    using Shared.Exceptions;

    internal class ChatErrorHandlingMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        private readonly ILogger _logger;

        private readonly bool _includeExceptionDetailsInResponse;

        public ChatErrorHandlingMiddleware(RequestDelegate requestDelegate, ILogger<ChatErrorHandlingMiddleware> logger, IOptions<ApplicationSettings> applicationSettings)
        {
            this._requestDelegate = requestDelegate;
            this._logger = logger;
            this._includeExceptionDetailsInResponse = applicationSettings.Value.IncludeExceptionStackInResponse;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await this._requestDelegate.Invoke(context).ConfigureAwait(false);
            }
            catch (EntityNotFoundException entityNotFoundException)
            {
                this._logger.LogWarning(System.Diagnostics.Activity.Current?.RootId, entityNotFoundException);

                context.Response.ContentType = "text/plain";
                context.Response.StatusCode = StatusCodes.Status404NotFound;

                await context.Response.WriteAsync(entityNotFoundException.Message);
            }
            catch (Exception exception)
            {
                this._logger.LogCritical(System.Diagnostics.Activity.Current?.RootId, exception);

                ErrorMessageDto errorMessageDto = new ErrorMessageDto
                {
                    Message = Const.ErrorMessages.ServerError,
                    CorrelationId = context.Response.Headers[Const.RequestHeaders.CorrelationId]
                };

                if (this._includeExceptionDetailsInResponse)
                {
                    errorMessageDto.InnerException = $"{exception.Message}{ exception.InnerException?.Message} {exception.StackTrace}";
                }

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(errorMessageDto)).ConfigureAwait(false);
            }
        }
    }
}
