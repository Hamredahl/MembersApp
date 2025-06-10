using System.ComponentModel.DataAnnotations;

namespace MembersApp.Web.Views.Account
{
    public class RegisterVM
    {
        [Required]
        public required string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Repeat password")]
        [Compare(nameof(Password))]
        public string PasswordRepeat { get; set; } = null!;
        [Display(Name = "Admin")]
        public bool IsAdmin { get; set; }
    }
}
