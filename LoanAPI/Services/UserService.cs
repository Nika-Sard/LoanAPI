using LoanAPI.Data;
using LoanAPI.Domain;
using LoanAPI.Domain.DTOs;

namespace LoanAPI.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User Login(UserCredentials userCredentials)
    {
        var loggedUser = GetUserByUsername(userCredentials.Username);

        if (!BCrypt.Net.BCrypt.Verify(userCredentials.Password, loggedUser.Password))
        {
            throw new UnauthorizedAccessException("Invalid username or password.");
        }
        
        return loggedUser;
    }

    public void AddUser(UserDTO userDTO)
    {
        var existingUser = _userRepository.GetUserByUsername(userDTO.Username);
        if (existingUser != null)
        {
            throw new InvalidOperationException("Username is already taken.");
        }

        userDTO.Password = BCrypt.Net.BCrypt.HashPassword(userDTO.Password);
        var user = new User()
        {
            Username = userDTO.Username,
            FirstName = userDTO.FirstName,
            LastName = userDTO.LastName,
            Age = userDTO.Age,
            Salary = userDTO.Salary,
            IsBlocked = userDTO.IsBlocked,
            Password = userDTO.Password,
            Role = userDTO.Role
        };
        _userRepository.AddUser(user);
        _userRepository.SaveChanges();
    }

    public User GetUserByUsername(string username)
    {
        var user = _userRepository.GetUserByUsername(username);
        if (user == null)
        {
            throw new KeyNotFoundException("User not found.");
        }
        return user;
    }
    
    public User GetUserById(int userId)
    {
        var user = _userRepository.GetUserById(userId);
        if (user == null)
        {
            throw new KeyNotFoundException("User not found.");
        }

        return user;
    }

    public void BlockUser(int userId)
    {
        var user = GetUserById(userId);
        user.IsBlocked = true;
        _userRepository.SaveChanges();
    }
}