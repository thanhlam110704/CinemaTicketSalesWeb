using CinemaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace CinemaWeb.Controllers
{
   
    public class HomeController : Controller
    {
        public QLCinemaWebEntities db = new QLCinemaWebEntities();
        public async Task<ActionResult> Index()
        {
            return View(await db.Movies.ToListAsync());
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