namespace PlanerApp.Models;

public class TodoTask
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Status { get; set; } = "Nowe";
    public string? Category { get; set; }
    public DateTime? DueDate { get; set; }

    public bool IsDone
    {
        get => Status == "Gotowe";
        set => Status = value ? "Gotowe" : "Nowe";
    }
}