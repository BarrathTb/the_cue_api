using System.ComponentModel.DataAnnotations;

public class Playlist
{
    [Required]
    public int PlaylistSongId { get; set; }
    [Required]
    public int PlaylistId { get; set; }
    [Required]
    public int SongId { get; set; }
}