using LoanAPI.Data;
using LoanAPI.Domain;

namespace LoanAPI.Repository;

public class LoanRepository : ILoanRepository
{
    private readonly LoanAPIContext _loanContext;

    public LoanRepository(LoanAPIContext loanContext)
    {
        _loanContext = loanContext;
    }
    
    public bool DoesLoanExist(int loanId)
    {
        return _loanContext.Loans.Any(loan => loan.LoanId == loanId);
    }
    public List<Loan> GetUserLoans(int userId)
    {
        return _loanContext.Loans.Where(l => l.UserId == userId).ToList();
    }
    
    public Loan GetUserLoan(int userId, int loanId)
    {
        return _loanContext.Loans.SingleOrDefault(l => l.LoanId == loanId && l.UserId == userId);
    }
    public void AddLoan(Loan loan)
    {
        _loanContext.Loans.Add(loan);
    }

    public void UpdateLoan(Loan loan)
    {
        _loanContext.Loans.Update(loan);
    }

    public void DeleteLoan(Loan loan)
    {
        _loanContext.Loans.Remove(loan);
    }

    public void SaveChanges()
    {
        _loanContext.SaveChanges();
    }
}