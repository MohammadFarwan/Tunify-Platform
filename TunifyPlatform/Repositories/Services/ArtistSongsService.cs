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

        //public async Task AddSongToArtistAsync(int artistId, int songId)
        //{
        //    var artist = await _context.Artists.Include(a => a.Songs).FirstOrDefaultAsync(a => a.Id == artistId);
        //    if (artist == null)
        //    {
        //        throw new Exception("Artist not found");
        //    }

        //    var song = await _context.Songs.FindAsync(songId);
        //    if (song == null)
        //    {
        //        throw new Exception("Song not found");
        //    }

        //    artist.Songs.Add(song);
        //    await _context.SaveChangesAsync();
        //}

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