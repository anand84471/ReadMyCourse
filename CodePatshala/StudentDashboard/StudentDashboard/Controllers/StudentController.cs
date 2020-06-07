using StudentDashboard.HttpRequest;
using StudentDashboard.Models;
using StudentDashboard.Models.Student;
using StudentDashboard.ServiceLayer;
using StudentDashboard.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace StudentDashboard.Controllers
{
    [RoutePrefix("Student")]
    public class StudentController : Controller
    {
        // GET: Student
        StringBuilder m_strLogMessage = new StringBuilder();
        StudentService objStudentService = new StudentService();
        public ActionResult Index()
        {
            if(Session["user_id"]!=null)
            {
                RedirectToAction("Home");
            }
            return View();
        }
        [HttpGet]
        [Route("Join")]
        public ActionResult Join()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(FormCollection collection)
        {
            try
            {
                StudentRegisterModal objRegiserModel = new StudentRegisterModal();
                objRegiserModel.m_strFirstName = collection["firstName"];
                objRegiserModel.m_strLastName = collection["lastName"];
                objRegiserModel.m_strPassword = collection["password"];
                objRegiserModel.m_strEmail = collection["email"];
                objRegiserModel.m_strPhoneNo = collection["phoneNo"];
                if (objStudentService.RegisterNewStudent(objRegiserModel))
                {
                    ViewBag.IsRegistered = true;
                    Response.Redirect("./RegistrationSuccessful?UserId=" + objRegiserModel.m_strEmail);
                }
                else
                {
                    ViewBag.IsRegistered = false;
                }

                return View();
            }
            catch (Exception Ex)
            {
                return View("Error");
            }
        }
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [HttpPost]
        public ActionResult ValidateLogin(FormCollection collection)
        {
            string strCurrentMethodName = "Register";
            string ViewName = "";
            try
            {
                StudentRegisterModal objStudentRegisterModal = new StudentRegisterModal();
                objStudentRegisterModal.m_strUserId = collection["userEmail"];
                objStudentRegisterModal.m_strPassword = collection["userPassword"];
                if (collection["remeberMe"] != null)
                {
                    objStudentRegisterModal.m_bIsRemeberMe = collection["remeberMe"].Equals("true") ? true : false;
                }

                if (objStudentService.ValidateLogin(objStudentRegisterModal))
                {

                    ViewName = "Home";
                    ViewBag.InstructorUserName = objStudentRegisterModal.m_strUserId;
                    ViewBag.IsLoggedIn = true;
                    Session["id"] = objStudentRegisterModal.m_strUserId;
                    Session["user_id"] = objStudentRegisterModal.m_llStudentId;
                    return RedirectToAction("Home");
                }
                else
                {
                    ViewName = "Index";
                    ViewBag.IsLoggedIn = false;
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                ViewName = "Error";
            }
            return View(ViewName);

        }
        [HttpGet]
        public ActionResult RegistrationSuccessful(string UserId)
        {
            ViewBag.UserMail = UserId;
            ViewBag.IsRegistered = true;
            return View();
        }
        public PartialViewResult Home()
        {
            try
            {
                long StudentId = (long)Session["user_id"];
                StudentHomeModal objStudentHomeModal = objStudentService.GetStudentHomeDetails(StudentId);
                if (objStudentHomeModal != null)
                {
                    return PartialView(objStudentHomeModal);
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "Home", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return PartialView("../Shared/Error");
        }
        [HttpGet]
        public ActionResult Logout()
        {
            string strCurrentMethodName = "Logout";
            try
            {
                Session.Remove("user_id");
                return RedirectToAction("Index");
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return RedirectToAction("Errors");
            }
        }
        [HttpGet]
        public PartialViewResult Account()
        {

            long Id = (long)Session["user_id"];
            StudentRegisterModal objModel = objStudentService.GetStudentDetails(Id);
            if (objModel == null)
            {
                return PartialView("Error");
            }
            return PartialView(objModel);
        }
        [HttpGet]
        public ActionResult EditAccount()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateDetails(FormCollection collection)
        {
            string strCurrentMethodName = "Register";
            try
            {
                StudentRegisterModal objRegiserModel = new StudentRegisterModal();
                objRegiserModel.m_strFirstName = collection["firstName"];
                objRegiserModel.m_strLastName = collection["lastName"];
                objRegiserModel.m_strPhoneNo = collection["phoneNo"];
                objRegiserModel.m_strAddressLine1 = collection["address1"];
                objRegiserModel.m_strAddressLine2 = collection["address2"];
                objRegiserModel.m_iCityId = int.Parse(collection["city"]);
                objRegiserModel.m_iStateId = int.Parse(collection["state"]);
                objRegiserModel.m_strGender = collection["gender"];
                objRegiserModel.m_strPinCode = collection["pinCode"];
                objRegiserModel.m_llStudentId = (long)Session["user_id"];
                if (objStudentService.UpdateStudentDetails(objRegiserModel))
                {
                    ViewBag.IsUpdated = true;
                    return RedirectToAction("Account");
                }
                else
                {
                    ViewBag.IsRegistered = false;
                }
                return View();
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return View("Error");
            }
        }
        [HttpGet]
        public PartialViewResult JoinNewCourse()
        {
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult JoinCourse(long id)
        {
            Session["course_id"] = id;
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult MyCourses()
        {
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult Instructors()
        {
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult JoinInstructor()
        {
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult AssignmentSubmissions()
        {
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult NewAssignment()
        {
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult GiveAssignment(long id)
        {
            ViewBag.id = id;
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult AssignmentResponse(long id)
        {
            ViewBag.id = id;
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult MyAssignments()
        {
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult NewTest()
        {
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult StartTest(long id)
        {
            ViewBag.id = id;
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult TestSubmissions()
        {
            return PartialView();
        }
        public PartialViewResult TestResponse(long id)
        {
            ViewBag.id = id;
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult LearnCourse(long id)
        {
            ViewBag.id = id;
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult TeacherProfile(long id)
        {
            ViewBag.id = id;
            return PartialView();
        }
    }
}