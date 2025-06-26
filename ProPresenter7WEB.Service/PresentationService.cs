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

        public async Task<WebImage> GetSlideImageAsync(string presentationUuid, int slideIndex)
        {
            var response = await _httpClient.GetAsync($"{BaseApiAddress}/v1/presentation/{presentationUuid}/thumbnail/{slideIndex}");

            response.EnsureSuccessStatusCode();

            var contentType = response.Content.Headers.ContentType?.MediaType;
            var slideImageStream = await response.Content.ReadAsStreamAsync();

            return new WebImage
            {
                ContentType = contentType ?? "image/jpeg",
                Content = slideImageStream,
            };
        }
    }
}
