using System.Security.Claims;
using LoanAPI.Domain;
using LoanAPI.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LoanAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class LoanController : Controller
{
    private readonly ILoanService _loanService;

    public LoanController(ILoanService loanService)
    {
        _loanService = loanService;
    }

    [HttpPost("loan")]
    public IActionResult AddLoan([FromQuery] LoanDTO loanDTO)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.Name);
        int userId = int.Parse(userIdClaim.Value);
        _loanService.AddLoan(userId, loanDTO);
        return Ok("Loan added");
    }

    [HttpGet("loans")]
    public IActionResult GetLoans()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.Name);
        int userId = int.Parse(userIdClaim.Value);
        return Ok(_loanService.GetUserLoans(userId));
    }
    
    [HttpPut("loans")]
    public IActionResult UpdateUserLoan([FromBody] Loan loan)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.Name);
        int userId = int.Parse(userIdClaim.Value);
        _loanService.UpdateLoan(userId, loan);
        return Ok("Loan updated");
    }

    [HttpDelete("loans/{loanId}")]
    public IActionResult DeleteLoan([FromRoute] int loanId)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.Name);
        int userId = int.Parse(userIdClaim.Value);
        _loanService.DeleteLoan(userId, loanId);
        return Ok("Loan deleted");
    }
     
    
}