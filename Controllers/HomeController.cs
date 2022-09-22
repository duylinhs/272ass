using _272ass.Data;
using _272ass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace _272ass.Controllers
{
    public class HomeController : Controller
    {
        private _272assContext db = new _272assContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            if(ModelState.IsValid)

            {
                bool IsValidUSer = db.Users.All(u=>u.Username == user.Username&& u.Password == user.Password);
                if (IsValidUSer)
                {
                    FormsAuthentication.SetAuthCookie(user.Username, false);
                    return RedirectToAction("Index", "Admin");
                }    
            }
            ModelState.AddModelError("", "Invalid Credential, please try again");
            return View();
        }
        public PartialViewResult RegisterUser()
        {
            return PartialView(db.Attendees.ToList());
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}