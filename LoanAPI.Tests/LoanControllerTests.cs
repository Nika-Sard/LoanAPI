using System.Security.Claims;
using LoanAPI.Controllers;
using LoanAPI.Domain;
using LoanAPI.Domain.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class LoanControllerTests
{
    private readonly Mock<ILoanService> _loanServiceMock;
    private readonly LoanController _controller;

    public LoanControllerTests()
    {
        _loanServiceMock = new Mock<ILoanService>();
        _controller = new LoanController(_loanServiceMock.Object);
        
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Name, "1")
        }, "mock"));

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };
    }

    [Fact]
    public void AddLoan_ShouldReturnOk()
    {
        var loanDTO = new LoanDTO();
        _controller.AddLoan(loanDTO);
        _loanServiceMock.Verify(service => service.AddLoan(1, loanDTO), Times.Once);
    }

    [Fact]
    public void GetLoans_ShouldReturnUserLoans()
    {
        var loans = new List<Loan>();
        _loanServiceMock.Setup(service => service.GetUserLoans(1)).Returns(loans);

        var result = _controller.GetLoans() as OkObjectResult;

        Assert.NotNull(result);
        Assert.Equal(loans, result.Value);
    }

    [Fact]
    public void UpdateUserLoan_ShouldReturnOk()
    {
        var loan = new Loan();
        _controller.UpdateUserLoan(loan);
        _loanServiceMock.Verify(service => service.UpdateLoan(1, loan), Times.Once);
    }

    [Fact]
    public void DeleteLoan_ShouldReturnOk()
    {
        _controller.DeleteLoan(1);
        _loanServiceMock.Verify(service => service.DeleteLoan(1, 1), Times.Once);
    }
}