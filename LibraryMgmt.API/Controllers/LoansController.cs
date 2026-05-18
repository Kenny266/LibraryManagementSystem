using LibraryMgmt.API.DTOs;
using LibraryMgmt.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMgmt.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class LoansController : ControllerBase
{
    private readonly ILoansService _loansService;

    public LoansController(ILoansService loansService)
    {
        _loansService = loansService;
    }

    [HttpGet]
    public async Task<ActionResult<List<LoanDto>>> GetLoans()
    {
        var loans = await _loansService.GetAllLoansAsync();
        return Ok(loans);
    }

    [HttpGet("active")]
    public async Task<ActionResult<List<LoanDto>>> GetActiveLoans()
    {
        var loans = await _loansService.GetActiveLoansAsync();
        return Ok(loans);
    }

    [HttpGet("overdue")]
    public async Task<ActionResult<List<LoanDto>>> GetOverdueLoans()
    {
        var loans = await _loansService.GetOverdueLoansAsync();
        return Ok(loans);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<LoanDto>> GetLoan(int id)
    {
        var loan = await _loansService.GetLoanByIdAsync(id);
        if (loan == null)
        {
            return NotFound();
        }
        return Ok(loan);
    }

    [HttpPost]
    public async Task<ActionResult<LoanDto>> CreateLoan(CreateLoanDto newLoan)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var createdLoan = await _loansService.CreateLoanAsync(newLoan);
            return CreatedAtAction(nameof(GetLoan), new { id = createdLoan.LoanId }, createdLoan);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}/return")]
    public async Task<ActionResult> ReturnLoan(int id, ReturnLoanDto returnLoan)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var success = await _loansService.ReturnLoanAsync(id, returnLoan);
        if (!success)
        {
            return NotFound("Loan not found or already returned");
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteLoan(int id)
    {
        var success = await _loansService.DeleteLoanAsync(id);
        if (!success)
        {
            return NotFound();
        }
        return NoContent();
    }
}