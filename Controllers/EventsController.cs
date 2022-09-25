using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _272ass.Data;
using _272ass.Models;

namespace _272ass.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        
        private _272assContext db = new _272assContext();

        // GET: Events
        [Authorize(Roles ="Organiser, Admin")]
        public ActionResult Index()
        {
            var events = db.Events.Include(e => e.EventType).Include(e => e.Organiser).Where(e => e.Deleted != true); 
            if (User.IsInRole("Organiser"))
            {
                 events = events.Where(e=>e.Organiser.Equals(db.Users.FirstOrDefault(c=>c.Username == User.Identity.Name)));
            }
            
            return View(events.ToList());
        }

        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: Events/Create
        [Authorize(Roles = "Organiser")]
        public ActionResult Create()
        {
            Event model = new Event()
            {
                OrganiserID = db.Users.FirstOrDefault(c => c.Username == User.Identity.Name).ID,
                Organiser = (Organiser)db.Users.FirstOrDefault(c => c.Username == User.Identity.Name)
            };
            ViewBag.EventTypeID = new SelectList(db.EventTypes, "ID", "Title");
            ViewBag.OrganiserID = new SelectList(db.Users, "ID", "Username");
            return View(model);
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Organiser")]
        public ActionResult Create([Bind(Include = "ID,Name,Description,Created,LastEdit,StartDate,RegistrationStart,RegistrationEnd,Location,Deleted,OrganiserID,EventTypeID,Price")] Event @event)
        {
            Debug.WriteLine(@event.Created);
            Debug.WriteLine(@event.LastEdit);
            @event.Created = DateTime.Now;
            @event.LastEdit = DateTime.Now;
            Debug.WriteLine(@event.Created);
            Debug.WriteLine(@event.LastEdit);
            if (ModelState.IsValid)
            {
                db.Events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EventTypeID = new SelectList(db.EventTypes, "ID", "Title", @event.EventTypeID);
            ViewBag.OrganiserID = new SelectList(db.Users, "ID", "Username", @event.OrganiserID);
            return View(@event);
        }

        // GET: Events/Edit/5
        [Authorize(Roles = "Organiser")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventTypeID = new SelectList(db.EventTypes, "ID", "Title", @event.EventTypeID);
            ViewBag.OrganiserID = new SelectList(db.Users, "ID", "Username", @event.OrganiserID);
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Organiser")]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,Created,LastEdit,StartDate,RegistrationStart,RegistrationEnd,Location,Deleted,OrganiserID,EventTypeID,Price")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventTypeID = new SelectList(db.EventTypes, "ID", "Title", @event.EventTypeID);
            ViewBag.OrganiserID = new SelectList(db.Users, "ID", "Username", @event.OrganiserID);
            return View(@event);
        }

        // GET: Events/Delete/5
        [Authorize(Roles = "Organiser")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [Authorize(Roles = "Organiser")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            @event.Deleted = true;
            //db.Events.Remove(@event);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Organiser,Admin")]
        public ActionResult Summary()
        {
            var a = db.Users.Include(e => e.Event).Where(e => e.Deleted != true);
            var events=db.Events.Include(e => e.EventType).Include(e => e.Organiser).Where(e => e.Deleted != true);
            if (User.IsInRole ("Admin"))
            {
            var OrganiserID = db.Users.FirstOrDefault(c => c.Username == User.Identity.Name).ID;
                events = events.Where(e => e.OrganiserID == OrganiserID);
            }
            //foreach (Event aea in events){
            //    var all =  aea.Attendees.Count
            //}
            //ViewData["students"] = studentList;

            return View(events.ToList());
        }
        [Authorize(Roles = "Attendee")]
        public ActionResult Register()
        {
            var events = db.Events.Include(e => e.EventType).Include(e => e.Organiser).Where(e => e.Deleted != true);
            return View(events.ToList());
        }
        [HttpPost]
        [Authorize(Roles = "Attendee")]
        public ActionResult Register(int id)
        {
            Debug.WriteLine("----------------");
            Event @event = db.Events.Find(id);
            @event.Attendees.Add ((Attendee)db.Users.FirstOrDefault(c => c.Username == User.Identity.Name));
            //db.Events.Remove(@event);
            db.SaveChanges();
            return RedirectToAction("Register");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
