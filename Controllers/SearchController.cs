using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
[Route("api/[controller]")]
public class SearchController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SearchController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET api/search/songs?keyword=abc
    [HttpGet("songs"), SwaggerOperation(summary: "Search for songs based on keyword", null)]
    public async Task<ActionResult<IEnumerable<Song>>> GetSongsByKeyword(string keyword)
    {
        var songs = await _context.Songs
            .Where(s => s.Title.Contains(keyword) || s.Artist.Contains(keyword) || s.Album.Contains(keyword))
            .ToListAsync();
        return songs;
    }

    // Additional methods for serachng songs by genre
    // GET api/search/songs?genre=rock
    [HttpGet("genre={genre}"), SwaggerOperation(summary: "Search for songs based on genre", null)]
    public async Task<ActionResult<IEnumerable<Song>>> GetSongsByGenre(string genre)
    {
        var songs = await _context.Songs
          .Where(s => s.Genres.Any(g => g.GenreName == genre))
          .ToListAsync();
        return songs;
    }

    // search for songs by artist
    // GET api/search/songs?artist=abc
    [HttpGet("artist={artist}"), SwaggerOperation(summary: "Search for songs based on artist", null)]
    public async Task<ActionResult<IEnumerable<Song>>> GetSongsByArtist(string artist)
    {
        var songs = await _context.Songs
        .Where(s => s.Artist.Contains(artist))
        .ToListAsync();
        return songs;
    }
    // search for songs by album

    // GET api/search/songs?album=abc
    [HttpGet("album={album}"), SwaggerOperation(summary: "Search for songs based on album", null)]
    public async Task<ActionResult<IEnumerable<Song>>> GetSongsByAlbum(string album)
    {
        var songs = await _context.Songs
      .Where(s => s.Album.Contains(album))


     .ToListAsync();
        return songs;
    }
    // additional 
}