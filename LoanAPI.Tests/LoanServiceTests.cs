using System;
using System.Collections.Generic;
using LoanAPI.Domain;
using LoanAPI.Domain.DTOs;
using LoanAPI.Repository;
using LoanAPI.Services;
using Moq;
using Xunit;

public class LoanServiceTests
{
    [Fact]
    public void AddLoan_ShouldCallRepositoryAddLoan()
    {
        var loanRepositoryMock = new Mock<ILoanRepository>();
        var service = new LoanService(loanRepositoryMock.Object);

        var loanDTO = new LoanDTO
        {
            UserId = 1,
            Amount = 1000,
            LoanType = "Personal",
            Currency = "USD",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddMonths(6),
            Status = "Active"
        };

        service.AddLoan(1, loanDTO);

        loanRepositoryMock.Verify(repo => repo.AddLoan(It.IsAny<Loan>()), Times.Once);
    }

    [Fact]
    public void GetUserLoans_ShouldReturnLoansFromRepository()
    {
        var loans = new List<Loan>
        {
            new Loan { LoanId = 1, UserId = 1, Amount = 1000 },
            new Loan { LoanId = 2, UserId = 1, Amount = 1500 }
        };

        var loanRepositoryMock = new Mock<ILoanRepository>();
        loanRepositoryMock.Setup(repo => repo.GetUserLoans(1)).Returns(loans);

        var service = new LoanService(loanRepositoryMock.Object);
        var result = service.GetUserLoans(1);

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void UpdateLoan_ShouldThrowException_WhenLoanNotFound()
    {
        var loanRepositoryMock = new Mock<ILoanRepository>();
        loanRepositoryMock.Setup(repo => repo.GetUserLoan(It.IsAny<int>(), It.IsAny<int>())).Returns((Loan)null);

        var service = new LoanService(loanRepositoryMock.Object);

        var loan = new Loan { LoanId = 1, UserId = 1, Amount = 1000 };

        Assert.Throws<InvalidOperationException>(() => service.UpdateLoan(1, loan));
    }

    [Fact]
    public void DeleteLoan_ShouldCallRepositoryDeleteLoan()
    {
        var loan = new Loan { LoanId = 1, UserId = 1, Amount = 1000 };

        var loanRepositoryMock = new Mock<ILoanRepository>();
        loanRepositoryMock.Setup(repo => repo.GetUserLoan(1, 1)).Returns(loan);

        var service = new LoanService(loanRepositoryMock.Object);
        service.DeleteLoan(1, 1);

        loanRepositoryMock.Verify(repo => repo.DeleteLoan(loan), Times.Once);
    }
}
