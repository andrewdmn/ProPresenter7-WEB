using AutoMapper;
using ProPresenter7WEB.Core;
using System.Net.Http.Json;

namespace ProPresenter7WEB.Service
{
    public class PlaylistService : IPlaylistService
    {
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;

        public PlaylistService(
            IMapper mapper,
            HttpClient httpClient)
        {
            _mapper = mapper;
            _httpClient = httpClient;
        }

        public string? BaseApiAddress { get; set; }

        public async Task<IEnumerable<Playlist>> GetPlaylistsAsync()
        {
            var response = await _httpClient.GetAsync($"{BaseApiAddress}/v1/playlists");

            response.EnsureSuccessStatusCode();

            var contracts = await response.Content.ReadFromJsonAsync<Contracts.Playlist[]>();

            if (contracts == null)
            {
                return Enumerable.Empty<Playlist>();
            }

            var playlists = FilterPlaylists(contracts);

            return playlists;
        }

        public async Task<PlaylistDetails> GetPlayListDetailsAsync(string uuid)
        {
            var response = await _httpClient.GetAsync($"{BaseApiAddress}/v1/playlist/{uuid}");

            response.EnsureSuccessStatusCode();

            var contract = await response.Content.ReadFromJsonAsync<Contracts.PlaylistDetails>();

            if (contract == null)
            {
                throw new InvalidOperationException($"No playlist details was found with uuid {uuid}.");
            }

            return _mapper.Map<PlaylistDetails>(contract);
        }

        private IEnumerable<Playlist> FilterPlaylists(IEnumerable<Contracts.Playlist> contracts)
        {
            var playlists = new List<Playlist>();

            foreach (var contract in contracts)
            {
                if (contract.FieldType == Contracts.PlaylistFieldTypeEnum.Playlist)
                {
                    playlists.Add(_mapper.Map<Playlist>(contract));
                }
                else
                {
                    playlists.AddRange(FilterPlaylists(contract.Children));
                }
            }

            return playlists;
        }
    }
}
