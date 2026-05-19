using LIBRARYMANAGEMENT.API.Models;
using System.ComponentModel.DataAnnotations;

namespace LIBRARYMANAGEMENT.API.DTOs;

public class LoanDto
{
    public int LoanId { get; set; }
    public int MemberId { get; set; }
    public int BookId { get; set; }
    public DateTime LoanDate { get; set; } = DateTime.UtcNow;
    public DateTime DueDate { get; set; } = DateTime.UtcNow.AddDays(14);
    public DateTime? ReturnDate { get; set; }
    public LoanStatus Status { get; set; }
    public decimal FineAmount { get; set; }

    public string MemberName { get; set; } = string.Empty;
    public string BookTitle { get; set; } = string.Empty;
}

public class CreateLoanDto
{
    [Required(ErrorMessage = "Member ID is required")]
    public int MemberId { get; set; }

    [Required(ErrorMessage = "Book ID is required")]
    public int BookId { get; set; }
}

public class ReturnLoanDto
{
    [Required(ErrorMessage = "Return date is required")]
    public DateTime ReturnDate { get; set; }
}