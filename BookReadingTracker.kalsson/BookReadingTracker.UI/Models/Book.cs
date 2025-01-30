namespace BookReadingTracker.UI.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int PagesRead { get; set; }
    public int TotalPages { get; set; }
}