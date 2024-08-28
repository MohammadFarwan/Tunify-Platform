using TunifyPlatform.Models;

namespace TunifyPlatform.Repositories.Interfaces
{
    public interface IArtistRepository
    {
        Task<IEnumerable<Artist>> GetAllArtistsAsync();
        Task<Artist> GetArtistByIdAsync(int id);
        Task AddArtistAsync(Artist artist);
        Task UpdateArtistAsync(Artist artist);
        Task DeleteArtistAsync(int id);
        Task<IEnumerable<Song>> GetSongsByArtistAsync(int artistId);
        //Task AddSongToArtistAsync(int artistId, int songId);
        Task RemoveSongFromArtistAsync(int artistId, int songId);
    }
}