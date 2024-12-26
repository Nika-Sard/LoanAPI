namespace LoanAPI.Domain;

public interface ILoanRepository
{
    public bool DoesLoanExist(int loanId);
    List<Loan> GetUserLoans(int userId);
    Loan GetUserLoan(int userId, int loanId);
    void AddLoan(Loan loan);
    void UpdateLoan(Loan loan);
    void DeleteLoan(Loan loan);
    void SaveChanges();
}