using TunifyPlatform.Models;

namespace TunifyPlatform.Repositories.interfaces
{
    public interface IArtist
    {
        Task<List<Artist>> GetAllAsync();
        Task<Artist> GetByIdAsync(int artistId);
        Task<Artist> InsertAsync(Artist artist);
        Task<Artist> UpdateAsync(int id, Artist artist);
        Task<Artist> DeleteAsync(int artistId);
    }
}
