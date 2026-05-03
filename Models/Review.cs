namespace CarwashBackend.Models;

public class Review
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string Text { get; set; } = string.Empty;
    public bool IsApproved { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
