namespace LoanAPI.Domain;

public class User
{
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public int Age { get; set; }
    public int Salary { get; set; }
    public bool IsBlocked { get; set; } = false;
    public string Password { get; set; }
    public string Role { get; set; }
    public List<Loan> Loans { get; set; } = new List<Loan>();
}