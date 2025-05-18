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

        public IActionResult BookingStatusViewWorker(int workerId)
        {
            var userId =Convert.ToInt32(HttpContext.Session.GetInt32("uid"));
            var booking = db.GetBookingByUserAndWorker(userId, workerId);
            return View(booking);
        }
        public IActionResult WithdrawBooking(int bookingId)
        {
           
            var booking = db.GetBookingById(bookingId);
            var deleteBooking = db.DeleteBooking(bookingId);
            return RedirectToAction("BookingStatusViewWorker", new {workerId=booking.Worker_Id});
        }
        public IActionResult MakePayment(int bookingId)
        {
            var booking = db.GetBookingById(bookingId);
            var CompleteWorkStatus = db.UpdatePaymentStatus(bookingId, "Yes");
            return RedirectToAction("BookingStatusViewWorker", new { workerId = booking.Worker_Id });
        }
        [HttpGet]
        public IActionResult Review(int workerId,int userId,int bookingId)
        {
            UserReview model = new UserReview
            {
                Worker_Id = workerId,
                User_Id = userId,
                Booking_Id = bookingId
            };

            return View(model);
        }
        [HttpPost]
        public IActionResult SubmitReview(UserReview model)
        {
            if (ModelState.IsValid)
            {
                var review = db.SaveUserReview(model);
                return RedirectToAction("BookingStatusViewWorker", new { workerId = model.Worker_Id });
            }
            return RedirectToAction("Review", model);
        }
        public IActionResult ShowReview(int workerId)
        {
            var ShowReview = db.GetReviewbyWorker(workerId);
            return View(ShowReview);
        }


    }
}
