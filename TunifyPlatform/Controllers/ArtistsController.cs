using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TunifyPlatform.Data;
using TunifyPlatform.Models;
using TunifyPlatform.Repositories.interfaces;

namespace TunifyPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtist _artist;
        private readonly ISong _song;

        public ArtistsController(IArtist Artist, ISong song)
        {
            _artist = Artist;
            _song = song;
        }

        // GET: api/Artists
        [Route("/artists/getAll")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Artist>>> GetArtists()
        {
            return await _artist.GetAllAsync();
        }

        // GET: api/Artists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Artist>> GetArtist(int id)
        {
            return await _artist.GetByIdAsync(id);
        }

        // PUT: api/Artists/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtist(int id, Artist Artist)
        {
            var updateArtist = await _artist.UpdateAsync(id, Artist);
            return Ok(updateArtist);
        }

        // POST: api/Artists
        [HttpPost]
        public async Task<ActionResult<Artist>> PostArtist(Artist Artist)
        {
            var newArtist = await _artist.InsertAsync(Artist);
            return Ok(newArtist);
        }

        // DELETE: api/Artists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            var deletedEmployee = _artist.DeleteAsync(id);
            return Ok(deletedEmployee);
        }


        // Adding a song to an artist
        // POST: api/artists/{artistId}/songs/{songId}
        [HttpPost]
        [Route("{artistId}/songs/{songId}")]
        public async Task<IActionResult> AddSongToArtist(int artistId, int songId)
        {
            var artist = await _artist.GetByIdAsync(artistId);
            if (artist == null)
            {
                return NotFound();
            }

            var song = await _song.GetByIdAsync(songId);
            if (song == null)
            {
                return NotFound();
            }

            song.ArtistId = artistId;
            await _song.UpdateAsync(songId, song);

            return Ok(song);
        }

        // Retrieve all songs by an artist
        // GET: api/artists/{artistId}/songs
        [HttpGet("{artistId}/songs")]
        public async Task<ActionResult<IEnumerable<Song>>> GetSongsByArtist(int artistId)
        {
            var artist = await _artist.GetByIdAsync(artistId);
            if (artist == null)
            {
                return NotFound();
            }

            // Assuming you have a method in ISong to get songs by artist ID
            var songs = await _song.GetSongsByArtistAsync(artistId);
            return Ok(songs);
        }

    }
}
