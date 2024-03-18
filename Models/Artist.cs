using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Artist
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    // Navigation property for related Songs (One-to-Many relationship)
    public virtual ICollection<Song> Songs { get; set; }

    // Navigation property for related Albums (One-to-Many relationship)
    //    public virtual ICollection<Album> Albums { get; set; }

    // Add any additional fields that are relevant to your application...

    public virtual ICollection<CueItem> CueItems { get; set; }
    public virtual ICollection<Playlist> Playlist { get; set; }
}
