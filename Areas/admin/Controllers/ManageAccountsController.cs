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
using BCryptNet = BCrypt.Net.BCrypt;

namespace CinemaWeb.Areas.admin.Controllers
{
    public class ManageAccountsController : Controller
    {
        private QLCinemaWebEntities db = new QLCinemaWebEntities();

        // GET: admin/ManageAccounts
        public async Task<ActionResult> Index()
        {
            return View(await db.Accounts.ToListAsync());
        }
        public async Task<ActionResult> ResetPassword(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Account account = await db.Accounts.FindAsync(id);

            if (account == null)
            {
                return HttpNotFound();
            }

            // Đặt lại mật khẩu của khách hàng thành "thanh12345"
            string newPassword = "thanh12345";
            account.password = BCryptNet.HashPassword(newPassword);
            await db.SaveChangesAsync();

            TempData["Message"] = "Mật khẩu của khách hàng đã được đặt lại thành 'thanh12345'.";

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
