using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;
using Swashbuckle.AspNetCore.Annotations;

namespace TheCueSongAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class SongsController : ControllerBase
    {
        // Application DB Context
        private readonly ApplicationDbContext _context;


        // Controller Constructor
        public SongsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/<SongsController>
        // Method to get all songs
        [HttpGet, SwaggerOperation(summary: "Get all songs in selected library", null)]
        public async Task<IEnumerable<Song>> Get()
        {
            return await _context.Songs.ToListAsync();
        }

        // POST api/<SongsController>
        // Method to add a new song
        [HttpPost, SwaggerOperation(summary: "Add a new song to the selected library", null)]
        public async Task Post([FromBody] Song value)
        {
            _context.Songs.Add(value);
            await _context.SaveChangesAsync();
        }

        // GET api/<SongsController>/{id}
        // Method to get song details using TagLib
        [HttpGet("{id}"), SwaggerOperation(summary: "Get song by id and its meta data", null)]
        public async Task<IActionResult> GetSongInfo(int id)
        {
            var song = await _context.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }

            try
            {
                var file = TagLib.File.Create(song.Path);

                var title = file.Tag.Title;
                var album = file.Tag.Album;
                var duration = file.Properties.Duration;
                var artists = file.Tag.Performers;

                var image = file.Tag.Pictures.FirstOrDefault();
                byte[] imageData = image?.Data.Data;

                return Ok(new
                {
                    Title = title,
                    Album = album,
                    Duration = duration.TotalSeconds,
                    Artists = artists,
                    Image = imageData == null ? null : Convert.ToBase64String(imageData)
                });
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                // Return a generic error message
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        // PUT api/<SongsController>/priority/{id}
        // Method to maintain song order priority
        [HttpPut("priority/{id}"), SwaggerOperation(summary: "Maintain requested song order priorities", null)]
        public async Task<IActionResult> Priority(int id)
        {
            var songToPrioritize = await _context.Songs.FindAsync(id);

            if (songToPrioritize == null)
            {
                return NotFound();
            }

            var highPrioritySongs = await _context.Songs.Where(s => s.Priority <= 1).OrderBy(s => s.TimeStamp).ToListAsync();

            // If there are any songs with priority less than or equal to 1
            if (highPrioritySongs.Any())
            {
                // Shift every song's priority down by one
                await IncrementSongPriorities();

                // Get the minimum priority value amongst the highest priority songs (+0.01 to differentiate multiple priority-1 songs based on timestamp)
                var minPriority = highPrioritySongs.Min(hps => hps.Priority);

                // Set desired song's priority based on necessary conditions
                songToPrioritize.Priority = songToPrioritize.TimeStamp > highPrioritySongs.First().TimeStamp ? minPriority + 1 : minPriority - 1;
            }
            else // If no song currently has highest priority, make the desired song as the highest priority
            {
                await IncrementSongPriorities();

                songToPrioritize.Priority = 1;
            }

            songToPrioritize.TimeStamp = DateTime.UtcNow;    // Update timestamp

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Method to increment song priorities
        [HttpPost, Route("priority/increment"), SwaggerOperation(summary: "Increment song priorities", null)]
        public async Task<IActionResult> IncrementSongPriorities()
        {
            var songs = await _context.Songs.ToListAsync();
            foreach (var song in songs)
            {
                song.Priority += 1;
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // GET api/<SongsController>/{id}
        // Method to get a song and update PlayCount and LastPlayed properties
        [HttpGet("{id}/song"), SwaggerOperation(summary: "Get a song and update PlayCount and LastPlayed properties", null)]
        public async Task<IActionResult> GetSong([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var song = await _context.Songs.FindAsync(id);

            if (song == null)
            {
                return NotFound();
            }

            // Increment play count and set last played date/time
            song.PlayCount++;
            song.LastPlayed = DateTime.Now;

            // Save changes in DB
            await _context.SaveChangesAsync();

            return Ok(song);
        }

        // POST: api/Songs/scan-library
        [HttpPost("scan-library"), SwaggerOperation(summary: "Scan library for new songs", null)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ScanLibrary([FromBody] LibraryScanRequest scanRequest)
        {
            if (Directory.Exists(scanRequest.FolderPath))
            {
                try
                {
                    var musicFiles = Directory.EnumerateFiles(scanRequest.FolderPath, "*.*", SearchOption.AllDirectories)
                                              .Where(s => s.EndsWith(".mp3") || s.EndsWith(".flac") || s.EndsWith(".wav"));

                    foreach (var filePath in musicFiles)
                    {
                        using (var file = TagLib.File.Create(filePath))
                        {
                            var title = file.Tag.Title ?? "Unknown Title";
                            var album = file.Tag.Album ?? "Unknown Album";
                            var artist = file.Tag.FirstPerformer ?? "Unknown Artist"; // Assuming single artist for simplicity.

                            var duration = file.Properties.Duration;
                            var albumCoverData = file.Tag.Pictures.Length > 0 ? file.Tag.Pictures[0].Data.Data : null;
                            var genre = file.Tag.FirstGenre ?? "Unknown Genre";
                            var releaseYear = (int)file.Tag.Year;

                            Song song = new Song
                            {
                                Title = title,
                                Duration = duration,
                                Album = album,
                                Artist = artist,
                                AlbumCover = albumCoverData,
                                GenreId = await GetOrCreateGenreId(genre), // Implement this method based on your application logic.
                                ReleaseYear = releaseYear,
                                Rating = 0, // Default value or extract if available.
                                Priority = 0, // Default value or implementation dependent.
                                TimeStamp = DateTime.Now,
                                PlayCount = 0, // Default value.
                                LastPlayed = DateTime.MinValue, // Default value, indicates never played.
                                Path = filePath
                            };

                            bool songExists = await _context.Songs.AnyAsync(s => s.Path == filePath);
                            if (!songExists)
                            {
                                await _context.Songs.AddAsync(song);
                            }
                        }
                    }

                    await _context.SaveChangesAsync();
                    return Ok("Library scanned and updated successfully.");
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"An error occurred while scanning the library: {ex.Message}");
                }
            }
            else
            {
                return BadRequest("Invalid folder path");
            }
        }

        private async Task<int> GetOrCreateGenreId(string genreName)
        {
            // Check if the genre already exists in the database
            var existingGenre = await _context.Genres.FirstOrDefaultAsync(g => g.GenreName == genreName);

            if (existingGenre != null)
            {
                // If the genre exists, return its ID
                return existingGenre.GenreId;
            }
            else
            {
                // If the genre does not exist, create a new Genre entity
                var newGenre = new Genre { GenreName = genreName };
                await _context.Genres.AddAsync(newGenre);
                await _context.SaveChangesAsync(); // Save changes so the new Genre gets an ID

                // Return the new genre's ID
                return newGenre.GenreId;
            }
        }

    }
}
