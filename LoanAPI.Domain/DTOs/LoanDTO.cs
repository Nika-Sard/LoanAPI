namespace LoanAPI.Domain.DTOs;

public class LoanDTO
{
    public string LoanType { get; set; }
    public double Amount { get; set; }
    public string Currency { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Status { get; set; }
    public int UserId { get; set; }
}