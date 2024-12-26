using LoanAPI.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoanAPI.Controllers;

[ApiController]
[Authorize(Roles = Role.Accountant)]
[Route("api/[controller]")]
public class AccountantController : Controller
{
    private readonly ILoanService _loanService;
    private readonly IUserService _userService;

    public AccountantController(ILoanService loanService, IUserService userService)
    {
        _loanService = loanService;
        _userService = userService;
    }

    
    [HttpGet("users/{userId}")]
    public IActionResult GetUserLoans([FromRoute] int userId)
    {
       return Ok(_loanService.GetUserLoans(userId));
    }

    [HttpPut("users/{userId}/loans")]
    public IActionResult UpdateUserLoan([FromRoute] int userId, [FromBody] Loan loan)
    {
        _loanService.UpdateLoan(userId, loan);
        return Ok("Loan updated");
    }

    [HttpDelete("users/{userId}/loans/{loanId}")]
    public IActionResult DeleteLoan([FromRoute] int userId, [FromRoute] int loanId)
    {
        _loanService.DeleteLoan(userId, loanId);
        return Ok("Loan deleted");
    }

    [HttpPost("users/{userId}")]
    public IActionResult BlockUser([FromRoute] int userId)
    {
        _userService.BlockUser(userId);
        return Ok("User blocked");
    }
}