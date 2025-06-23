using AutoMapper;
using ProPresenter7WEB.Core;
using System.Net.Http.Json;

namespace ProPresenter7WEB.Service
{
    public class PresentationService : IPresentationService
    {
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly string _apiPresentationUrl;

        public PresentationService(
            IMapper mapper,
            HttpClient httpClient,
            IProPresenterService proPresenterService)
        {
            _mapper = mapper;
            _httpClient = httpClient;

            _apiPresentationUrl = $"{proPresenterService.ApiAddress}/v1/presentation";
        }

        public async Task<Presentation> GetPresentationAsync(string presentationUuid)
        {
            var response = await _httpClient.GetAsync(_apiPresentationUrl);

            response.EnsureSuccessStatusCode();

            var contract = await response.Content.ReadFromJsonAsync<Contracts.Presentation>();

            // TODO: Implement mapping and try to add logic to count amount of slides
            return _mapper.Map<Presentation>(contract);
        }
    }
}
