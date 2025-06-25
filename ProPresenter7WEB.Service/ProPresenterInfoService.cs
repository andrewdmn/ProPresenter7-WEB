using AutoMapper;
using ProPresenter7WEB.Core;
using System.Net.Http.Json;

namespace ProPresenter7WEB.Service
{
    public class ProPresenterInfoService : IProPresenterInfoService
    {
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;
        private readonly IProPresenterStorageService _proPresenterService;

        public ProPresenterInfoService(
            IMapper mapper,
            HttpClient httpClient, 
            IProPresenterStorageService proPresenterService)
        {
            _mapper = mapper;
            _httpClient = httpClient;
            _proPresenterService = proPresenterService;
        }

        public async Task<ProPresenterInfo> GetProPresenterInfoAsync()
        {
            var response = await _httpClient.GetAsync($"{_proPresenterService.ApiAddress}/version");

            response.EnsureSuccessStatusCode();

            var contract = await response.Content.ReadFromJsonAsync<Contracts.VersionInfo>();

            if (contract == null)
            {
                throw new InvalidOperationException("Response cannot be deserialized.");
            }

            return _mapper.Map<ProPresenterInfo>(contract);
        }
    }
}
