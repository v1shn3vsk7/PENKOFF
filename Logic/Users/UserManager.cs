using Logic.PENKOFF;
using Storage;

namespace Logic.Users;

public class UserManager : IUserManager
{
    private readonly BankContext _context;

    public UserManager(BankContext context)
    {
        _context = context;
    }

}
