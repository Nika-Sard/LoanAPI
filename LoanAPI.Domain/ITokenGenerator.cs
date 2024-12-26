namespace LoanAPI.Domain;

public interface ITokenGenerator
{
    string GenerateToken(User user);
}