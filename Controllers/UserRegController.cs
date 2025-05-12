using Microsoft.AspNetCore.Mvc;
using FixMyTask_Core_Project.Models;


namespace FixMyTask_Core_Project.Controllers
{
    public class UserRegController : Controller
    {
        DataDB db = new DataDB();
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserRegController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult UserReg_PageLoad()
        {
            return View();
        }
        public IActionResult UserReg_Click(UserInsert clsobj, IFormFile file)
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
                        clsobj.User_Photo = "/img/" + fileName;
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
                    clsobj.User_Id= regId;
                    clsobj.CreatedDate=DateTime.Now.Date;

                    if (db.LoginCheck(clsobj.Username))
                    {
                        clsobj.usermsg = "Username already exists!";
                    }
                    else
                    {
                        Login login = new Login
                        {
                            Reg_Id = regId,
                            Username = clsobj.Username,
                            Password = clsobj.Password,
                            UserType = "User"
                        };

                        db.LoginInsert(login);
                        db.UserinsertDB(clsobj);

                        //clsobj.usermsg = "successfully inserted";
                        return RedirectToAction("Login_PageLoad", "Login");
                    }
                    
                }
            }
            catch (Exception ex)
            {
                TempData["usermsg"] = ex.Message;

            }

            return View("UserReg_PageLoad", clsobj);
        }


    }
}
