namespace LoanAPI.Domain;

public interface IUserRepository
{
    User? GetUserByUsername(string username);
    User? GetUserById(int userId);
    void AddUser(User user);
    void SaveChanges();
}