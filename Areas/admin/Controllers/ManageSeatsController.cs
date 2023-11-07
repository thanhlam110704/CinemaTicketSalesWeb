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
    public class ManageSeatsController : Controller
    {
        private QLCinemaWebEntities db = new QLCinemaWebEntities();

        // GET: admin/ManageSeats
        public async Task<ActionResult> Index()
        {
            var seats = db.Seats.Include(s => s.Room);
            return View(await seats.ToListAsync());
        }

        // GET: admin/ManageSeats/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seat seat = await db.Seats.FindAsync(id);
            if (seat == null)
            {
                return HttpNotFound();
            }
            return View(seat);
        }

        // GET: admin/ManageSeats/Create
        public ActionResult Create()
        {
            ViewBag.id_room = new SelectList(db.Rooms, "id", "name");
            return View();
        }

        // POST: admin/ManageSeats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,name,type,price,id_room,quality")] Seat seat)
        {
            if (ModelState.IsValid)
            {
                db.Seats.Add(seat);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.id_room = new SelectList(db.Rooms, "id", "name", seat.id_room);
            return View(seat);
        }

        // GET: admin/ManageSeats/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seat seat = await db.Seats.FindAsync(id);
            if (seat == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_room = new SelectList(db.Rooms, "id", "name", seat.id_room);
            return View(seat);
        }

        // POST: admin/ManageSeats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,name,type,price,id_room,quality")] Seat seat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(seat).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.id_room = new SelectList(db.Rooms, "id", "name", seat.id_room);
            return View(seat);
        }

        // GET: admin/ManageSeats/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seat seat = await db.Seats.FindAsync(id);
            if (seat == null)
            {
                return HttpNotFound();
            }
            return View(seat);
        }

        // POST: admin/ManageSeats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Seat seat = await db.Seats.FindAsync(id);
            db.Seats.Remove(seat);
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
