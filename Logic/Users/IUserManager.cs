using Storage.Entities;

namespace Logic.PENKOFF;

public interface IUserManager
{
    Task<User?> FindUser(int Id);

    Task<User?> FindUser(string Login, string Password);

    Task<User?> FindUser(string Login);

    Task AddUser(User user);

    int GetUserId(string Login);

    Task AddEmailToUser(int Id, string Email);
}
