using StudentDashboard.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentDashboard.Controllers
{
    [AllowAnonymous]
    public class AssignmentController : Controller
    {
        HomeService objHomeService; 
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Details(long id,string access_code)
        {
            //id=assigment id
            //asscessid =access code
            if(Session["student_id"]!=null)
            {
                Response.Redirect("Student/Assignment/Details?id="+ id, true);
            }
            return View();
        }
        public ActionResult ViewResponse(string id)
        {
            return View();
        }
        public ActionResult Submissions(long id, string access_id)
        {
            return View();
        }
        public ActionResult Start(long id, string ShareCode)
        {
            return View();
        }
    }

}