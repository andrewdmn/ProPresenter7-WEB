using AutoMapper;
using ProPresenter7WEB.Core;
using System.Net.Http.Json;

namespace ProPresenter7WEB.Service
{
    public class PresentationService : IPresentationService
    {
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly IProPresenterStorageService _proPresenterService;
        private readonly string _presentationApiUrl;

        public PresentationService(
            IMapper mapper,
            HttpClient httpClient,
            IProPresenterStorageService proPresenterService)
        {
            _mapper = mapper;
            _httpClient = httpClient;
            _proPresenterService = proPresenterService;
            _presentationApiUrl = $"{_proPresenterService.ApiAddress}/v1/presentation";
        }

        public async Task<Presentation> GetPresentationAsync(string presentationUuid)
        {
            var response = await _httpClient.GetAsync($"{_presentationApiUrl}/{presentationUuid}");

            response.EnsureSuccessStatusCode();

            var contract = await response.Content.ReadFromJsonAsync<Contracts.PresentationResponse>();

            var result = _mapper.Map<Presentation>(contract?.Presentation);

            return result;
        }
    }
}
