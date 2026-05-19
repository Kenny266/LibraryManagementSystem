namespace LIBRARYMANAGEMENT.API.Models;

public enum LoanStatus
{
    Borrowed,
    Returned,
    Overdue,
    Lost
}

public class Loan
{
    public int LoanId { get; set; }
    public int MemberId { get; set; }
    public int BookId { get; set; }
    public DateTime LoanDate { get; set; } = DateTime.UtcNow;
    public DateTime DueDate { get; set; } = DateTime.UtcNow.AddDays(14);
    public DateTime? ReturnDate { get; set; }
    public LoanStatus Status { get; set; } = LoanStatus.Borrowed;
    public decimal FineAmount { get; set; }

    public Member Member { get; set; } = null!;
    public Book Book { get; set; } = null!;
}