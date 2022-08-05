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

    public IQueryable<User> GetAll() => _context.Users;

    public async Task<User?> FindUser(int id) => await _context.Users.FindAsync(id);

    public async Task<User?> FindUser(string login, string password) =>
        await _context.Users.FirstOrDefaultAsync(user => user.Login == login && user.Password == password);

    public async Task<User?> FindUser(string login) =>
        await _context.Users.FirstOrDefaultAsync(user => user.Login == login);
    
    public async Task AddUser(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public int GetUserId(string login) =>
        _context.Users.FirstOrDefault(user => user.Login == login).Id;

    public async Task AddEmailToUser(int id, string email)
    {
        var user = await _context.Users.FindAsync(id);

        user.Mail = email;
        await _context.SaveChangesAsync();
    }

    public async Task Create(User entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
    }
    
}