using FluentValidation;
using LoanAPI.Domain.DTOs;

public class UserDTOValidator : AbstractValidator<UserDTO>
{
    public UserDTOValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .Length(2, 50).WithMessage("First name must be between 2 and 50 characters.");
        
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .Length(2, 50).WithMessage("Last name must be between 2 and 50 characters.");
        
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .Length(4, 20).WithMessage("Username must be between 4 and 20 characters.");

        RuleFor(x => x.Age)
            .GreaterThan(0).WithMessage("Age must be a positive number.")
            .LessThan(130).WithMessage("Age must be less than 130.");
        
        RuleFor(x => x.Salary)
            .GreaterThan(0).WithMessage("Salary must be a positive number.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .Length(8, 100).WithMessage("Password must be at least 8 characters long.");

        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Role is required.");
    }
}