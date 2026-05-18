using LibraryMgmt.API.DTOs;
using LibraryMgmt.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryMgmt.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembersController : ControllerBase
{
    private readonly IMembersService _membersService;
    private readonly ILoansService _loansService;

    public MembersController(IMembersService membersService, ILoansService loansService)
    {
        _membersService = membersService;
        _loansService = loansService;
    }

    [HttpGet]
    public async Task<ActionResult<List<MemberDto>>> GetMembers()
    {
        var members = await _membersService.GetAllMembersAsync();
        return Ok(members);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MemberDto>> GetMember(int id)
    {
        var member = await _membersService.GetMemberByIdAsync(id);
        if (member == null)
        {
            return NotFound();
        }
        return Ok(member);
    }

    [HttpGet("{id}/loans")]
    public async Task<ActionResult<List<LoanDto>>> GetMemberLoans(int id)
    {
        var member = await _membersService.GetMemberByIdAsync(id);
        if (member == null)
        {
            return NotFound("Member not found");
        }

        var allLoans = await _loansService.GetAllLoansAsync();
        var memberLoans = allLoans.Where(l => l.MemberId == id).ToList();
        return Ok(memberLoans);
    }

    [HttpPost]
    public async Task<ActionResult<MemberDto>> CreateMember(CreateMemberDto newMember)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdMember = await _membersService.CreateMemberAsync(newMember);
        return CreatedAtAction(nameof(GetMember), new { id = createdMember.MemberId }, createdMember);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateMember(int id, UpdateMemberDto updatedMember)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var success = await _membersService.UpdateMemberAsync(id, updatedMember);
        if (!success)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteMember(int id)
    {
        var success = await _membersService.DeleteMemberAsync(id);
        if (!success)
        {
            return NotFound();
        }
        return NoContent();
    }
}