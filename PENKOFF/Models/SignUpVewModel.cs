using Storage.Entities;

namespace PENKOFF.Models;

public class SignUpVewModel
{
    public User user { get; set; }
    
    public string repeatPassword { get; set; }
    
    public string result { get; set; }
}