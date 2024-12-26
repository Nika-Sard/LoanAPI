using LoanAPI.Domain.DTOs;

namespace LoanAPI.Domain;

public interface ILoanService
{
    List<Loan> GetUserLoans(int userId);
    void AddLoan(int userId, LoanDTO loanDTO);
    void UpdateLoan(int userId, Loan loan);
    void DeleteLoan(int userId, int loanId);
}