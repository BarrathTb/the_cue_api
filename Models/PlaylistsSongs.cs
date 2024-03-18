using System.ComponentModel.DataAnnotations;

public class PlaylistSongs
{
    [Key] // This denotes the primary key
    public int PlaylistSongId { get; set; }

    [Required]
    public int PlaylistId { get; set; }
    public virtual Playlist Playlist { get; set; }

    [Required]
    public int SongId { get; set; }
    public virtual Song Song { get; set; }
}
