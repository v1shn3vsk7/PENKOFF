using Storage.Entities;

namespace Logic.PENKOFF;

public interface IUserManager
{
    Task<User> FindUser(int Id);

    Task<User?> FindUser(string Login, string Password);
}
