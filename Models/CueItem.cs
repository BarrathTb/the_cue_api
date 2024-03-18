using Microsoft.AspNetCore.Identity;
public class CueItem
{
    public int Id { get; set; }
    public int SongId { get; set; }
    public virtual Song Song { get; set; }
    public virtual Request Request { get; set; }

    public DateTime RequestedTime { get; set; }
    // Other properties such as priority...
}