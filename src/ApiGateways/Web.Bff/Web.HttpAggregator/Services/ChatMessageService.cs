namespace Web.HttpAggregator.Services
{
    using System.Text.Json;

    using Dtos.ChatMessage;
    using Dtos.Enums;

    using Shared;

    using Web.HttpAggregator.Contracts;
    using Web.HttpAggregator.Extensions;

    internal class ChatMessageService : IChatMessageService
    {
        private readonly HttpClient _httpClient;
        private readonly ICorrelationIdGenerator _correlationIdGenerator;

        private readonly string _endpointUrl = string.Empty;

        public ChatMessageService(
            HttpClient httpClient,
            ICorrelationIdGenerator correlationIdGenerator)
        {
            this._httpClient = httpClient;
            this._correlationIdGenerator = correlationIdGenerator;
            this._endpointUrl = "api/ChatMessages";
            this.AddCorrelationIdToHttpClient();
        }

        public async Task<Guid> AddAsync(ChatMessageCreateDto createDto, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await this._httpClient.PostAsJsonAsync(this._endpointUrl, createDto, cancellationToken).ConfigureAwait(false);

            await response.HandleUnsuccessfulResponse(cancellationToken).ConfigureAwait(false);

            string? id = await response.Content.ReadFromJsonAsync<string>(cancellationToken: cancellationToken).ConfigureAwait(false);
            
            return new Guid(id);
        }

        public async Task UpdateAsync(ChatMessageUpdateDto updateDto, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await this._httpClient.PutAsJsonAsync($"{this._endpointUrl}/{updateDto.Id}", updateDto, cancellationToken).ConfigureAwait(false);

            await response.HandleUnsuccessfulResponse(cancellationToken).ConfigureAwait(false);
        }

        public async Task<ChatMessageGetDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await this._httpClient.GetAsync($"{this._endpointUrl}/{id}", cancellationToken).ConfigureAwait(false);

            await response.HandleUnsuccessfulResponse(cancellationToken).ConfigureAwait(false);

            string responseString = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            return JsonSerializer.Deserialize<ChatMessageGetDto>(responseString);
        }

        public async Task<IList<ChatMessageListDto>> ListAsync(int page, int take, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await this._httpClient.GetAsync($"{this._endpointUrl}?page={page}&take={take}", cancellationToken).ConfigureAwait(false);

            await response.HandleUnsuccessfulResponse(cancellationToken).ConfigureAwait(false);

            string responseString = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            return JsonSerializer.Deserialize<IList<ChatMessageListDto>>(responseString);
        }

        public async Task<IList<ChatMessageAggregationDto>> AggregateStatisticsAsync(int page, int take, ChatMessageAggregationType type, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await this._httpClient.GetAsync($"{this._endpointUrl}/statistics?page={page}&take={take}&type={type}", cancellationToken).ConfigureAwait(false);

            await response.HandleUnsuccessfulResponse(cancellationToken).ConfigureAwait(false);

            string responseString = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            return JsonSerializer.Deserialize<IList<ChatMessageAggregationDto>>(responseString);
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await this._httpClient.DeleteAsync($"{this._endpointUrl}/{id}", cancellationToken).ConfigureAwait(false);

            await response.HandleUnsuccessfulResponse(cancellationToken).ConfigureAwait(false);
        }

        private void AddCorrelationIdToHttpClient()
        {
            this._httpClient.DefaultRequestHeaders.Add(Const.RequestHeaders.CorrelationId, this._correlationIdGenerator.Get());
        }
    }
}
