namespace Web.HttpAggregator.Contracts
{
    internal interface ICorrelationIdGenerator
    {
        string Get();

        void Set(string correlationId);
    }
}
