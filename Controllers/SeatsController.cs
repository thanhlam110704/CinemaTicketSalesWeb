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
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Principal;

namespace CinemaWeb.Controllers
{
    
    public class SeatsController : Controller
    {
        private QLCinemaWebEntities db = new QLCinemaWebEntities();

        // GET: Seats
      
        public async Task<ActionResult> Index()
        {
            if (Session["Customer"] == null)
            {
                // Nếu chưa đăng nhập, chuyển hướng đến trang đăng nhập
                return RedirectToAction("Login", "Account");
            }
           

            // Kiểm tra xem có giá trị hay không
            string idStr = Request.QueryString["id_scheduletime"];

            if (!string.IsNullOrEmpty(idStr) && Int32.TryParse(idStr, out int idInt))
            {
                // Gán scheduleTimeId vào ViewBag để sử dụng trong View
                ViewBag.ScheduleTimeId = idInt;

                // Tìm kiếm ScheduleTime theo id
                var tickets = await db.Tickets.Where(t => t.id_scheduletime == idInt).ToListAsync();

                ViewBag.Tickets = tickets;

                var seats = await db.Seats.Include(s => s.Room).ToListAsync();
                return View(seats);
            }

            return RedirectToAction("Login", "Account");
        }
    }
}
