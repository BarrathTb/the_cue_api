using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.EntityFrameworkCore;
using TagLib;
using TheCueSongAPI.Hubs;
using Microsoft.AspNetCore.SignalR;
namespace TheCueSongAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class APIController : ControllerBase
    {
        // Application DB Context
        private ApplicationDbContext _context;
        private readonly IHubContext<CueHub> _hubContext;
        public APIController(ApplicationDbContext context, IHubContext<CueHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        [HttpGet("songs"), SwaggerOperation(summary: "return entire song collection by title", null)]
        // returns all songs in the database
        public IEnumerable<Song> Get() => _context.Songs.OrderBy(p => p.Title);

        [HttpGet("songs/{songId}"), SwaggerOperation(summary: "return specific song from collection by id", null)]
        //returns a specific song
        public Song GetSong(int songId) => _context.Songs.FirstOrDefault(s => s.Id == songId);

        [HttpGet("genre"), SwaggerOperation(summary: "return entire all genres in db", null)]
        // returns all genres in the database
        public IEnumerable<Genre> GetGenre() => _context.Genres.Include("Songs").OrderBy(c => c.GenreName);

        [HttpGet("genres/{genreId}/songs"), SwaggerOperation(summary: "return all songs in a specific category", null)]
        // returns all songs in a specific genre
        public IEnumerable<Song> GetByGenre(int GenreId) => _context.Songs.Where(s => s.GenreId == GenreId).Include("Songs").OrderBy(c => c.GenreId);
    }


}