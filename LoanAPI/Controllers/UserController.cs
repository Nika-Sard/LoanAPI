using LoanAPI.Data;
using LoanAPI.Domain;
using LoanAPI.Domain.DTOs;
using LoanAPI.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace LoanAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly ITokenGenerator _tokenGenerator;

    public UserController(IUserService userService, ITokenGenerator tokenGenerator)
    {
        _userService = userService;
        _tokenGenerator = tokenGenerator;
    }

    [AllowAnonymous]
    [HttpPost("Register")]
    public IActionResult Register([FromBody] UserDTO userDTO)
    {
        if (userDTO == null)
        {
            throw new ArgumentNullException(nameof(userDTO), "User data is required.");
        }
        
        _userService.AddUser(userDTO);

        return Ok("User registered successfully.");
    }
    
    [AllowAnonymous]
    [HttpPost("Login")]
    public IActionResult Login([FromBody] UserCredentials userCredentials)
    {
        if (userCredentials == null)
        {
            throw new ArgumentNullException(nameof(userCredentials), "User credentials are required.");
        }

        var resultUser = _userService.Login(userCredentials);
        string token = _tokenGenerator.GenerateToken(resultUser);
        var loginResponse = new LoginResponse()
        {
            UserId = resultUser.UserId,
            FirstName = resultUser.FirstName,
            LastName = resultUser.LastName,
            Username = resultUser.Username,
            Age = resultUser.Age,
            Salary = resultUser.Salary,
            IsBlocked = resultUser.IsBlocked,
            Role = resultUser.Role,
            Token = token
        };
        return Ok(loginResponse);
    }

    
}