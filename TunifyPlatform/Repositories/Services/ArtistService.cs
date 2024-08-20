using Microsoft.EntityFrameworkCore;
using TunifyPlatform.Data;
using TunifyPlatform.Models;
using TunifyPlatform.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TunifyPlatform.Repositories.Services
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly TunifyDbContext _context;

        public ArtistRepository(TunifyDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Artist>> GetAllArtistsAsync()
        {
            return await _context.Artists.ToListAsync();
        }

        public async Task<Artist> GetArtistByIdAsync(int id)
        {
            return await _context.Artists.FindAsync(id);
        }

        public async Task AddArtistAsync(Artist artist)
        {
            await _context.Artists.AddAsync(artist);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateArtistAsync(Artist artist)
        {
            _context.Artists.Update(artist);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteArtistAsync(int id)
        {
            var artist = await _context.Artists.FindAsync(id);
            if (artist != null)
            {
                _context.Artists.Remove(artist);
                await _context.SaveChangesAsync();
            }
        }

        // New methods for managing ArtistSongs relationships

        public async Task AddSongToArtistAsync(int artistId, int songId)
        {
            var artist = await _context.Artists.FindAsync(artistId);
            var song = await _context.Songs.FindAsync(songId);

            if (artist != null && song != null)
            {
                var artistSong = new ArtistSongs
                {
                    ArtistId = artistId,
                    SongId = songId
                };
                _context.ArtistSongs.Add(artistSong);
                await _context.SaveChangesAsync();
            }
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