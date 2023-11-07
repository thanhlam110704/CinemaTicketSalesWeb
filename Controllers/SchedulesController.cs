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
    public class SchedulesController : Controller
    {

        private QLCinemaWebEntities db = new QLCinemaWebEntities();

        // GET: Schedules
        public ActionResult getSchedule(int id)
        {
            DateTime currentDateTime = DateTime.Now;
            
            var schedules = db.Schedules.Include(s => s.Movie).Include(s => s.Room).Where(s => s.id_movie == id && s.date >= currentDateTime).ToList();
            foreach (var schedule in schedules)
            {
                
                string dayOfWeek = schedule.date.DayOfWeek.ToString();
                schedule.dateofweek = TranslateToVietnamese(dayOfWeek); 
            }
            return PartialView(schedules);
        }
        private string TranslateToVietnamese(string dayOfWeek)
        {
            Dictionary<string, string> vietnameseDayNames = new Dictionary<string, string>
            {
        
            {"Sunday", "Chủ Nhật" },
            { "Monday", "Thứ Hai" },
            { "Tuesday", "Thứ Ba" },
            { "Wednesday", "Thứ Tư" },
            {"Thursday", "Thứ Năm" },
            { "Friday", "Thứ Sáu" },
            { "Saturday", "Thứ Bảy" }
            };

            if (vietnameseDayNames.ContainsKey(dayOfWeek))
            {
                return vietnameseDayNames[dayOfWeek];
            }

            return dayOfWeek;
        }
        public ActionResult getScheduleTime(int id)
        {
            ViewBag.id = id;
            var times = db.ScheduleTimes.Where(s => s.id_schedule == id).ToList();
            return PartialView(times);
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
