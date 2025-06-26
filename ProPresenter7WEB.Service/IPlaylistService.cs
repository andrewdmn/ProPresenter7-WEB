using ProPresenter7WEB.Core;

namespace ProPresenter7WEB.Service
{
    public interface IPlaylistService
    {
        string? BaseApiAddress { get; set; }

        Task<IEnumerable<Playlist>> GetPlaylistsAsync();

        Task<PlaylistDetails> GetPlayListDetailsAsync(string uuid);
    }
}
