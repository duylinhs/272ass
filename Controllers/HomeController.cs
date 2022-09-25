using _272ass.Data;
using _272ass.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
            Debug.WriteLine("");
            var events = db.Events.Include(e => e.EventType).Include(e => e.Organiser).Where(e=>e.Deleted!=true);
            return View(events.ToList());
        }
        public ActionResult Login()
        {
            if (TempData["shortMessage"] != null)
            {
                ViewBag.errormess = TempData["shortMessage"].ToString();
                ModelState.AddModelError("", TempData["shortMessage"].ToString());
            }
            return View("Login");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            if(ModelState.IsValid)
            {
                var inp = user.Password;
                using(var md5Hash = MD5.Create())
                {
                    var sourceByte = Encoding.UTF8.GetBytes(inp);
                    var hasBute = md5Hash.ComputeHash(sourceByte);
                    var outs  = BitConverter.ToString(hasBute).Replace("-", string.Empty);
                    user.Password = outs;
                }
                bool IsValidUSer = db.Users.Any(u=>u.Username.ToLower() == user.Username.ToLower() && u.Password== user.Password);
                
                if (IsValidUSer)
                {
                    FormsAuthentication.SetAuthCookie(user.Username, false);
                    return RedirectToAction("Index", "Home");
                }    
            }
            TempData["shortMessage"] = "Invalid Credential, please try again";
            return RedirectToAction("Login", "Home");
            //return View("Login");
        }
        public PartialViewResult RegisterAdmin()
        {
            return PartialView("RegisterAdmin");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterAdmin([Bind(Include = "ID,Username,Password,Deleted,CreatedDate,LastEdit")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                var inp = admin.Password;
                using (var md5Hash = MD5.Create())
                {
                    var sourceByte = Encoding.UTF8.GetBytes(inp);
                    var hasBute = md5Hash.ComputeHash(sourceByte);
                    var outs = BitConverter.ToString(hasBute).Replace("-", string.Empty);
                    admin.Password = outs;
                }
                try
                {
                    db.Admins.Add(admin);
                    db.SaveChanges();
                    return RedirectToActionPermanent("Login", "Home");
                }
                catch (DbUpdateException error)
                {
                    TempData["shortMessage"] = "Registration failed, this username existed";
                    ViewBag.errormess = "Registration failed, this username existed";
                    return RedirectToAction("Login");
                }
                
            }
            return new EmptyResult();
        }
        public PartialViewResult RegisterOrganiser()
        {
            return PartialView("RegisterOrganiser");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterOrganiser([Bind(Include = "ID,Username,Password,Deleted,CreatedDate,LastEdit")] Organiser organiser)
        {
            if (ModelState.IsValid)
            {
                var inp = organiser.Password;
                using (var md5Hash = MD5.Create())
                {
                    var sourceByte = Encoding.UTF8.GetBytes(inp);
                    var hasBute = md5Hash.ComputeHash(sourceByte);
                    var outs = BitConverter.ToString(hasBute).Replace("-", string.Empty);
                    organiser.Password = outs;
                }
                try { 
                db.Organisers.Add(organiser);
                db.SaveChanges();
                return RedirectToActionPermanent("Index","Home");
                }
                catch
                {
                    TempData["shortMessage"] = "Registration failed, this username existed";
                    ViewBag.errormess = "Registration failed, this username existed";
                    return RedirectToAction("Login");
                }
            }

            return View(organiser);
        }
        public PartialViewResult RegisterUser()
        {
            return PartialView("RegisterUser");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterUser([Bind(Include = "ID,FirstName,LastName,Birthday,Email,Address,PhoneNumber,Username,Password,Deleted,CreatedDate,LastEdit")] Attendee attendee)
        {
            if (ModelState.IsValid)
            {
                var inp = attendee.Password;
                using (var md5Hash = MD5.Create())
                {
                    var sourceByte = Encoding.UTF8.GetBytes(inp);
                    var hasBute = md5Hash.ComputeHash(sourceByte);
                    var outs = BitConverter.ToString(hasBute).Replace("-", string.Empty);
                    attendee.Password = outs;
                }
                try
                {
                    db.Attendees.Add(attendee);
                    db.SaveChanges();
                    return RedirectToActionPermanent("Index", "Home");
                }
                catch
                {
                    TempData["shortMessage"] = "Registration failed, this username existed";
                    ViewBag.errormess = "Registration failed, this username existed";
                    return RedirectToAction("Login");
                }
                
            }

            return RedirectToActionPermanent("Index","Home");
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