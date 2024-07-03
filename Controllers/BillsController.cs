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
    public class BillsController : Controller
    {
        private QLCinemaWebEntities db = new QLCinemaWebEntities();
        // GET: Bills/Details/5
       
        public async Task<ActionResult> History(int customerId)
        {
           
            var customerBills = await db.Bills
                                    .Where(b => b.id_account == customerId)
                                    .Include(b => b.Tickets)
                                    .OrderByDescending(b => b.date)
                                    .ToListAsync();

            return View(customerBills);
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "amount")] Bill newBill)
        {
            try
            {
                // Tạo mới một đối tượng Bill
                newBill.date = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                newBill.id_account = (int)((Account)Session["Customer"]).id;

                // Lưu bill vào cơ sở dữ liệu
                db.Bills.Add(newBill);
                db.SaveChanges();

                return Json(new { success = true, message = "Đặt vé thành công.", newBill = newBill });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi khi đặt vé: " + ex.Message});
            }
        }

      

    }
}
