using StudentDashboard.DTO;
using StudentDashboard.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace StudentDashboard.Controllers
{
    public class CourseController : Controller
    {
        StringBuilder m_strLogMessage = new StringBuilder();
        HomeDTO objHomeDTO = new HomeDTO();
        HomeService objHomeService = new HomeService();
        // GET: Course
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public PartialViewResult CourseFullDetails()
        {
            return PartialView();
        }
    }
}