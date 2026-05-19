using LIBRARYMANAGEMENT.API.Data;
using LIBRARYMANAGEMENT.API.DTOs;
using LIBRARYMANAGEMENT.API.Models;
using Microsoft.EntityFrameworkCore;

namespace LIBRARYMANAGEMENT.API.Services;

public class LoansService : ILoansService
{
    private readonly LibraryDbContext _context;

    public LoansService(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<List<LoanDto>> GetAllLoansAsync()
    {
        var loans = await _context.Loans
            .Include(l => l.Member)
            .Include(l => l.Book)
            .ToListAsync();

        return loans.Select(MapToDto).ToList();
    }

    public async Task<List<LoanDto>> GetActiveLoansAsync()
    {
        var loans = await _context.Loans
            .Include(l => l.Member)
            .Include(l => l.Book)
            .Where(l => l.ReturnDate == null)
            .ToListAsync();

        return loans.Select(MapToDto).ToList();
    }

    public async Task<List<LoanDto>> GetOverdueLoansAsync()
    {
        var loans = await _context.Loans
            .Include(l => l.Member)
            .Include(l => l.Book)
            .Where(l => l.ReturnDate == null && l.DueDate < DateTime.UtcNow)
            .ToListAsync();

        return loans.Select(MapToDto).ToList();
    }

    public async Task<LoanDto?> GetLoanByIdAsync(int id)
    {
        var loan = await _context.Loans
            .Include(l => l.Member)
            .Include(l => l.Book)
            .FirstOrDefaultAsync(l => l.LoanId == id);

        return loan == null ? null : MapToDto(loan);
    }

    public async Task<LoanDto> CreateLoanAsync(CreateLoanDto newLoanDto)
    {
        var member = await _context.Members.FindAsync(newLoanDto.MemberId);
        if (member == null)
        {
            throw new InvalidOperationException("Member not found.");
        }

        if (member.Status != MemberStatus.Active)
        {
            throw new InvalidOperationException("Only active members may borrow books.");
        }

        var activeLoanCount = await _context.Loans.CountAsync(l => l.MemberId == newLoanDto.MemberId && l.ReturnDate == null);
        if (activeLoanCount >= 3)
        {
            throw new InvalidOperationException("Member has reached the maximum of 3 active loans.");
        }

        var book = await _context.Books.FindAsync(newLoanDto.BookId);
        if (book == null || book.AvailableCopies <= 0)
        {
            throw new InvalidOperationException("Book is not available for loan.");
        }

        var existingLoan = await _context.Loans
            .FirstOrDefaultAsync(l => l.MemberId == newLoanDto.MemberId && l.BookId == newLoanDto.BookId && l.ReturnDate == null);

        if (existingLoan != null)
        {
            throw new InvalidOperationException("Member already has an active loan for this book.");
        }

        var loan = new Loan
        {
            MemberId = newLoanDto.MemberId,
            BookId = newLoanDto.BookId,
            LoanDate = DateTime.UtcNow,
            DueDate = DateTime.UtcNow.AddDays(14),
            Status = LoanStatus.Borrowed,
            FineAmount = 0m
        };

        _context.Loans.Add(loan);
        book.AvailableCopies--;
        await _context.SaveChangesAsync();

        await _context.Entry(loan).Reference(l => l.Member).LoadAsync();
        await _context.Entry(loan).Reference(l => l.Book).LoadAsync();

        return MapToDto(loan);
    }

    public async Task<bool> ReturnLoanAsync(int id, ReturnLoanDto returnLoanDto)
    {
        var loan = await _context.Loans
            .Include(l => l.Book)
            .Include(l => l.Member)
            .FirstOrDefaultAsync(l => l.LoanId == id);

        if (loan == null || loan.ReturnDate.HasValue)
        {
            return false;
        }

        loan.ReturnDate = returnLoanDto.ReturnDate;
        loan.Book.AvailableCopies++;

        if (loan.ReturnDate > loan.DueDate)
        {
            var daysLate = (loan.ReturnDate.Value.Date - loan.DueDate.Date).Days;
            loan.FineAmount = daysLate * 1.00m;
            loan.Status = LoanStatus.Overdue;
        }
        else
        {
            loan.Status = LoanStatus.Returned;
            loan.FineAmount = 0m;
        }

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteLoanAsync(int id)
    {
        var loan = await _context.Loans.FindAsync(id);
        if (loan == null)
        {
            return false;
        }

        _context.Loans.Remove(loan);
        await _context.SaveChangesAsync();
        return true;
    }

    private static LoanDto MapToDto(Loan loan)
    {
        return new LoanDto
        {
            LoanId = loan.LoanId,
            MemberId = loan.MemberId,
            BookId = loan.BookId,
            LoanDate = loan.LoanDate,
            DueDate = loan.DueDate,
            ReturnDate = loan.ReturnDate,
            Status = loan.Status,
            FineAmount = loan.FineAmount,
            MemberName = $"{loan.Member.FirstName} {loan.Member.LastName}",
            BookTitle = loan.Book.Title
        };
    }
}