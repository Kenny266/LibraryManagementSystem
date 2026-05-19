using LIBRARYMANAGEMENT.API.DTOs;

namespace LIBRARYMANAGEMENT.API.Services;

public interface ILoansService
{
    Task<List<LoanDto>> GetAllLoansAsync();
    Task<List<LoanDto>> GetActiveLoansAsync();
    Task<List<LoanDto>> GetOverdueLoansAsync();
    Task<LoanDto?> GetLoanByIdAsync(int id);
    Task<LoanDto> CreateLoanAsync(CreateLoanDto newLoan);
    Task<bool> ReturnLoanAsync(int id, ReturnLoanDto returnLoan);
    Task<bool> DeleteLoanAsync(int id);
}