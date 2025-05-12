using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace FixMyTask_Core_Project.Models
{
    public class UserInsert
    {
        public int User_Id { get; set; }
        [Required (ErrorMessage = "Name is required")]
        public string? User_Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format")]
        public string? User_Email { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid phone number format. It should be 10 digits.")]
        public long User_Phone { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string? User_Address { get; set; }
        [Required(ErrorMessage = "Photo is required")]
        public string? User_Photo { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public string? User_Gender { get; set; }
        [Required(ErrorMessage = "Country is required")]
        public string? User_Country { get; set; }
        [Required(ErrorMessage = "State is required")]
        public string? User_State { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string? User_City { get; set; }
        [Required(ErrorMessage = "Pincode is required")]
        public long Pincode { get; set; }
        public DateTime CreatedDate { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
        [Compare("Password", ErrorMessage = "Password Not Match")]
        public string? cPassword { get; set; }
        public string? usermsg { get; set; }

    }
}
