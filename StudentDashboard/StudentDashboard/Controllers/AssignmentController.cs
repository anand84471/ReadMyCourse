using StudentDashboard.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StudentDashboard.Controllers
{
    [AllowAnonymous]
    public class AssignmentController : Controller
    {
       
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Details(long id,string access_code)
        {
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
        
    }

}