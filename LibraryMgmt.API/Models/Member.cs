namespace LibraryMgmt.API.Models;

public enum MemberStatus
{
    Active,
    Suspended,
    Inactive
}

public class Member
{
    public int MemberId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string DepartmentClass { get; set; } = string.Empty;
    public DateTime DateRegistered { get; set; } = DateTime.UtcNow;
    public MemberStatus Status { get; set; } = MemberStatus.Active;

    // Navigation property
    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
}