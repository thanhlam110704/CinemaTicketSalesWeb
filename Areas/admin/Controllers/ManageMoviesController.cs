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
using System.IO;

namespace CinemaWeb.Areas.admin.Controllers
{
    public class ManageMoviesController : Controller
    {
        private QLCinemaWebEntities db = new QLCinemaWebEntities();

        // GET: admin/ManageMovies
        public async Task<ActionResult> Index()
        {
            return View(await db.Movies.ToListAsync());
        }

        // GET: admin/ManageMovies/Details/5
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
            return View(movie);
        }

        // GET: admin/ManageMovies/Create
        public async Task<ActionResult> Create()
        {
            List<Genre> genres = await db.Genres.ToListAsync();
            ViewBag.Genres = new SelectList(genres, "id", "name");
            return View();
        }

        // POST: admin/ManageMovies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,name,img,trailer,length_time,description,status,ageallow")] Movie movie, HttpPostedFileBase img, int[] selectedGenre)
        {
            var path = "";
            var filename = "";
            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + img.FileName;
                    path = Path.Combine(Server.MapPath("~/wwwroot/upload/img/movies"), filename);
                    img.SaveAs(path);
                    movie.img = filename;
                }
                else
                {
                    movie.img = "logo.png";
                }
                if (selectedGenre != null)
                {
                    foreach (var genreId in selectedGenre)
                    {
                        MovieGenre temp = new MovieGenre();
                        temp.id_movie = movie.id;
                        temp.id_genre= genreId;
                        db.MovieGenres.Add(temp);
                    }
                }
                db.Movies.Add(movie);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(movie);
        }

        // GET: admin/ManageMovies/Edit/5
        public async Task<ActionResult> Edit(int? id)
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
            List<Genre> genres = await db.Genres.ToListAsync();
            ViewBag.Genres = new SelectList(genres, "id", "name");
            return View(movie);
        }

        // POST: admin/ManageMovies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,name,img,trailer,length_time,description,status,ageallow")] Movie movie, HttpPostedFileBase img, int[] selectedGenre)
        {
            var path = "";
            var filename = "";
            Movie temp = db.Movies.Find(movie.id);

            if (ModelState.IsValid)
            {
                if (img != null)
                {
                    filename = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss-") + img.FileName;
                    path = Path.Combine(Server.MapPath("~/wwwroot/upload/img/movies"), filename);
                    img.SaveAs(path);
                    db.Entry(temp).Property(x => x.img).CurrentValue = filename;
                }

                temp.name = movie.name;
                temp.length_time = movie.length_time;
                temp.description = movie.description;
                temp.status = movie.status;
                temp.trailer = movie.trailer;
                temp.ageallow = movie.ageallow;

                List<MovieGenre> currentGenres = new List<MovieGenre>(temp.MovieGenres); // Sao chép danh sách thể loại hiện tại

                foreach (var genre in temp.MovieGenres.ToList())
                {
                    if (selectedGenre == null || !selectedGenre.Contains(genre.id_genre))
                    {
                        db.MovieGenres.Remove(genre);
                    }
                }

                if (selectedGenre != null)
                {
                    foreach (var genreId in selectedGenre)
                    {
                        if (!currentGenres.Any(mg => mg.id_genre == genreId))
                        {
                            var movieGenre = new MovieGenre
                            {
                                id_movie = temp.id,
                                id_genre = genreId
                            };
                            db.MovieGenres.Add(movieGenre);
                        }
                    }
                }

                db.Entry(temp).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(movie);
        }

        // GET: admin/ManageMovies/Delete/5
        public async Task<ActionResult> Delete(int? id)
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
            return View(movie);
        }

        // POST: admin/ManageMovies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Movie movie = await db.Movies.FindAsync(id);
            db.Movies.Remove(movie);
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
