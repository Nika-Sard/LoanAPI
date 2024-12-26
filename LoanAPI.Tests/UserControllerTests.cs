using LoanAPI.Controllers;
using LoanAPI.Domain;
using LoanAPI.Domain.DTOs;
using LoanAPI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

public class UserControllerTests
{
    private readonly Mock<IUserService> _userServiceMock;
    private readonly Mock<ITokenGenerator> _tokenGeneratorMock;
    private readonly UserController _controller;

    public UserControllerTests()
    {
        _userServiceMock = new Mock<IUserService>();
        _tokenGeneratorMock = new Mock<ITokenGenerator>();
        _controller = new UserController(_userServiceMock.Object, _tokenGeneratorMock.Object);
    }

    [Fact]
    public void Register_ShouldReturnOk()
    {
        var userDTO = new UserDTO();
        var result = _controller.Register(userDTO) as OkObjectResult;

        Assert.NotNull(result);
        Assert.Equal("User registered successfully.", result.Value);
        _userServiceMock.Verify(service => service.AddUser(userDTO), Times.Once);
    }

    [Fact]
    public void Login_ShouldReturnTokenAndUserDetails()
    {
        var userCredentials = new UserCredentials();
        var resultUser = new User
        {
            UserId = 1,
            FirstName = "John",
            LastName = "Doe",
            Username = "johndoe",
            Age = 30,
            Salary = 50000,
            IsBlocked = false,
            Role = "User"
        };

        _userServiceMock.Setup(service => service.Login(userCredentials)).Returns(resultUser);
        _tokenGeneratorMock.Setup(generator => generator.GenerateToken(resultUser)).Returns("mock-token");

        var result = _controller.Login(userCredentials) as OkObjectResult;
        var response = result.Value as LoginResponse;
        
        Assert.NotNull(result);
        Assert.Equal("mock-token", response.Token);
        Assert.Equal(resultUser.UserId, response.UserId);
        Assert.Equal(resultUser.FirstName, response.FirstName);
        Assert.Equal(resultUser.LastName, response.LastName);
        Assert.Equal(resultUser.Username, response.Username);
        Assert.Equal(resultUser.Age, response.Age);
        Assert.Equal(resultUser.Salary, response.Salary);
        Assert.Equal(resultUser.IsBlocked, response.IsBlocked);
        Assert.Equal(resultUser.Role, response.Role);
    }
}