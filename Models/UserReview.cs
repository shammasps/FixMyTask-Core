namespace FixMyTask_Core_Project.Models
{
    public class UserReview
    {
        public int Review_Id { get; set; }
        public int Worker_Id { get; set; }
        public int User_Id { get; set; }
        public int Booking_Id { get; set; }
        public string? Review { get; set; }
        public int Rating { get; set; }
        public DateTime ReviewDate { get; set; }
    }
}
