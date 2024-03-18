using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
[Route("[controller]")]
public class PlaylistsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PlaylistsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet, SwaggerOperation(summary: "return entire playlist collection", null)]
    public async Task<IEnumerable<Playlist>> GetPlaylist()
    {
        return await _context.Playlists.ToListAsync();
    }

    [HttpPost, SwaggerOperation(summary: "Add new playlist to collection", null)]
    public async Task PostPlaylist([FromBody] Playlist playlist)
    {
        _context.Playlists.Add(playlist);
        await _context.SaveChangesAsync();
    }

    [HttpPut, SwaggerOperation(summary: "Update existing playlist", null)]
    public async Task PutPlaylist([FromBody] Playlist playlist)
    {
        _context.Playlists.Update(playlist);
        await _context.SaveChangesAsync();
    }

    [HttpDelete, SwaggerOperation(summary: "Delete existing playlist", null)]
    public async Task DeletePlaylist([FromBody] Playlist playlist)
    {
        _context.Playlists.Remove(playlist);
        await _context.SaveChangesAsync();

    }
}
