using FixMyTask_Core_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FixMyTask_Core_Project.Controllers
{
    public class WorkerRegController : Controller
    {
        DataDB db = new DataDB();

        private readonly IWebHostEnvironment _webHostEnvironment;

        public WorkerRegController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        
        public IActionResult WorkerReg_PageLoad()
        {
            var categories=db.GetCategories();
            ViewBag.Categories = new SelectList(categories, "Category_Id", "Category_Name");
            return View();
        }



        public IActionResult WorkerReg_Click(WorkerInsert clsobj, IFormFile file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (file != null && file.Length > 0)
                    {
                        string fileName = Path.GetFileName(file.FileName);

                        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img");

                        string filePath = Path.Combine(uploadsFolder, fileName);

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            file.CopyTo(fileStream);
                        }
                        clsobj.Worker_Photo = "/img/" + fileName;
                    }
                    var getmaxid = db.GetMaxId();
                    int mid = Convert.ToInt32(getmaxid);
                    int regId = 0;
                    if (mid == 0)
                    {
                        regId = 1;
                    }
                    else
                    {
                        regId = mid + 1;
                    }
                    clsobj.Worker_Id = regId;
                    clsobj.Worker_CreatedDate = DateTime.Now.Date;

                    if (db.LoginCheck(clsobj.Username))
                    {
                        clsobj.msg = "Username already exists!";
                    }
                    else
                    {
                        Login login = new Login
                        {
                            Reg_Id = regId,
                            Username = clsobj.Username,
                            Password = clsobj.Password,
                            UserType = "Worker"
                        };
                        db.LoginInsert(login);
                        db.WorkerInsertDB(clsobj);
                        //clsobj.msg = "successfully inserted";

                        //var cat = db.GetCategories();
                        //ViewBag.Categories = new SelectList(cat, "Category_Id", "Category_Name");

                        return RedirectToAction("Login_PageLoad","Login");

                    }

                }
            }
            catch (Exception ex)
            {
                clsobj.msg = ex.Message;

            }

            var categories = db.GetCategories();
            ViewBag.Categories = new SelectList(categories, "Category_Id", "Category_Name");

            return View("WorkerReg_PageLoad", clsobj);
        }


    }
}
