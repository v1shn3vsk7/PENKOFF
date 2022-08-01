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

    public async Task<User?> FindUser(string Login) =>
        await _context.Users.FirstOrDefaultAsync(user => user.Login == Login);
    
    public async Task AddUser(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public int GetUserId(string Login) =>
        _context.Users.FirstOrDefaultAsync(user => user.Login == Login).Id;

    public async Task AddEmailToUser(int Id, string Email)
    {
        var user = await _context.Users.FindAsync("Id");

        user.Mail = Email;
        await _context.SaveChangesAsync();
    }
    
}