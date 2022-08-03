using System.ComponentModel.DataAnnotations;

namespace Storage.Enums;

public enum Role
{
    [Display(Name = "User")]
    User = 0,
    
    [Display(Name = "TechSupport")]
    TechSupport = 1,
    
    [Display(Name = "Admin")]
    Admin = 2
}