﻿using CinemaWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CinemaWeb.Areas.admin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private QLCinemaWebEntities1 db = new QLCinemaWebEntities1();

        // GET: admin/Home
        public ActionResult Index()
        {
            return View();
        }

       
    }
}