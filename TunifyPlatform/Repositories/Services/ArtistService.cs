using Microsoft.EntityFrameworkCore;
using TunifyPlatform.Data;
using TunifyPlatform.Models;
using TunifyPlatform.Repositories.interfaces;

namespace TunifyPlatform.Repositories.Services
{
    public class ArtistService : IArtist
    {
        private readonly TunifyDbContext _context;

        public ArtistService(TunifyDbContext context)
        {
            _context = context;
        }

        public async Task<List<Artist>> GetAllAsync()
        {
            var allArtists = await _context.Artists.ToListAsync();
            return allArtists;
        }

        public async Task<Artist> GetByIdAsync(int artistId) => await _context.Artists.FindAsync(artistId);

        public async Task<Artist> InsertAsync(Artist artist)
        {
            _context.Artists.Add(artist);
            await _context.SaveChangesAsync();
            return artist;
        }

        public async Task<Artist> UpdateAsync(int id, Artist artist)
        {
            var exsitingEmployee = await _context.Artists.FindAsync(id);
            exsitingEmployee = artist;
            await _context.SaveChangesAsync();
            return artist;
        }

        public async Task<Artist> DeleteAsync(int id)
        {
            var artist = await GetByIdAsync(id);
            _context.Entry(artist).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return artist;
        }
    }
}
