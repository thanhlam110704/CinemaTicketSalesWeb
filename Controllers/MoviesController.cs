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
using System.Web.Configuration;
using CinemaWeb.Models.ViewModel;

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

        [HttpGet]
        public JsonResult Search(string keyword)
        {
            try
            {
                var movieResults = db.Movies
                    .AsNoTracking()
                    .Where(movie =>
                        movie.name.ToLower().Contains(keyword.ToLower()) ||
                        movie.MovieGenres.Any(mg => mg.Genre.name.ToLower().Contains(keyword.ToLower()))
                    )
                    .ToList();

                // Chuyển đổi danh sách Movie thành danh sách MovieDTO
                var movieDTOs = MovieMapper.MapToDTOList(movieResults);

                return Json(new { data = movieDTOs }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Ghi log lỗi
                return Json(new { success = false });
            }
        }
    }
}
