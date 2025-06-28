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
            var url = $"{BaseApiAddress}/v1/presentation/{presentationUuid}";
            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var contract = await response.Content.ReadFromJsonAsync<Contracts.PresentationResponse>();
            var presentation = _mapper.Map<Presentation>(contract?.Presentation);

            return presentation;
        }

        public async Task<WebImage> GetSlideImageAsync(string presentationUuid, int slideIndex)
        {
            var url = $"{BaseApiAddress}/v1/presentation/{presentationUuid}/thumbnail/{slideIndex}";
            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var contentType = response.Content.Headers.ContentType?.MediaType;
            var slideImageStream = await response.Content.ReadAsStreamAsync();

            return new WebImage
            {
                ContentType = contentType ?? "image/jpeg",
                Content = slideImageStream,
            };
        }

        public async Task TriggerNextSlideAsync(string presentationUuid)
        {
            var url = $"{BaseApiAddress}/v1/presentation/{presentationUuid}/next/trigger";
            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();
        }

        public async Task TriggerPreviousSlideAsync(string presentationUuid)
        {
            var url = $"{BaseApiAddress}/v1/presentation/{presentationUuid}/previous/trigger";
            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();
        }

        public async Task TriggerSlideAsync(string presentationUuid, int slideIndex)
        {
            var url = $"{BaseApiAddress}/v1/presentation/{presentationUuid}/{slideIndex}/trigger";
            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();
        }
    }
}
