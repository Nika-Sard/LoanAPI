using LoanAPI.Domain;
using LoanAPI.Domain.DTOs;

namespace LoanAPI.Services;

public class LoanService : ILoanService
{
    private readonly ILoanRepository _loanRepository;

    public LoanService(ILoanRepository loanRepository)
    {
        _loanRepository = loanRepository;
    }
    
    public List<Loan> GetUserLoans(int userId)
    {
        return _loanRepository.GetUserLoans(userId);
    }
    
    public void AddLoan(int userId, LoanDTO loanDTO)
    {
        if (userId != loanDTO.UserId)
        {
            throw new UnauthorizedAccessException("You are not authorized to add loan for others");
        }
        var loan = new Loan
        {
            LoanType = loanDTO.LoanType,
            Amount = loanDTO.Amount,
            Currency = loanDTO.Currency,
            StartDate = loanDTO.StartDate,
            EndDate = loanDTO.EndDate,
            Status = loanDTO.Status,
            UserId = loanDTO.UserId
        };
        _loanRepository.AddLoan(loan);
    }

    public void UpdateLoan(int userId, Loan loan)
    {
        var existingLoan = _loanRepository.GetUserLoan(userId, loan.LoanId);
        if(existingLoan == null)
        {
            throw new InvalidOperationException("Loan not found for the specified user.");
        }

        if (loan.Status.Equals("Pending"))
        {
            throw new InvalidOperationException("User cannot update loan due to status");
        }
        existingLoan.UpdateLoanDetails(loan);
        _loanRepository.SaveChanges();
    }

    public void DeleteLoan(int userId, int loanId)
    {
        var loan = _loanRepository.GetUserLoan(userId, loanId);
        
        if (loan == null)
        {
            throw new InvalidOperationException("Loan not found for the specified user and loan ID.");
        }

        _loanRepository.DeleteLoan(loan);
        _loanRepository.SaveChanges();
    }
}