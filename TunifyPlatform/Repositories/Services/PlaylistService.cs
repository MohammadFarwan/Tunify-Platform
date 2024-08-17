using Microsoft.EntityFrameworkCore;
using TunifyPlatform.Data;
using TunifyPlatform.Models;
using TunifyPlatform.Repositories.interfaces;

namespace TunifyPlatform.Repositories.Services
{
    public class PlaylistService : IPlayList
    {
        private readonly TunifyDbContext _context;

        public PlaylistService(TunifyDbContext context)
        {
            _context = context;
        }

        public async Task<List<Playlist>> GetAllAsync()
        {
            var allPlaylists = await _context.Playlists.ToListAsync();
            return allPlaylists;
        }

        public async Task<Playlist> GetByIdAsync(int PlaylistId) => await _context.Playlists.FindAsync(PlaylistId);

        public async Task<Playlist> InsertAsync(Playlist Playlist)
        {
            _context.Playlists.Add(Playlist);
            await _context.SaveChangesAsync();
            return Playlist;
        }

        public async Task<Playlist> UpdateAsync(int id, Playlist Playlist)
        {
            var exsitingEmployee = await _context.Playlists.FindAsync(id);
            exsitingEmployee = Playlist;
            await _context.SaveChangesAsync();
            return Playlist;
        }

        public async Task<Playlist> DeleteAsync(int id)
        {
            var Playlist = await GetByIdAsync(id);
            _context.Entry(Playlist).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return Playlist;
        }
    }
}
