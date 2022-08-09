using System.ComponentModel.DataAnnotations;
using Storage.Entities;

namespace PENKOFF.Models;

public class RegisterVewModel
{
    public User user { get; set; }
    
    public string ConfirmPassword { get; set; }
    
    public string result { get; set; }
}