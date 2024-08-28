using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TunifyPlatform.Models;
using TunifyPlatform.Repositories.Interfaces;

namespace TunifyPlatform.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PlaylistController : ControllerBase
    {
        private readonly IPlaylistRepository _playlistRepository;

        public PlaylistController(IPlaylistRepository playlistRepository)
        {
            _playlistRepository = playlistRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPlaylists()
        {
            var playlists = await _playlistRepository.GetAllPlaylistsAsync();
            return Ok(playlists);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlaylistById(int id)
        {
            var playlist = await _playlistRepository.GetPlaylistByIdAsync(id);
            if (playlist == null)
                return NotFound();

            return Ok(playlist);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlaylist(Playlist playlist)
        {
            await _playlistRepository.AddPlaylistAsync(playlist);
            return CreatedAtAction(nameof(GetPlaylistById), new { id = playlist.Id }, playlist);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlaylist(int id, Playlist playlist)
        {
            if (id != playlist.Id)
                return BadRequest();

            await _playlistRepository.UpdatePlaylistAsync(playlist);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlaylist(int id)
        {
            await _playlistRepository.DeletePlaylistAsync(id);
            return NoContent();
        }
    }
}