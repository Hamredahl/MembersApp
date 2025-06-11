using System.ComponentModel.DataAnnotations;

namespace MembersApp.Web.Views.Member;

public class CreateVM
{
    [Required(ErrorMessage = "Name is required")]
    [Display(Name = "Full Name")]
    public required string Name { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    [Display(Name = "Phone number")]
    [RegularExpression($"(\\d+)", ErrorMessage = "Phone number is not valid")]
    public string? Phone { get; set; }

    [Display(Name = "Streetname and number")]
    public string? Street { get; set; }

    [Display(Name = "ZIP code")]
    public string? ZipNumber { get; set; }

    public string? City { get; set; }
}
