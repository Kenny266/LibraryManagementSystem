namespace LibraryMgmt.API.Models;

public class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public string ISBN { get; set; } = string.Empty;

    public string Category { get; set; } = string.Empty;

    public string Publisher { get; set; } = string.Empty;

    public int YearPublished { get; set; }

    public int TotalCopies { get; set; }

    public int AvailableCopies { get; set; }

    public DateTime DateAdded { get; set; } = DateTime.UtcNow;
}