using Microsoft.EntityFrameworkCore;
using TunifyPlatform.Data;
using TunifyPlatform.Models;
using TunifyPlatform.Repositories.Interfaces;

namespace TunifyPlatform.Repositories.Services
{
    public class ArtistSongsRepository : IArtistSongsRepository
    {
        private readonly TunifyDbContext _context;

        public ArtistSongsRepository(TunifyDbContext context)
        {
            _context = context;
        }

        public async Task AddSongToArtistAsync(int artistId, int songId)
        {
            var artistSong = new ArtistSongs
            {
                ArtistId = artistId,
                SongId = songId
            };
            _context.ArtistSongs.Add(artistSong);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Song>> GetSongsByArtistAsync(int artistId)
        {
            return await _context.ArtistSongs
                .Where(a => a.ArtistId == artistId)
                .Select(a => a.Song)
                .ToListAsync();
        }

        public async Task RemoveSongFromArtistAsync(int artistId, int songId)
        {
            var artistSong = await _context.ArtistSongs
                .FirstOrDefaultAsync(a => a.ArtistId == artistId && a.SongId == songId);

            if (artistSong != null)
            {
                _context.ArtistSongs.Remove(artistSong);
                await _context.SaveChangesAsync();
            }
        }
    }
}