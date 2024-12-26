using LoanAPI.Data;
using LoanAPI.Domain;
using LoanAPI.Repository;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class UserRepositoryTests
{
    private readonly UserRepository _repository;
    private readonly LoanAPIContext _context;

    public UserRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<LoanAPIContext>()
            .UseInMemoryDatabase(databaseName: "LoanAPI_TestDB")
            .Options;

        _context = new LoanAPIContext(options);

        _context.Users.Add(new User {        
            FirstName = "John",
            LastName = "Doe",
            Username = "john_doe",
            Age = 30,
            Salary = 50000,
            IsBlocked = false,
            Role = "User",
            Password = "pass"
        });
        _context.SaveChanges();
        _repository = new UserRepository(_context);
    }

    [Fact]
    public void GetUserByUsername_ShouldReturnUser_WhenUserExists()
    {
        var result = _repository.GetUserByUsername("john_doe");

        Assert.NotNull(result);
        Assert.Equal("john_doe", result.Username);
    }

    [Fact]
    public void GetUserByUsername_ShouldReturnNull_WhenUserDoesNotExist()
    {
        var result = _repository.GetUserByUsername("non_existent_user");

        Assert.Null(result);
    }

    [Fact]
    public void AddUser_ShouldAddUserToContext()
    {
        var user = new User{
            FirstName = "new",
            LastName = "user",
            Username = "new_user",
            Age = 30,
            Salary = 50000,
            IsBlocked = false,
            Role = "User",
            Password = "pass"
        };

        _repository.AddUser(user);
        _repository.SaveChanges();

        var addedUser = _context.Users.SingleOrDefault(u => u.Username.Equals("new_user"));
        Assert.NotNull(addedUser);
        Assert.Equal("new_user", addedUser.Username);
    }
}