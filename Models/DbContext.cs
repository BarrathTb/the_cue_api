using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Song> Songs { get; set; }

    public DbSet<Playlist> Playlists { get; set; }
    public DbSet<PlaylistSongs> PlaylistSongs { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Request> Requests { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<LibraryScanRequest> LibraryScanRequests { get; set; }



}

