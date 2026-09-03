namespace BasicAuthApi.Models;

public class Todo
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public User? User { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
}
