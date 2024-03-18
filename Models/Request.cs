
using System.ComponentModel.DataAnnotations;

public class Request
{
    [Required]
    public int RequestId { get; set; }

    [Required]
    public int SongId { get; set; }
    public DateTime Timestamp { get; set; }
    // Additional request-related properties...
}
