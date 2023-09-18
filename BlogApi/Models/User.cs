using Microsoft.Extensions.Hosting;

namespace BlogApi.Models;
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<BlogPost> BlogPosts { get; } = new List<BlogPost>();
}
