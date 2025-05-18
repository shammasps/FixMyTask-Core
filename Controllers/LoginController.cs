using FixMyTask_Core_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace FixMyTask_Core_Project.Controllers
{
    public class LoginController : Controller
    {
        DataDB db = new DataDB();
        public IActionResult Login_PageLoad()
        {
            return View();
        }

        //public IActionResult UserHome()
        //{
        //    return View();
        //}

        //public IActionResult WorkerHome()
        //{
        //    return View();
        //}


        public IActionResult Login_Click(Login clsobj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var val = db.LoginCountId(clsobj.Username, clsobj.Password);
                    if (val == 1)
                    {
                        var uid = db.LoginId(clsobj.Username, clsobj.Password);
                        HttpContext.Session.SetInt32("uid", uid);

                        var userType = db.GetUserType(clsobj.Username, clsobj.Password);

                        if(userType== "User")
                        {
                            return RedirectToAction("User_PageLoad", "User");
                        }
                        else if(userType== "Worker")
                        {
                            return RedirectToAction("Worker_PageLoad","Worker");
                        }
                        
                    }
                    else
                    {
                      
                        ModelState.AddModelError(string.Empty, "Invalid Username or Password");
                        return View("Login_PageLoad", clsobj);
                    }
                }
                catch (Exception ex)
                {
                    TempData["msg"] = ex.Message;

                }
            }
            return View("Login_PageLoad", clsobj);
        }
    }
}
