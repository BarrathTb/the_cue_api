using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
[Route("[controller]")]
public class EventsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public EventsController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("events"), SwaggerOperation(summary: "return entire event collection", null)]
    public async Task<IEnumerable<Event>> GetEvent()
    {
        return await _context.Events.ToListAsync();
    }

    [HttpPost, SwaggerOperation(summary: "Add new event to collection", null)]
    public async Task PostEvent([FromBody] Event eventItem)
    {
        _context.Events.Add(eventItem);
        await _context.SaveChangesAsync();
    }

    [HttpPut, SwaggerOperation(summary: "Update event in collection", null)]
    public async Task PutEvent([FromBody] Event eventItem)
    {
        _context.Events.Update(eventItem);
        await _context.SaveChangesAsync();
    }

    [HttpDelete, SwaggerOperation(summary: "Delete event from collection", null)]
    public async Task DeleteEvent([FromBody] Event eventItem)
    {
        _context.Events.Remove(eventItem);
        await _context.SaveChangesAsync();

    }
}
