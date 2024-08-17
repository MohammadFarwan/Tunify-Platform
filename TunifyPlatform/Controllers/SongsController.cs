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
    public class SongsController : ControllerBase
    {
        private readonly ISong _song;

        public SongsController(ISong Song)
        {
            _song = Song;
        }

        // GET: api/Songs
        [Route("/songs/getAll")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Song>>> GetSongs()
        {
            return await _song.GetAllAsync();
        }

        // GET: api/Songs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Song>> GetSong(int id)
        {
            return await _song.GetByIdAsync(id);
        }

        // PUT: api/Songs/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSong(int id, Song Song)
        {
            var updateSong = await _song.UpdateAsync(id, Song);
            return Ok(updateSong);
        }

        // POST: api/Songs
        [HttpPost]
        public async Task<ActionResult<Song>> PostSong(Song Song)
        {
            var newSong = await _song.InsertAsync(Song);
            return Ok(newSong);
        }

        // DELETE: api/Songs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(int id)
        {
            var deletedEmployee = _song.DeleteAsync(id);
            return Ok(deletedEmployee);
        }
    }
}
