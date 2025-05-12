using System.ComponentModel.DataAnnotations;

namespace FixMyTask_Core_Project.Models
{
    public class Login
    {
        public int Reg_Id { get; set; }
        [Required(ErrorMessage = "Enter The user Name")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "Enter The Password")]
        public string? Password { get; set; }
        public string? UserType { get; set; }
        public string? msg { get; set; }
    }
}
