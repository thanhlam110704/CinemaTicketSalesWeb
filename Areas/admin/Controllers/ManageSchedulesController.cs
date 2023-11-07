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
    public class ManageSchedulesController : Controller
    {
        private QLCinemaWebEntities db = new QLCinemaWebEntities();

        // GET: admin/ManageSchedules
        public async Task<ActionResult> Index()
        {
            var schedules = db.Schedules.Include(s => s.Movie).Include(s => s.Room);
            return View(await schedules.ToListAsync());
        }
        // GET: admin/ManageSchedules/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = await db.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        // GET: admin/ManageSchedules/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.id_movie = new SelectList(db.Movies, "id", "name");
            ViewBag.id_room = new SelectList(db.Rooms, "id", "name");
            return View();
        }

        // POST: admin/ManageSchedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,date,time,id_room,id_movie")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                // Lấy tên phim và tên rạp dựa trên id_movie và id_room
                var movie = db.Movies.Find(schedule.id_movie);
                var room = db.Rooms.Find(schedule.id_room);

                // Kiểm tra xem movie và room có tồn tại không
                if (movie == null || room == null)
                {
                    ModelState.AddModelError("", "Tên phim hoặc tên rạp không hợp lệ.");
                    ViewBag.id_movie = new SelectList(db.Movies, "id", "name", schedule.id_movie);
                    ViewBag.id_room = new SelectList(db.Rooms, "id", "name", schedule.id_room);
                    return View(schedule);
                }

                // Cập nhật tên phim và tên rạp cho schedule
                schedule.Movie = movie;
                schedule.Room = room;

                db.Schedules.Add(schedule);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.id_movie = new SelectList(db.Movies, "id", "name", schedule.id_movie);
            ViewBag.id_room = new SelectList(db.Rooms, "id", "name", schedule.id_room);
            return View(schedule);
        }
        // GET: admin/ManageSchedules/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = await db.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_movie = new SelectList(db.Movies, "id", "name", schedule.id_movie);
            ViewBag.id_room = new SelectList(db.Rooms, "id", "name", schedule.id_room);
            return View(schedule);
        }

        // POST: admin/ManageSchedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,date,time,id_room,id_movie")] Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                Schedule temp = db.Schedules.Find(schedule.id);

                temp.date = schedule.date;
               
                temp.id_room = schedule.id_room;
                temp.id_movie = schedule.id_movie;

                db.Entry(temp).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
          
            ViewBag.id_movie = new SelectList(db.Movies, "id", "name", schedule.id_movie);
            ViewBag.id_room = new SelectList(db.Rooms, "id", "name", schedule.id_room);

            return View(schedule);
        }

        // GET: admin/ManageSchedules/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Schedule schedule = await db.Schedules.FindAsync(id);
            if (schedule == null)
            {
                return HttpNotFound();
            }
            return View(schedule);
        }

        // POST: admin/ManageSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Schedule schedule = await db.Schedules.FindAsync(id);
            db.Schedules.Remove(schedule);
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
