namespace Project.WebApi.Entities.Models;

public class Article
{
  public int Id { get; set; }
  
  public string? Title { get; set; }

  public string? Author { get; set; }

  public string? Content { get; set; }
  
  public DateTime? Date { get; set; }

  public string? Category { get; set; }
}