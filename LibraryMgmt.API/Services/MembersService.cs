using LibraryMgmt.API.Data;
using LibraryMgmt.API.DTOs;
using LibraryMgmt.API.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryMgmt.API.Services;

public class MembersService : IMembersService
{
    private readonly LibraryDbContext _context;

    public MembersService(LibraryDbContext context)
    {
        _context = context;
    }

    public async Task<List<MemberDto>> GetAllMembersAsync()
    {
        var members = await _context.Members.ToListAsync();
        return members.Select(MapToDto).ToList();
    }

    public async Task<MemberDto?> GetMemberByIdAsync(int id)
    {
        var member = await _context.Members.FindAsync(id);
        return member == null ? null : MapToDto(member);
    }

    public async Task<MemberDto> CreateMemberAsync(CreateMemberDto createMemberDto)
    {
        if (await _context.Members.AnyAsync(m => m.Email == createMemberDto.Email))
        {
            throw new InvalidOperationException("A member with this email already exists.");
        }

        var member = new Member
        {
            FirstName = createMemberDto.FirstName,
            LastName = createMemberDto.LastName,
            Email = createMemberDto.Email,
            Phone = createMemberDto.Phone,
            DepartmentClass = createMemberDto.DepartmentClass,
            Status = createMemberDto.Status,
            DateRegistered = DateTime.UtcNow
        };

        _context.Members.Add(member);
        await _context.SaveChangesAsync();
        return MapToDto(member);
    }

    public async Task<bool> UpdateMemberAsync(int id, UpdateMemberDto updateMemberDto)
    {
        var member = await _context.Members.FindAsync(id);
        if (member == null)
        {
            return false;
        }

        if (member.Email != updateMemberDto.Email && await _context.Members.AnyAsync(m => m.Email == updateMemberDto.Email))
        {
            throw new InvalidOperationException("A member with this email already exists.");
        }

        member.FirstName = updateMemberDto.FirstName;
        member.LastName = updateMemberDto.LastName;
        member.Email = updateMemberDto.Email;
        member.Phone = updateMemberDto.Phone;
        member.DepartmentClass = updateMemberDto.DepartmentClass;
        member.Status = updateMemberDto.Status;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteMemberAsync(int id)
    {
        var member = await _context.Members.FindAsync(id);
        if (member == null)
        {
            return false;
        }

        var hasActiveLoans = await _context.Loans.AnyAsync(l => l.MemberId == id && l.ReturnDate == null);
        if (hasActiveLoans)
        {
            throw new InvalidOperationException("Cannot delete a member with active loans.");
        }

        _context.Members.Remove(member);
        await _context.SaveChangesAsync();
        return true;
    }

    private static MemberDto MapToDto(Member member)
    {
        return new MemberDto
        {
            MemberId = member.MemberId,
            FirstName = member.FirstName,
            LastName = member.LastName,
            Email = member.Email,
            Phone = member.Phone,
            DepartmentClass = member.DepartmentClass,
            Status = member.Status,
            DateRegistered = member.DateRegistered
        };
    }
}