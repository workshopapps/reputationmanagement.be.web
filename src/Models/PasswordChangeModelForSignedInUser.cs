using System.ComponentModel.DataAnnotations;

namespace src.Models
{
    public class PasswordChangeModelForSignedInUser
    {
        public string OldPassword { get; set; }  
        public string NewPassword { get; set; }
    }
}
