using AutoMapper;
using ProPresenter7WEB.Core;
using System.Net.Http.Json;

namespace ProPresenter7WEB.Service
{
    public class PresentationService : IPresentationService
    {
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly IProPresenterService _proPresenterService;

        public PresentationService(
            IMapper mapper,
            HttpClient httpClient,
            IProPresenterService proPresenterService)
        {
            _mapper = mapper;
            _httpClient = httpClient;
            _proPresenterService = proPresenterService;
        }

        public async Task<Presentation> GetPresentationAsync(string presentationUuid)
        {
            if (_proPresenterService.ApiAddress == null)
            {
                throw new Exception("ProPresenter connection is not set.");
            }

            try
            {
                var url = $"{_proPresenterService.ApiAddress}/v1/presentation/{presentationUuid}";

                var response = await _httpClient.GetAsync(url);

                response.EnsureSuccessStatusCode();

                var contract = await response.Content.ReadFromJsonAsync<Contracts.PresentationResponse>();

                var result = _mapper.Map<Presentation>(contract?.Presentation);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
