using Microsoft.AspNetCore.Mvc;
using FixMyTask_Core_Project.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FixMyTask_Core_Project.Controllers
{
    public class UserController : Controller
    {
        DataDB db = new DataDB();

        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult User_PageLoad()
        {
            var categories = db.GetCategoriesUserPage().ToList();
            return View(categories);  
        }
        public IActionResult ViewWorkersByCategory(int categoryId)
        {
            var workers=db.GetWorkersByCategory(categoryId).ToList();
            return View(workers);
        }
        public IActionResult WorkerProfile(int workerId)
        {
            var worker = db.ViewWorker(workerId);
            return View(worker);
        }
        public IActionResult BookWorker(Booking booking)
        {
            if (ModelState.IsValid)
            {
                var userId = HttpContext.Session.GetInt32("uid");
                booking.Worker_Id=
                booking.User_Id = Convert.ToInt32(userId);
                booking.Booking_Date = DateTime.Now;
                booking.Booking_Status = "Pending";
                booking.Paid = "No";
                var newBookingId=db.SaveBooking(booking);
                
                TempData["BookingSuccess"] = "Booking Successful!";
                return RedirectToAction("BookingStatusView", new {bookingId= newBookingId });
            }
            return View("WorkerProfile",booking);
        }
        public IActionResult BookingStatusView(int bookingId)
        {
            var booking=db.GetBookingById(bookingId);
            return View(booking);
        }


    }
}
