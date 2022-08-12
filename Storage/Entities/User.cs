using Storage.Enums;

namespace Storage.Entities;

public class User
{
    [Key]
    public int Id { get; set; }
    
    public Role Role { get; set; }

    public string Login { get; set; }

    public string Password { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }
    
}

