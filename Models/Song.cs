using System.ComponentModel.DataAnnotations;
using TagLib;

public class Song
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }

    public TimeSpan Duration { get; set; }

    public string Album { get; set; }
    [Required]
    public string Artist { get; set; }
    public byte[] AlbumCover { get; set; }

    [Required]
    public int GenreId { get; set; }

    public int ReleaseYear { get; set; }
    public int Rating { get; set; }

    public int Priority { get; set; }
    public DateTime TimeStamp { get; set; }

    // Additional properties for tracking play count and last played date
    public int PlayCount { get; set; }
    public DateTime LastPlayed { get; set; }

    public string Path { get; set; }

    public virtual ICollection<Genre> Genres { get; set; }

    public virtual ICollection<PlaylistSongs> PlaylistSongs { get; set; }
    public virtual ICollection<Artist> Artists { get; set; }
}

