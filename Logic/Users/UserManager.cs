using Logic.PENKOFF;
using Microsoft.EntityFrameworkCore;
using Storage;
using Storage.Entities;

namespace Logic.Users;

public class UserManager : IUserManager
{
    private readonly BankContext _context;

    public UserManager(BankContext context)
    {
        _context = context;
    }

    public async Task<User> FindUser(int Id) => await _context.Users.FindAsync(Id);

    public async Task<User?> FindUser(string Login, string Password) =>
        await _context.Users.FirstOrDefaultAsync(user => user.Login == Login && user.Password == Password);
}