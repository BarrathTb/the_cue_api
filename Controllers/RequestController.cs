using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
[Route("[controller]")]
public class RequestController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public RequestController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet, SwaggerOperation(summary: "return entire song request collection", null)]
    public async Task<IEnumerable<Request>> GetSongRequest()
    {
        return await _context.Requests.ToListAsync();
    }

    [HttpPost, SwaggerOperation(summary: "Add new song request to collection", null)]
    public async Task RequestSong([FromBody] Request request)
    {
        _context.Requests.Add(request);
        await _context.SaveChangesAsync();
    }

    //put
    [HttpPut, SwaggerOperation(summary: "Update song request in collection", null)]
    public async Task UpdateSongRequest([FromBody] Request request)
    {
        _context.Requests.Update(request);
        await _context.SaveChangesAsync();

    }

    //delete
    [HttpDelete, SwaggerOperation(summary: "Delete song request from collection", null)]
    public async Task DeleteSongRequest([FromBody] Request request)
    {
        _context.Requests.Remove(request);
        await _context.SaveChangesAsync();

    }
}
