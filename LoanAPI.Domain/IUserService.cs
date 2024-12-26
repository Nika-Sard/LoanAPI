using LoanAPI.Domain.DTOs;

namespace LoanAPI.Domain;

public interface IUserService
{
    User Login(UserCredentials userCredentials);
    void BlockUser(int userId);
    void AddUser(UserDTO userDTO);
    public User GetUserByUsername(string username);
    public User GetUserById(int userId);
}