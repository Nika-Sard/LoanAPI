namespace LoanAPI.Domain.DTOs;

public class UserDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public int Age { get; set; }
    public int Salary { get; set; }
    public bool IsBlocked { get; set; } = false;
    public string Password { get; set; }
    public string Role { get; set; }
}