using LIBRARYMANAGEMENT.API.DTOs;

namespace LIBRARYMANAGEMENT.API.Services;

public interface IMembersService
{
    Task<List<MemberDto>> GetAllMembersAsync();
    Task<MemberDto?> GetMemberByIdAsync(int id);
    Task<MemberDto> CreateMemberAsync(CreateMemberDto newMember);
    Task<bool> UpdateMemberAsync(int id, UpdateMemberDto updatedMember);
    Task<bool> DeleteMemberAsync(int id);
}