using System.ComponentModel.DataAnnotations;

namespace ProjectPlanner.Models
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "First Name is Required")]
        [MaxLength(50, ErrorMessage = "Max 50 characters allowed")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is Required")]
        [MaxLength(50, ErrorMessage = "Max 50 characters allowed")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [MaxLength(100, ErrorMessage = "Max 100 characters allowed")]
        [EmailAddress(ErrorMessage = "Please enter valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "User Name is Required")]
        [MaxLength(20, ErrorMessage = "Max 20 characters allowed")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Password should be of 10-15 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Please confirm your password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
