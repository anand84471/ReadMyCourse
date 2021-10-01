using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentDashboard.Controllers
{
    public class BrowseController : Controller
    {
        // GET: Browse
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DataScience()
        {
            return View();
        }
      

    }
}