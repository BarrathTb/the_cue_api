using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
[Route("[controller]")]
public class PlaylistSongsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PlaylistSongsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet, SwaggerOperation(summary: "return entire playlist song collection", null)]
    public async Task<IEnumerable<PlaylistSongs>> GetPlaylistSong()
    {
        return await _context.PlaylistSongs.ToListAsync();
    }

    [HttpPost, SwaggerOperation(summary: "Add new playlist song to collection", null)]
    public async Task PostPlaylistSongs([FromBody] PlaylistSongs playlistSongs)
    {
        _context.PlaylistSongs.Add(playlistSongs);
        await _context.SaveChangesAsync();
    }

    //put
    [HttpPut, SwaggerOperation(summary: "Update existing playlist song in collection", null)]
    public async Task PutPlaylistSongs([FromBody] PlaylistSongs playlistSongs)
    {
        _context.PlaylistSongs.Update(playlistSongs);
        await _context.SaveChangesAsync();
    }

    //delete
    [HttpDelete, SwaggerOperation(summary: "Delete existing playlist song from collection", null)]
    public async Task DeletePlaylistSongs([FromBody] PlaylistSongs playlistSongs)
    {
        _context.PlaylistSongs.Remove(playlistSongs);
        await _context.SaveChangesAsync();

    }
}