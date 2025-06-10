using System.ComponentModel.DataAnnotations;

namespace MembersApp.Web.Views.Account
{
    public class LoginVM
    {
        [Required]
        public required string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
