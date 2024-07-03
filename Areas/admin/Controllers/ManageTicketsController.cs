using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using CinemaWeb.Models;

namespace CinemaWeb.Areas.admin.Controllers
{
    public class ManageTicketsController : Controller
    {
        private QLCinemaWebEntities db = new QLCinemaWebEntities();

        // GET: admin/ManageTickets
        public async Task<ActionResult> Index()
        {
            var tickets = db.Tickets.Include(t => t.Bill).Include(t => t.ScheduleTime);
            return View(await tickets.ToListAsync());
        }

        [HttpPost]
        public JsonResult UpdateStatus(int id)
        {
            try
            {
                var item = db.Tickets.Find(id);

                if (item == null)
                {
                    return Json(new { success = false, error = "Ticket not found" });
                }

                // Update the status to 1
                item.status = 1;

                // Save changes to the database
                db.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating status: {ex.Message}");
                return Json(new { success = false, error = ex.Message });
            }
        }
    }
}
