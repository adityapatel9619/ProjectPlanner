using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ProjectPlanner.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "User Name or Email is Required")]
        [MaxLength(20, ErrorMessage = "Max 20 characters allowed")]

        [DisplayName("Username or Email")]
        public string UserNameOrEmail { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Password should be of 10-15 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
