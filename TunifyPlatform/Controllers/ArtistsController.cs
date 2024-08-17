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

        public ArtistsController(IArtist Artist)
        {
            _artist = Artist;
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
    }
}
