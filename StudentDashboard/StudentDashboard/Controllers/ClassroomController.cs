using StudentDashboard.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StudentDashboard.Controllers
{
    public class ClassroomController : Controller
    {
        DocumentService objDocumentService = new DocumentService();

        // GET: Classroom
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> Details(long id, string access_code)
        {
            if(await objDocumentService.CheckClassroomAccess(id,access_code))
            {
                if (Session["user_id"] != null)
                {

                    Response.Redirect(MvcApplication._strApplicationBaseUrl + "/Student/JoinClassroom?id=" + id+"&&access_code="+access_code);
                }
                ViewBag.ReturnUrl = MvcApplication._strApplicationBaseUrl + "/student?return_url=" + MvcApplication._strApplicationBaseUrl + "/Student/JoinClassroom?id=" + id+ "&&access_code=" + access_code;
                ViewBag.Id = id;
                return PartialView();
            }
            return PartialView();
        }
    }
}