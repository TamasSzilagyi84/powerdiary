namespace Web.HttpAggregator.Services
{
    using Web.HttpAggregator.Contracts;

    internal class CorrelationIdGenerator : ICorrelationIdGenerator
    {
        private string _correlationId = Guid.NewGuid().ToString();

        public string Get() => this._correlationId;

        public void Set(string correlationId) 
        { 
            this._correlationId = correlationId;
        }
    }
}
