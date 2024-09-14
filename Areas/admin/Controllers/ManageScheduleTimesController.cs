using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CinemaWeb.Models;

namespace CinemaWeb.Areas.admin.Controllers
{
    public class ManageScheduleTimesController : Controller
    {
        private QLCinemaWebEntities1 db = new QLCinemaWebEntities1();

        // GET: admin/ManageScheduleTimes
        public async Task<ActionResult> Index()
        {
            var scheduleTimes = db.ScheduleTimes.Include(s => s.Schedule);
            return View(await scheduleTimes.ToListAsync());
        }

        // GET: admin/ManageScheduleTimes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduleTime scheduleTime = await db.ScheduleTimes.FindAsync(id);
            if (scheduleTime == null)
            {
                return HttpNotFound();
            }
            return View(scheduleTime);
        }

        // GET: admin/ManageScheduleTimes/Create
        public ActionResult Create()
        {
            ViewBag.id_schedule = new SelectList(db.Schedules, "id", "id");
            return View();
        }

        // POST: admin/ManageScheduleTimes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,time,id_schedule")] ScheduleTime scheduleTime)
        {
            if (ModelState.IsValid)
            {
                db.ScheduleTimes.Add(scheduleTime);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.id_schedule = new SelectList(db.Schedules, "id", "id", scheduleTime.id_schedule);
            return View(scheduleTime);
        }

        // GET: admin/ManageScheduleTimes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduleTime scheduleTime = await db.ScheduleTimes.FindAsync(id);
            if (scheduleTime == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_schedule = new SelectList(db.Schedules, "id", "id", scheduleTime.id_schedule);
            return View(scheduleTime);
        }

        // POST: admin/ManageScheduleTimes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,time,id_schedule")] ScheduleTime scheduleTime)
        {
            if (ModelState.IsValid)
            {
                db.Entry(scheduleTime).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.id_schedule = new SelectList(db.Schedules, "id", "id", scheduleTime.id_schedule);
            return View(scheduleTime);
        }

        // GET: admin/ManageScheduleTimes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ScheduleTime scheduleTime = await db.ScheduleTimes.FindAsync(id);
            if (scheduleTime == null)
            {
                return HttpNotFound();
            }
            return View(scheduleTime);
        }

        // POST: admin/ManageScheduleTimes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ScheduleTime scheduleTime = await db.ScheduleTimes.FindAsync(id);
            db.ScheduleTimes.Remove(scheduleTime);
            await db.SaveChangesAsync();
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
