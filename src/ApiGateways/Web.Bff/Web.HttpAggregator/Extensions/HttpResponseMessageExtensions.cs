namespace Web.HttpAggregator.Extensions
{
    using System.Net;

    using Shared.Exceptions;

    internal static class HttpResponseMessageExtensions
    {
        internal static async Task HandleUnsuccessfulResponse(this HttpResponseMessage httpResponseMessage, CancellationToken cancellationToken)
        {
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                return;
            }

            throw httpResponseMessage.StatusCode switch
            {
                HttpStatusCode.BadRequest => new BadHttpRequestException(await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken)),
                HttpStatusCode.NotFound => new EntityNotFoundException(await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken)),
                HttpStatusCode.InternalServerError => new Exception(await httpResponseMessage.Content.ReadAsStringAsync(cancellationToken)),
                _ => new ArgumentOutOfRangeException(),
            };
        }
    }
}
