using LoanAPI.Controllers;
using LoanAPI.Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class AccountantControllerTests
{
    private readonly Mock<ILoanService> _loanServiceMock;
    private readonly Mock<IUserService> _userServiceMock;
    private readonly AccountantController _controller;

    public AccountantControllerTests()
    {
        _loanServiceMock = new Mock<ILoanService>();
        _userServiceMock = new Mock<IUserService>();
        _controller = new AccountantController(_loanServiceMock.Object, _userServiceMock.Object);
    }

    [Fact]
    public void GetUserLoans_ShouldReturnUserLoans()
    {
        var loans = new List<Loan>();
        _loanServiceMock.Setup(service => service.GetUserLoans(1)).Returns(loans);

        var result = _controller.GetUserLoans(1) as OkObjectResult;

        Assert.NotNull(result);
        Assert.Equal(loans, result.Value);
    }

    [Fact]
    public void UpdateUserLoan_ShouldReturnOk()
    {
        var loan = new Loan();
        _controller.UpdateUserLoan(1, loan);
        _loanServiceMock.Verify(service => service.UpdateLoan(1, loan), Times.Once);
    }

    [Fact]
    public void DeleteLoan_ShouldReturnOk()
    {
        _controller.DeleteLoan(1, 2);
        _loanServiceMock.Verify(service => service.DeleteLoan(1, 2), Times.Once);
    }

    [Fact]
    public void BlockUser_ShouldReturnOk()
    {
        _controller.BlockUser(1);
        _userServiceMock.Verify(service => service.BlockUser(1), Times.Once);
    }
}