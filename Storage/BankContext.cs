using Storage.Entities;

namespace Storage;

public class BankContext : DbContext
{
    public BankContext(DbContextOptions<BankContext> options) : base(options)
    {

    }

    public DbSet<User> Users { get; set; }
}

