using FluentValidation;
using LoanAPI.Domain.DTOs;

namespace LoanAPI.Helpers
{
    public class LoanDTOValidator : AbstractValidator<LoanDTO>
    {
        public LoanDTOValidator()
        {
            RuleFor(x => x.LoanType)
                .NotEmpty().WithMessage("Loan type is required.")
                .IsInEnum().WithMessage("Loan type must be a valid type.");

            RuleFor(x => x.Amount)
                .GreaterThan(0).WithMessage("Amount must be greater than zero.");

            RuleFor(x => x.Currency)
                .NotEmpty().WithMessage("Currency is required.")
                .Length(3).WithMessage("Currency must be 3 characters.");

            RuleFor(x => x.StartDate)
                .LessThan(x => x.EndDate).WithMessage("Start date must be before end date.");

            RuleFor(x => x.EndDate)
                .GreaterThanOrEqualTo(DateTime.Now).WithMessage("End date must be in the future.");
        }
    }
}