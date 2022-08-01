using Storage.Entities;

namespace PENKOFF.Models;

public class MailVerificationViewModel
{
    public string mail { get; set; }

    public string result { get; set; } = "";

    public bool isCodeSent { get; set; } = false;
    
    public int inputForVerificationCode { get; set; }
}