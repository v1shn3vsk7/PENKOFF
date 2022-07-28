using Storage.Entities;

namespace Storage;

public class BankContext : DbContext
{
    public BankContext(DbContextOptions<BankContext> options) : base(options)
    {

    }

    public DbSet<User?> Users { get; set; }

    public DbSet<CurrencyAccounts> CurrencyAccounts { get; set; }

    public DbSet<Card> Cards { get; set; }
}

