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
    public class SeatsController : Controller
    {
        private QLCinemaWebEntities db = new QLCinemaWebEntities();

        // GET: Seats
        public async Task<ActionResult> Index()
        {
            var seats = db.Seats.Include(s => s.Room);
            return View(await seats.ToListAsync());
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
