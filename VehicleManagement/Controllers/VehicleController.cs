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
    public class VehicleController : Controller
    {
        private VehicleDbContext db = new VehicleDbContext();

        // GET: /Vehicle/
        public ActionResult Index()
        {
            return View(db.Vehicles.ToList());
        }

        public ActionResult BookedSchedule()
        {
            ViewBag.VehicleId = new SelectList(db.Vehicles, "VehicleId", "RegNo");
            return View();
        }

        [HttpPost]
        public ActionResult BookedSchedule(int VehicleId)
        {
            ViewBag.VehicleId = new SelectList(db.Vehicles, "VehicleId", "RegNo");
            var bookedSchedule = db.Schedules.Where(a => a.VehicleId == VehicleId && a.Dates >= DateTime.Today).ToList();
            return View(bookedSchedule);
        }

        public ActionResult DateSchedule()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DateSchedule(DateTime From,DateTime To)
        {
            
            var bookedSchedule = db.Schedules.Where(a => a.Dates >= From && a.Dates <= To).ToList();
            return View(bookedSchedule);
        }
        // GET: /Vehicle/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // GET: /Vehicle/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Vehicle/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="VehicleId,RegNo,EngineNo")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                db.Vehicles.Add(vehicle);
                db.SaveChanges();
                return View();
            }

            return View(vehicle);
        }

        // GET: /Vehicle/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // POST: /Vehicle/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="VehicleId,RegNo,EngineNo")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vehicle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vehicle);
        }

        // GET: /Vehicle/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // POST: /Vehicle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Vehicle vehicle = db.Vehicles.Find(id);
            db.Vehicles.Remove(vehicle);
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

        public JsonResult RegNoExits(string regno)
        {
            var aCategory = db.Vehicles.FirstOrDefault(x => x.RegNo == regno);
            if (aCategory != null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EngineNoExits(string engineno)
        {
            var aCategory = db.Vehicles.FirstOrDefault(x => x.EngineNo == engineno);
            if (aCategory != null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
