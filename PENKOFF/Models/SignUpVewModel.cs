using Storage.Entities;

namespace PENKOFF.Models;

public class SignUpVewModel
{
    public User user { get; set; }
    
    public string ConfirmPassword { get; set; }
    
    public string result { get; set; }
}