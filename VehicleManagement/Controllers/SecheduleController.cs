using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VehicleManagement.Models;

namespace VehicleManagement.Controllers
{
    public class SecheduleController : Controller
    {
        private VehicleDbContext db = new VehicleDbContext();

        // GET: /Sechedule/
        public ActionResult Index()
        {
            var schedules = db.Schedules.Include(s => s.Shifts).Include(s => s.Vehicles);
            return View(schedules.ToList());
        }

        // GET: /Sechedule/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        // GET: /Sechedule/Create
        public ActionResult Create()
        {
            ViewBag.ShiftId = new SelectList(db.Shifts, "ShiftId", "ShiftName");
            ViewBag.VehicleId = new SelectList(db.Vehicles, "VehicleId", "RegNo");
            return View();
        }

        // POST: /Sechedule/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ScheduleId,VehicleId,Dates,ShiftId,BookedBy,Address")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                var bookedschedule = db.Schedules.FirstOrDefault(
                    a =>
                        a.VehicleId == schedule.VehicleId && a.ShiftId == schedule.ShiftId &&
                        a.Dates == schedule.Dates);
                if (bookedschedule== null)
                {
                    db.Schedules.Add(schedule);
                    db.SaveChanges();
                    ViewBag.ShiftId = new SelectList(db.Shifts, "ShiftId", "ShiftName");
                    ViewBag.VehicleId = new SelectList(db.Vehicles, "VehicleId", "RegNo");
                    return View();
                }
                else
                {
                    ViewBag.ShiftId = new SelectList(db.Shifts, "ShiftId", "ShiftName");
                    ViewBag.VehicleId = new SelectList(db.Vehicles, "VehicleId", "RegNo");
                    ViewBag.Booked = "This Schedule already booked by " + bookedschedule.BookedBy;
                    return View();
                }
            }

            ViewBag.ShiftId = new SelectList(db.Shifts, "ShiftId", "ShiftName", schedule.ShiftId);
            ViewBag.VehicleId = new SelectList(db.Vehicles, "VehicleId", "RegNo", schedule.VehicleId);
            return View(schedule);
        }

        // GET: /Sechedule/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            ViewBag.ShiftId = new SelectList(db.Shifts, "ShiftId", "ShiftName", schedule.ShiftId);
            ViewBag.VehicleId = new SelectList(db.Vehicles, "VehicleId", "RegNo", schedule.VehicleId);
            return View(schedule);
        }

        // POST: /Sechedule/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ScheduleId,VehicleId,Dates,ShiftId,BookedBy,Address")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schedule).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ShiftId = new SelectList(db.Shifts, "ShiftId", "ShiftName", schedule.ShiftId);
            ViewBag.VehicleId = new SelectList(db.Vehicles, "VehicleId", "RegNo", schedule.VehicleId);
            return View(schedule);
        }

        // GET: /Sechedule/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = db.Schedules.Find(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        // POST: /Sechedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Schedule schedule = db.Schedules.Find(id);
            db.Schedules.Remove(schedule);
            db.SaveChanges();
            return RedirectToAction("Index");
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
