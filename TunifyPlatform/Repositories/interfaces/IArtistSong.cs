using TunifyPlatform.Models;

namespace TunifyPlatform.Repositories.Interfaces
{
    public interface IArtistSongsRepository
    {
        Task AddSongToArtistAsync(int artistId, int songId);
        Task<IEnumerable<Song>> GetSongsByArtistAsync(int artistId);
        Task RemoveSongFromArtistAsync(int artistId, int songId);
    }
}