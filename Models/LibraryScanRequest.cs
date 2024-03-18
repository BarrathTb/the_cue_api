using System.ComponentModel.DataAnnotations;

public class LibraryScanRequest
{
  [Key]
  public int Id { get; set; }
  public string FolderPath { get; set; }
}