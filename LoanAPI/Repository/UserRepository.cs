using LoanAPI.Data;
using LoanAPI.Domain;

namespace LoanAPI.Repository;

public class UserRepository : IUserRepository
{
    private readonly LoanAPIContext _userContext;

    public UserRepository(LoanAPIContext userContext)
    {
        _userContext = userContext;
    }

    public User? GetUserByUsername(string username)
    {
        return _userContext.Users.SingleOrDefault(u => u.Username == username);
    }

    public User? GetUserById(int userId)
    {
        return _userContext.Users.SingleOrDefault(u => u.UserId == userId);
    }
    public void AddUser(User user)
    {
        _userContext.Users.Add(user);
    }

    public void SaveChanges()
    {
        _userContext.SaveChanges();
    }
}