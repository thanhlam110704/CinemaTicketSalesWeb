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
    public class MoviesController : Controller
    {
        private QLCinemaWebEntities db = new QLCinemaWebEntities();

        // GET: Movies/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Movie movie = await db.Movies.FindAsync(id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            return View("Details", movie);
        }
    }
}
