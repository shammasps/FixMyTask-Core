using FixMyTask_Core_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace FixMyTask_Core_Project.Controllers
{
    public class WorkerController : Controller
    {
        DataDB db = new DataDB();

        private readonly IWebHostEnvironment _webHostEnvironment;

        public WorkerController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Worker_PageLoad()
        {
            var workerId = Convert.ToInt32(HttpContext.Session.GetInt32("uid"));
            var worker = db.GetWorkerById(workerId);
            
            return View(worker);
        }
        [HttpGet]
        public IActionResult EditProfile()
        {
            var workerId = Convert.ToInt32(HttpContext.Session.GetInt32("uid"));
            var worker = db.GetWorkerById(workerId);
            return View(worker);
        }
        [HttpPost]
        public IActionResult EditProfileClick(WorkerModel model,IFormFile file)
        {
            var workerId = Convert.ToInt32(HttpContext.Session.GetInt32("uid"));
            var existingWorker = db.GetWorkerById(workerId);
            model.WorkerId = workerId;


            if (file != null && file.Length > 0)
            {
                string fileName = Path.GetFileName(file.FileName);

                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img");

                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                model.WorkerPhoto = "/img/" + fileName;
            }
            else
            {
                model.WorkerPhoto = existingWorker.WorkerPhoto;
            }

            var workerUpdate = db.UpdateWorkerDetails(model);
            return RedirectToAction("EditProfile",model);
        }
        public IActionResult ViewBookingRequests()
        {
            var workerId = Convert.ToInt32(HttpContext.Session.GetInt32("uid"));
            var pandingRequests = db.GetBookingByStatus(workerId, "Pending");
            return View(pandingRequests);
        }
        public IActionResult AcceptBooking(int bookingId)
        {
            var acceptRequests= db.UpdateBookingStatus(bookingId, "Accepted");
            return RedirectToAction("ViewBookingRequests");
        }
        public IActionResult RejectBooking(int bookingId)
        {
            var rejectRequests = db.UpdateBookingStatus(bookingId, "Rejected");
            return RedirectToAction("ViewBookingRequests");
        }
        public IActionResult ManageBookingStatus()
        {
            var workerId = Convert.ToInt32(HttpContext.Session.GetInt32("uid"));
            var ManageStatus = db.GetWorkerActiveBookings(workerId);
            return View(ManageStatus);
        }
        public IActionResult ReachedLocation(int bookingId)
        {
            var ReachedLocation = db.UpdateBookingStatus(bookingId, "Reached");
            return RedirectToAction("ManageBookingStatus");
        }
        public IActionResult CompleteWork(int bookingId)
        {
            var CompleteWorkStatus = db.UpdateBookingStatus(bookingId, "Completed");
            return RedirectToAction("ManageBookingStatus");
        }
        
        public IActionResult Reviews()
        {
            var workerId = Convert.ToInt32(HttpContext.Session.GetInt32("uid"));
            var ShowReview = db.GetReviewbyWorker(workerId);
            return View(ShowReview);
        }


    }
}
