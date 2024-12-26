namespace LoanAPI.Domain.DTOs;

public class LoginResponse
{
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public int Age { get; set; }
    public decimal Salary { get; set; }
    public bool IsBlocked { get; set; }
    public string Role { get; set; }
    public string Token { get; set; }
}
