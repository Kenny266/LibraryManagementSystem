using LibraryMgmt.API.Models;
using System.ComponentModel.DataAnnotations;

namespace LibraryMgmt.API.DTOs;

public class MemberDto
{
    public int MemberId { get; set; }

    [Required(ErrorMessage = "First name is required")]
    [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required")]
    [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = string.Empty;

    [Phone(ErrorMessage = "Invalid phone number")]
    public string Phone { get; set; } = string.Empty;

    [StringLength(100, ErrorMessage = "Department/Class cannot exceed 100 characters")]
    public string DepartmentClass { get; set; } = string.Empty;

    public MemberStatus Status { get; set; } = MemberStatus.Active;

    public DateTime DateRegistered { get; set; } = DateTime.UtcNow;
}

public class CreateMemberDto
{
    [Required(ErrorMessage = "First name is required")]
    [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required")]
    [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = string.Empty;

    [Phone(ErrorMessage = "Invalid phone number")]
    public string Phone { get; set; } = string.Empty;

    [StringLength(100, ErrorMessage = "Department/Class cannot exceed 100 characters")]
    public string DepartmentClass { get; set; } = string.Empty;

    public MemberStatus Status { get; set; } = MemberStatus.Active;
}

public class UpdateMemberDto
{
    [Required(ErrorMessage = "First name is required")]
    [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required")]
    [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = string.Empty;

    [Phone(ErrorMessage = "Invalid phone number")]
    public string Phone { get; set; } = string.Empty;

    [StringLength(100, ErrorMessage = "Department/Class cannot exceed 100 characters")]
    public string DepartmentClass { get; set; } = string.Empty;

    public MemberStatus Status { get; set; } = MemberStatus.Active;
}