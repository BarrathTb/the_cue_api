using System.ComponentModel.DataAnnotations;

public class Event
{
    [Required]
    public int EventId { get; set; }
    [Required]
    public string Name { get; set; }
    public DateTime Date { get; set; }
    public string Location { get; set; }
}