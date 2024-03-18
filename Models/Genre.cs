//create a model for song genres

public class Genre
{
    public int GenreId { get; set; }
    public string GenreName { get; set; }
    public virtual ICollection<Song> Songs { get; set; }

    // Playlists property here suggests that each Genre has zero or more Playlists.
    // Ensure that there's a valid reason to include this association directly.
    public virtual ICollection<Playlist> Playlists { get; set; }
}
