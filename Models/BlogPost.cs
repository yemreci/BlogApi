namespace BlogApi.Models;

public class BlogPost
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Content { get; set; }
    public DateTime DateTime { get; set; }
}