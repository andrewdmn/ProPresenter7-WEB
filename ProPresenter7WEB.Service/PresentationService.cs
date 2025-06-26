using AutoMapper;
using ProPresenter7WEB.Core;
using System.Net.Http.Json;

namespace ProPresenter7WEB.Service
{
    public class PresentationService : IPresentationService
    {
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;

        public PresentationService(
            IMapper mapper,
            HttpClient httpClient)
        {
            _mapper = mapper;
            _httpClient = httpClient;
        }

        public string? BaseApiAddress { get; set; }

        public async Task<Presentation> GetPresentationAsync(string presentationUuid)
        {
            var response = await _httpClient.GetAsync($"{BaseApiAddress}/v1/presentation/{presentationUuid}");

            response.EnsureSuccessStatusCode();

            var contract = await response.Content.ReadFromJsonAsync<Contracts.PresentationResponse>();
            var presentation = _mapper.Map<Presentation>(contract?.Presentation);

            return presentation;
        }
    }
}
