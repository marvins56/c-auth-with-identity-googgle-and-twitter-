using System.ComponentModel.DataAnnotations;

namespace STMIS.Models.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
