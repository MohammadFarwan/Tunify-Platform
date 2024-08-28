using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TunifyPlatform.Models;
using TunifyPlatform.Repositories.Interfaces;

namespace TunifyPlatform.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ArtistController : ControllerBase
    {
        private readonly IArtistRepository _artistRepository;

        public ArtistController(IArtistRepository artistRepository)
        {
            _artistRepository = artistRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllArtists()
        {
            var artists = await _artistRepository.GetAllArtistsAsync();
            return Ok(artists);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetArtistById(int id)
        {
            var artist = await _artistRepository.GetArtistByIdAsync(id);
            if (artist == null)
                return NotFound();

            return Ok(artist);
        }

        [HttpPost]
        public async Task<IActionResult> CreateArtist(Artist artist)
        {
            await _artistRepository.AddArtistAsync(artist);
            return CreatedAtAction(nameof(GetArtistById), new { id = artist.Id }, artist);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArtist(int id, Artist artist)
        {
            if (id != artist.Id)
                return BadRequest();

            await _artistRepository.UpdateArtistAsync(artist);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            await _artistRepository.DeleteArtistAsync(id);
            return NoContent();
        }

        //[HttpPost("{artistId}/songs/{songId}")]
        //public async Task<IActionResult> AddSongToArtist(int artistId, int songId)
        //{
        //    await _artistRepository.AddSongToArtistAsync(artistId, songId);
        //    return Ok();
        //}

        [HttpGet("{artistId}/songs")]
        public async Task<IActionResult> GetSongsByArtist(int artistId)
        {
            var songs = await _artistRepository.GetSongsByArtistAsync(artistId);
            return Ok(songs);
        }
    }
}