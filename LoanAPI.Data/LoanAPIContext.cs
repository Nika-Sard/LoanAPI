using LoanAPI.Domain;
using Microsoft.EntityFrameworkCore;

namespace LoanAPI.Data;

public class LoanAPIContext : DbContext
{
    public DbSet<Loan> Loans { get; set; }
    public DbSet<User> Users { get; set; }
    public LoanAPIContext(DbContextOptions<LoanAPIContext> options) : base(options) { }
}