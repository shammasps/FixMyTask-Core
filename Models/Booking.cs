namespace FixMyTask_Core_Project.Models
{
    public class Booking
    {
        public int Booking_Id { get; set; }
        public int Worker_Id { get; set; }
        public int User_Id { get; set; }
        public DateTime Booking_Date { get; set; }
        public DateTime ServiceDate { get; set; }
        public string? ServiceTime { get; set; }
        public string? User_Address { get; set; }
        public string? Remarks { get; set; }
        public string? Paid { get; set; }
        public string? Booking_Status { get; set; }

        public string? UserName { get; set; }
        public string? CategoryName { get; set; }

    }
}
