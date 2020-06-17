using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentDashboard.Controllers
{
    public class TestController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Details(long id, string access_code)
        {
            if (Session["student_id"] != null)
            {
                Response.Redirect("Student/TestDetails?id=" + id, true);
            }
            ViewBag.TestId = id;
            return View();
        }
    }
}