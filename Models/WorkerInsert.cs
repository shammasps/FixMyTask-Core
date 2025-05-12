using System.ComponentModel.DataAnnotations;

namespace FixMyTask_Core_Project.Models
{
    public class WorkerInsert
    {
        public int Worker_Id { get; set; }
        public int Category_Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string? Worker_Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format")]
        public string? Worker_Email { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid phone number format. It should be 10 digits.")]
        public string? Worker_Phone { get; set; }
        
        public string? Worker_Photo { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string? Worker_Address { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        public string? Worker_Gender { get; set; }
        [Required(ErrorMessage = "Country is required")]
        public string? Worker_Country { get; set; }
        [Required(ErrorMessage = "State is required")]
        public string? Worker_State { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string? Worker_City { get; set; }
        [Required(ErrorMessage = "Experience is required")]
        public int Worker_Exp { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public long Worker_Price { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string? Worker_Description { get; set; }
        public DateTime Worker_CreatedDate { get; set; }
        [Required(ErrorMessage = "Username is required")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
        [Compare("Password", ErrorMessage = "Password Not Match")]
        public string? cPassword { get; set; }
        public string? msg { get; set; }
    }
}
