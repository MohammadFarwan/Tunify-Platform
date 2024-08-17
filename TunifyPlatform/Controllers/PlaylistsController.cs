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
    public class PlaylistsController : ControllerBase
    {
        private readonly IPlayList _playlist;

        public PlaylistsController(IPlayList Playlist)
        {
            _playlist = Playlist;
        }

        // GET: api/Playlists
        [Route("/playlists/getAll")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Playlist>>> GetPlaylists()
        {
            return await _playlist.GetAllAsync();
        }

        // GET: api/Playlists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Playlist>> GetPlaylist(int id)
        {
            return await _playlist.GetByIdAsync(id);
        }

        // PUT: api/Playlists/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlaylist(int id, Playlist Playlist)
        {
            var updatePlaylist = await _playlist.UpdateAsync(id, Playlist);
            return Ok(updatePlaylist);
        }

        // POST: api/Playlists
        [HttpPost]
        public async Task<ActionResult<Playlist>> PostPlaylist(Playlist Playlist)
        {
            var newPlaylist = await _playlist.InsertAsync(Playlist);
            return Ok(newPlaylist);
        }

        // DELETE: api/Playlists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlaylist(int id)
        {
            var deletedEmployee = _playlist.DeleteAsync(id);
            return Ok(deletedEmployee);
        }
    }
}
