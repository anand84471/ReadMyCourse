using StudentDashboard.Models;
using StudentDashboard.Models.Course;
using StudentDashboard.ServiceLayer;
using StudentDashboard.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace StudentDashboard.Controllers
{
    //[Authorize]
    public class InstructorController : Controller
    {
        StringBuilder m_strLogMessage=new StringBuilder();
        InstructorService objInstructorService = new InstructorService();
        // GET: Instructor
        [AllowAnonymous]
        public ActionResult Index()
        {
           return View();
        }
        [HttpGet]
        public ActionResult EditAccount()
        {
            return View();
        }
        public PartialViewResult Home()
        {
            try
            {
                int Id = (int)Session["instructor_id"];
                InstructorRegisterModel objInstructorRegisterModel = objInstructorService.GetInstructorPostLoginDetails(Id);
                if(objInstructorRegisterModel!=null)
                {
                    return PartialView(objInstructorRegisterModel);
                }
            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "Home", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return PartialView("../Shared/Error");
        }
        [HttpGet]
        public PartialViewResult ForgotPassword()
        {
            return PartialView();
        }
        [AllowAnonymous]
        [HttpGet] 
        public ActionResult Join()
        {
            return View();
        }
        [HttpGet]
        public PartialViewResult Contact()
        {
            return PartialView();
        }

        [HttpGet]
        public PartialViewResult Account()
        {

            int Id = (int)Session["instructor_id"];
            InstructorRegisterModel objModel = objInstructorService.GetInstructorDetails(Id);
            if(objModel==null)
            {
                return PartialView("Error");
            }
            
            return PartialView(objModel);
        }
       
        [HttpGet]
        public ActionResult RegistrationSuccessful(string UserId)
        {
            ViewBag.UserMail = UserId;
            ViewBag.IsRegistered = true;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateDetails(FormCollection collection)
        {
            string strCurrentMethodName = "Register";
            try
            {
                InstructorRegisterModel objRegiserModel = new InstructorRegisterModel();
                objRegiserModel.m_strFirstName = collection["firstName"];
                objRegiserModel.m_strLastName = collection["lastName"];
                objRegiserModel.m_strPhoneNo = collection["phoneNo"];
                objRegiserModel.m_strAddressLine1 = collection["address1"];
                objRegiserModel.m_strAddressLine2 = collection["address2"];
                objRegiserModel.m_iCityid = int.Parse(collection["city"]);
                objRegiserModel.m_iStateId = int.Parse(collection["state"]);
                objRegiserModel.m_strGender = collection["gender"];
                objRegiserModel.m_strPinCode = collection["pinCode"];
                objRegiserModel.m_iInstructorId = (int)Session["instructor_id"];
                if (objInstructorService.UpdateInstructorDetails(objRegiserModel))
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChnagePassword(FormCollection collection)
        {
            string strCurrentMethodName = "ChnagePassword";
            try
            {
                InstructorRegisterModel objRegiserModel = new InstructorRegisterModel();
                objRegiserModel.m_strFirstName = collection[""];
                objRegiserModel.m_strLastName = collection["lastName"];
                objRegiserModel.m_strPhoneNo = collection["phoneNo"];
                objRegiserModel.m_strAddressLine1 = collection["address1"];
                objRegiserModel.m_strAddressLine2 = collection["address2"];
                objRegiserModel.m_iCityid = int.Parse(collection["city"]);
                objRegiserModel.m_iStateId = int.Parse(collection["state"]);
                objRegiserModel.m_strGender = collection["gender"];
                objRegiserModel.m_strPinCode = collection["pinCode"];
                objRegiserModel.m_iInstructorId = (int)Session["instructor_id"];
                if (objInstructorService.UpdateInstructorDetails(objRegiserModel))
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(FormCollection collection)
        {
            string strCurrentMethodName = "Register";
            try
            {
                InstructorRegisterModel objRegiserModel = new InstructorRegisterModel();
                objRegiserModel.m_strFirstName = collection["firstName"];
                objRegiserModel.m_strLastName = collection["lastName"];
                objRegiserModel.m_strPassword = collection["password"];
                objRegiserModel.m_strEmail = collection["email"];
                objRegiserModel.m_strPhoneNo = collection["phoneNo"];
                if (objInstructorService.RegisterNewUser(objRegiserModel))
                {
                    ViewBag.IsRegistered = true;
                    Response.Redirect("./RegistrationSuccessful?UserId="+ objRegiserModel.m_strEmail);
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
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [HttpPost]
        public ActionResult ValidateLogin(FormCollection collection)
        {
            string strCurrentMethodName = "Register";
            string ViewName = "";
            try
            {
                InstructorRegisterModel objInstructorRegiserModel = new InstructorRegisterModel();
                objInstructorRegiserModel.m_strEmail = collection["userEmail"];
                objInstructorRegiserModel.m_strPassword = collection["userPassword"];
                if(collection["remeberMe"]!=null)
                {
                    objInstructorRegiserModel.m_bIsRememberMe = collection["remeberMe"].Equals("true") ? true : false;
                }

                if (objInstructorService.ValidateLoginDetails(objInstructorRegiserModel))
                {
                    ViewName = "Home";
                    ViewBag.InstructorUserName = objInstructorRegiserModel.m_strEmail;
                    ViewBag.IsLoggedIn = true;
                    Session["instructor_id"] = objInstructorRegiserModel.m_iInstructorId;
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
        public PartialViewResult CreateNewCourse()
        {
            return PartialView("NewCourse");
        }
        [HttpGet]
        public PartialViewResult CreateTest()
        {
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult CreateAssignment()
        {
            return PartialView();
        }
        [HttpPost]
        public String FileUpload(FormCollection collection, HttpPostedFileBase[] fileUploads)
        {
            String path = null;
            String strServerPathOfUploadedFile = null;
            if (ModelState.IsValid)
            {
                if (fileUploads == null)
                {
                    ModelState.AddModelError("File", "Please Upload Your file");
                }
                foreach (var fileUpload in fileUploads)
                {
                    if (fileUpload.ContentLength > 0)
                    {
                        int MaxContentLength = 1024 * 1024 * 3; //3 MB
                        string[] AllowedFileExtensions = new string[] { ".jpg", ".gif", ".png", ".pdf", ".mp4", ".jpeg", ".MVI", ".tiff", ".tif" };
                        var FileExtension = fileUpload.FileName.Substring(fileUpload.FileName.LastIndexOf('.'));
                        if (!AllowedFileExtensions.Contains(FileExtension))
                        {
                            ModelState.AddModelError("File", "Please file of type: " + string.Join(", ", AllowedFileExtensions));
                        }

                        else if (fileUpload.ContentLength > MaxContentLength)
                        {
                            ModelState.AddModelError("File", "Your file is too large, maximum allowed size is: " + MaxContentLength + " MB");
                        }
                        else
                        {
                            var fileName = $@"{Guid.NewGuid()}" + FileExtension;
                            path = Path.Combine(Server.MapPath("~/Uploads/Images"), fileName);
                            strServerPathOfUploadedFile = "/ Uploads / Images/" + fileName;
                            if (FileExtension.Equals(".mp4"))
                            {
                                path = Path.Combine(Server.MapPath("~/Uploads/Videos"), fileName);
                                strServerPathOfUploadedFile = "/ Uploads / Videos/" + fileName;
                            }
                            fileUpload.SaveAs(path);
                            ModelState.Clear();
                        }
                    }
                }
            }
            return strServerPathOfUploadedFile;
        }
        [HttpGet]
        public ActionResult Logout()
        {
            string strCurrentMethodName = "Logout";
            try
            {
                Session.Remove("instructor_id");
                return RedirectToAction("Index");
            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return RedirectToAction("Errors");
            }
        }
        [HttpGet]
        public PartialViewResult Courses()
        {
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult Assignments()
        {
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult Tests()
        {
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult Activity()
        {
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult ViewCourse(int id)
        {
            Session["course_id"] = id;
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult PreviewCourse(int id)
        {
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult ViewAssignment(long id)
        {
            ViewBag.AssignmnetId = id;
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult ViewTest(long id)
        {
            ViewBag.TestId = id;
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult AssignmentSubmissions(long id)
        {
            ViewBag.TestId = id;
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult TestSubmissions(long id)
        {
            ViewBag.id = id;
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult StudentsJoined()
        {
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult AssignmentResponse(long id,long AssId)
        {
            ViewBag.AssignmentId = AssId;
            ViewBag.id = id;
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult TestResponse(long id,long TestId)
        {
            ViewBag.id = id;
            ViewBag.TestId = TestId;
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult CreateInterativeCourse()
        {
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult Search(string q)
        {
            ViewBag.q = q;
            return PartialView();
        }
    }
}