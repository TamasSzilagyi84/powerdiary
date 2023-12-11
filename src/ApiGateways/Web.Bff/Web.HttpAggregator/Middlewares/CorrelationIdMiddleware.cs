namespace Web.HttpAggregator.Middlewares
{
    using Contracts;

    using Microsoft.Extensions.Primitives;

    using Shared;

    internal class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;

        public CorrelationIdMiddleware(RequestDelegate next) { 
            this._next = next; 
        }

        public async Task Invoke(HttpContext context, ICorrelationIdGenerator correlationIdGenerator)
        {
            StringValues correlationId = GetCorrelationId(context, correlationIdGenerator);
            AddCorrelationIdHeaderToResponse(context, correlationId);

            await this._next(context);
        }

        private static StringValues GetCorrelationId(HttpContext context, ICorrelationIdGenerator correlationIdGenerator)
        {
            if(context.Request.Headers.TryGetValue(Const.RequestHeaders.CorrelationId, out StringValues correlationId))
            {
                correlationIdGenerator.Set(correlationId);
                return correlationId;
            }

            return correlationIdGenerator.Get();
        }

        private static void AddCorrelationIdHeaderToResponse(HttpContext context, StringValues correlationId)
        { 
            context.Response.OnStarting(() =>
            {
                context.Response.Headers.Add(Const.RequestHeaders.CorrelationId, correlationId.ToString());
                return Task.CompletedTask;
            });
        }
    }
}
