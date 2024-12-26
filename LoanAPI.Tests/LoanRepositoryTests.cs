using LoanAPI.Data;
using LoanAPI.Domain;
using LoanAPI.Repository;
using Microsoft.EntityFrameworkCore;

public class LoanRepositoryTests
{
    private LoanAPIContext GetInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<LoanAPIContext>()
            .UseInMemoryDatabase(databaseName: "LoanTestDb")
            .Options;
        return new LoanAPIContext(options);
    }

    [Fact]
    public void AddLoan_ShouldAddLoanToDatabase()
    {
        var context = GetInMemoryContext();
        var repository = new LoanRepository(context);

        var loan = new Loan
        {
            LoanId = 1,
            LoanType = "auto-loan",
            UserId = 1,
            Amount = 1000,
            Currency = "USD",
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddYears(1),
            Status = "Pending"
        };

        repository.AddLoan(loan);
        repository.SaveChanges();

        var storedLoan = context.Loans.Single();
        Assert.Equal(loan.LoanId, storedLoan.LoanId);
        Assert.Equal(loan.StartDate, storedLoan.StartDate);
        Assert.Equal(loan.EndDate, storedLoan.EndDate);
        Assert.Equal(loan.Status, storedLoan.Status);
    }

    [Fact]
    public void GetUserLoans_ShouldReturnLoansForUser()
    {
        var context = GetInMemoryContext();
        context.Loans.AddRange(
            new Loan { UserId = 1, LoanType = "loan", Amount = 1000, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddYears(1), Currency = "USD", Status = "Approved" },
            new Loan { UserId = 2, LoanType = "loan", Amount = 2000, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddYears(1), Currency = "USD", Status = "Pending" },
            new Loan { UserId = 2, LoanType = "loan", Amount = 1500, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddYears(1), Currency = "USD", Status = "Approved" }
        );
        context.SaveChanges();

        var repository = new LoanRepository(context);
        var loans = repository.GetUserLoans(1);
        
        Assert.Equal(2, loans.Count);
    }

    [Fact]
    public void DeleteLoan_ShouldRemoveLoanFromDatabase()
    {
        var context = GetInMemoryContext();
        var loan = new Loan { LoanId = 1, UserId = 1, LoanType = "auot-loan", Amount = 1000, StartDate = DateTime.UtcNow, EndDate = DateTime.UtcNow.AddYears(1), Currency = "USD", Status = "Pending" };
        context.Loans.Add(loan);
        context.SaveChanges();

        var repository = new LoanRepository(context);
        repository.DeleteLoan(loan);
        repository.SaveChanges();

        Assert.Empty(context.Loans);
    }
}
