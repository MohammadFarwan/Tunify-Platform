using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TunifyPlatform.Models;
using TunifyPlatform.Repositories.Interfaces;

namespace TunifyPlatform.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SongController : ControllerBase
    {
        private readonly ISongRepository _songRepository;

        public SongController(ISongRepository songRepository)
        {
            _songRepository = songRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSongs()
        {
            var songs = await _songRepository.GetAllSongsAsync();
            return Ok(songs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSongById(int id)
        {
            var song = await _songRepository.GetSongByIdAsync(id);
            if (song == null)
                return NotFound();

            return Ok(song);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSong(Song song)
        {
            await _songRepository.AddSongAsync(song);
            return CreatedAtAction(nameof(GetSongById), new { id = song.Id }, song);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSong(int id, Song song)
        {
            if (id != song.Id)
                return BadRequest();

            await _songRepository.UpdateSongAsync(song);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(int id)
        {
            await _songRepository.DeleteSongAsync(id);
            return NoContent();
        }
    }
}