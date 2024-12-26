using LoanAPI.Domain;
using LoanAPI.Domain.DTOs;
using LoanAPI.Repository;
using LoanAPI.Services;
using Moq;
using Xunit;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _repositoryMock;
    private readonly UserService _service;

    public UserServiceTests()
    {
        _repositoryMock = new Mock<IUserRepository>();
        _service = new UserService(_repositoryMock.Object);
    }

    [Fact]
    public void Login_ShouldReturnUser_WhenCredentialsAreValid()
    {
        var user = new User
        {
            Username = "john_doe",
            Password = BCrypt.Net.BCrypt.HashPassword("password123")
        };

        var credentials = new UserCredentials
        {
            Username = "john_doe",
            Password = "password123"
        };

        _repositoryMock.Setup(r => r.GetUserByUsername("john_doe")).Returns(user);

        var result = _service.Login(credentials);

        Assert.NotNull(result);
        Assert.Equal("john_doe", result.Username);
    }

    [Fact]
    public void Login_ShouldThrowUnauthorizedAccessException_WhenPasswordIsInvalid()
    {
        var user = new User
        {
            Username = "john_doe",
            Password = BCrypt.Net.BCrypt.HashPassword("password123")
        };

        var credentials = new UserCredentials
        {
            Username = "john_doe",
            Password = "wrongpassword"
        };

        _repositoryMock.Setup(r => r.GetUserByUsername("john_doe")).Returns(user);

        Assert.Throws<UnauthorizedAccessException>(() => _service.Login(credentials));
    }

    [Fact]
    public void AddUser_ShouldThrowInvalidOperationException_WhenUsernameIsTaken()
    {
        var userDTO = new UserDTO { Username = "existing_user" };
        var existingUser = new User { Username = "existing_user" };

        _repositoryMock.Setup(r => r.GetUserByUsername("existing_user")).Returns(existingUser);

        Assert.Throws<InvalidOperationException>(() => _service.AddUser(userDTO));
    }

    [Fact]
    public void BlockUser_ShouldSetIsBlockedToTrue_WhenUserExists()
    {
        var user = new User { UserId = 1, IsBlocked = false };
        _repositoryMock.Setup(r => r.GetUserById(1)).Returns(user);

        _service.BlockUser(1);

        Assert.True(user.IsBlocked);
        _repositoryMock.Verify(r => r.SaveChanges(), Times.Once);
    }
}
