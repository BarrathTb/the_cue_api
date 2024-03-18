using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

[ApiController]
[Route("[controller]")]
public class GenreController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    public GenreController(ApplicationDbContext context)
    {
        _context = context;
    }
    // GET: api/genre
    /// <summary>
    /// Retrieves all genres.
    /// </summary>
    /// <returns>A list of genres.</returns>
    /// <response code="200">Returns the list of genres</response>
    [HttpGet, SwaggerOperation(summary: "Retrieve all genres", null)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Genre>>> GetGenres()
    {
        return Ok(await _context.Genres.ToListAsync());
    }
    // POST: api/genre
    /// <summary>
    /// Creates a new genre.
    /// </summary>
    /// <param name="genre">The genre to create.</param>
    /// <returns>A newly created genre.</returns>
    /// <response code="201">Returns the newly created genre</response>
    /// <response code="400">If the genre is null or invalid</response>            
    [HttpPost, SwaggerOperation(summary: "Creates a new genre", null)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PostGenre([FromBody] Genre genre)
    {
        _context.Genres.Add(genre);
        await _context.SaveChangesAsync();
        return CreatedAtAction("GetGenre", new { id = genre.GenreId }, genre);
    }
    // PUT: api/genre
    /// <summary>
    /// Updates an existing genre.
    /// </summary>
    /// <param name="genre">The genre to update.</param>
    /// <response code="204">No Content, if the update is successful</response>
    /// <response code="400">If the genre is null or invalid</response>
    [HttpPut, SwaggerOperation(summary: "Updates an existing genre", null)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutGenre([FromBody] Genre genre)
    {
        _context.Genres.Update(genre);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/genre
    /// <summary>
    /// Deletes an existing genre.
    /// </summary>
    /// <param name="genre">The genre to delete.</param>
    /// <response code="204">No Content, if the deletion is successful</response>
    [HttpDelete, SwaggerOperation(summary: "Deletes an existing genre", null)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteGenre([FromBody] Genre genre)
    {
        _context.Genres.Remove(genre);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // GET: api/genre/{id}
    /// <summary>
    /// Retrieves a specific genre by unique ID.
    /// </summary>
    /// <param name="id">The ID of the genre.</param>
    /// <returns>A genre with the specified ID.</returns>
    /// <response code="200">Returns the genre with the specified ID</response>
    /// <response code="404">If the genre does not exist</response>
    [HttpGet("{id}"), SwaggerOperation(summary: "Retrieve a specific genre by unique ID", null)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Genre>> GetGenre(int id)
    {
        var genre = await _context.Genres.FindAsync(id);

        if (genre == null)
        {
            return NotFound();
        }

        return Ok(genre);
    }

    // PUT: api/genre/{id}
    /// <summary>
    /// Updates a specific genre by unique ID.
    /// </summary>
    /// <param name="id">The ID of the genre to update.</param>
    /// <param name="genre">The updated genre object.</param>
    /// <response code="204">No Content, if the update is successful</response>
    /// <response code="400">If the genre is null, ids do not match or it's invalid</response>
    /// <response code="404">If the genre to be updated does not exist</response>
    [HttpPut("{id}"), SwaggerOperation(summary: "Update a genre by id from collection", null)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> PutGenre(int id, [FromBody] Genre genre)
    {
        if (id != genre.GenreId)
        {
            return BadRequest("ID mismatch");
        }

        _context.Entry(genre).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Genres.Any(e => e.GenreId == id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/genre/{id}
    /// <summary>
    /// Deletes a specific genre by unique ID.
    /// </summary>
    /// <param name="id">The ID of the genre to delete.</param>
    /// <response code="204">No Content, if the deletion is successful</response>
    /// <response code="404">If the genre does not exist</response>
    [HttpDelete("{id}"), SwaggerOperation(summary: "Delete a genre by id from collection", null)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteGenre(int id)
    {
        var genre = await _context.Genres.FindAsync(id);
        if (genre == null)
        {
            return NotFound();
        }

        _context.Genres.Remove(genre);
        await _context.SaveChangesAsync();
        return NoContent();
    }
    // GET: api/genre/5/songs
    /// <summary>
    /// Retrieves all songs for a specific genre.
    /// </summary>
    /// <param name="id">The ID of the genre.</param>
    /// <returns>A list of songs from the specified genre.</returns>
    /// <response code="200">Returns the list of songs</response>
    /// <response code="404">If the genre does not exist</response>
    [HttpGet("{id}/songs"), SwaggerOperation(summary: "Retrieves all songs for a specific genre.", null)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<Song>>> GetGenreSongs(int id)
    {
        return await _context.Songs.Where(x => x.GenreId == id).ToListAsync();
    }
}