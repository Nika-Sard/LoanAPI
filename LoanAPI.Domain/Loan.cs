using LoanAPI.Domain.DTOs;

namespace LoanAPI.Domain;

public class Loan
{
    public int LoanId { get; set; }
    public string LoanType { get; set; }
    public double Amount { get; set; }
    public String Currency { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Status { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }

    public Loan()
    {
        Status = "Pending";
    }
    public void UpdateLoanDetails(Loan loan)
    {
        Amount = loan.Amount;
        Currency = loan.Currency;
        Status = loan.Status; 
        StartDate = loan.StartDate;
        EndDate = loan.EndDate;
        LoanType = loan.LoanType;
        UserId = loan.UserId;
    }
}