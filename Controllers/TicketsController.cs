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

namespace CinemaWeb.Controllers
{
    public class TicketsController : Controller
    {
        private QLCinemaWebEntities db = new QLCinemaWebEntities();

        // GET: Tickets
        public async Task<ActionResult> Index(int id_bill)
        {
            var tickets = await db.Tickets
                                  .Where(t => t.id_bill == id_bill)
                                  .Include(t => t.Bill)
                                  .Include(t => t.ScheduleTime)
                                  .ToListAsync();

            return View(tickets);
        }

        // GET: Tickets/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = await db.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Create

        [HttpPost]
        public ActionResult Create([Bind(Include = "seat,price,id_bill,id_scheduletime")] Ticket newTicket)
        {
            try
            {
                newTicket.status = 0;
                db.Tickets.Add(newTicket);
                db.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false });
            }
        }
    
    }
}
