using System.ComponentModel.DataAnnotations;

namespace LibraryMgmt.API.DTOs;

public class BookDto
{
    public int BookId { get; set; }

    [Required(ErrorMessage = "Title is required")]
    [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Author is required")]
    [StringLength(100, ErrorMessage = "Author cannot exceed 100 characters")]
    public string Author { get; set; } = string.Empty;

    [Required(ErrorMessage = "ISBN is required")]
    [StringLength(20, ErrorMessage = "ISBN cannot exceed 20 characters")]
    public string ISBN { get; set; } = string.Empty;

    [Required(ErrorMessage = "Category is required")]
    [StringLength(50, ErrorMessage = "Category cannot exceed 50 characters")]
    public string Category { get; set; } = string.Empty;

    [Required(ErrorMessage = "Publisher is required")]
    [StringLength(100, ErrorMessage = "Publisher cannot exceed 100 characters")]
    public string Publisher { get; set; } = string.Empty;

    [Required(ErrorMessage = "Year Published is required")]
    [Range(1000, 2100, ErrorMessage = "Year Published must be between 1000 and 2100")]
    public int YearPublished { get; set; }

    [Required(ErrorMessage = "Total Copies is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Total Copies must be at least 1")]
    public int TotalCopies { get; set; }

    [Required(ErrorMessage = "Available Copies is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Available Copies cannot be negative")]
    public int AvailableCopies { get; set; }

    public DateTime DateAdded { get; set; } = DateTime.UtcNow;
}

public class CreateBookDto
{
    [Required(ErrorMessage = "Title is required")]
    [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Author is required")]
    [StringLength(100, ErrorMessage = "Author cannot exceed 100 characters")]
    public string Author { get; set; } = string.Empty;

    [Required(ErrorMessage = "ISBN is required")]
    [StringLength(20, ErrorMessage = "ISBN cannot exceed 20 characters")]
    public string ISBN { get; set; } = string.Empty;

    [Required(ErrorMessage = "Category is required")]
    [StringLength(50, ErrorMessage = "Category cannot exceed 50 characters")]
    public string Category { get; set; } = string.Empty;

    [Required(ErrorMessage = "Publisher is required")]
    [StringLength(100, ErrorMessage = "Publisher cannot exceed 100 characters")]
    public string Publisher { get; set; } = string.Empty;

    [Required(ErrorMessage = "Year Published is required")]
    [Range(1000, 2100, ErrorMessage = "Year Published must be between 1000 and 2100")]
    public int YearPublished { get; set; }

    [Required(ErrorMessage = "Total Copies is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Total Copies must be at least 1")]
    public int TotalCopies { get; set; }

    [Required(ErrorMessage = "Available Copies is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Available Copies cannot be negative")]
    public int AvailableCopies { get; set; }
}

public class UpdateBookDto
{
    [Required(ErrorMessage = "Title is required")]
    [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Author is required")]
    [StringLength(100, ErrorMessage = "Author cannot exceed 100 characters")]
    public string Author { get; set; } = string.Empty;

    [Required(ErrorMessage = "ISBN is required")]
    [StringLength(20, ErrorMessage = "ISBN cannot exceed 20 characters")]
    public string ISBN { get; set; } = string.Empty;

    [Required(ErrorMessage = "Category is required")]
    [StringLength(50, ErrorMessage = "Category cannot exceed 50 characters")]
    public string Category { get; set; } = string.Empty;

    [Required(ErrorMessage = "Publisher is required")]
    [StringLength(100, ErrorMessage = "Publisher cannot exceed 100 characters")]
    public string Publisher { get; set; } = string.Empty;

    [Required(ErrorMessage = "Year Published is required")]
    [Range(1000, 2100, ErrorMessage = "Year Published must be between 1000 and 2100")]
    public int YearPublished { get; set; }

    [Required(ErrorMessage = "Total Copies is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Total Copies must be at least 1")]
    public int TotalCopies { get; set; }

    [Required(ErrorMessage = "Available Copies is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Available Copies cannot be negative")]
    public int AvailableCopies { get; set; }
}