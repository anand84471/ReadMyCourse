using StudentDashboard.Models;
using StudentDashboard.Models.Course;
using StudentDashboard.Models.Instructor;
using StudentDashboard.Models.Student;
using StudentDashboard.Security;
using StudentDashboard.Security.Instrcutor;
using StudentDashboard.ServiceLayer;
using StudentDashboard.Utilities;
using StudentDashboard.ValidationHandler;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace StudentDashboard.Controllers
{
    //[RequireHttps]
    [InstructorAuthenticationFilter]
    public class InstructorController : Controller
    {
        StringBuilder m_strLogMessage=new StringBuilder();
        InstructorService objInstructorService = new InstructorService();
        HomeService objHomeService = new HomeService();
      
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
        [AsyncTimeout(150)]
        public async Task<PartialViewResult> Home()
        {
            try
            {
                int Id = (int)Session["instructor_id"];
                InstructorRegisterModel objInstructorRegisterModel = await objInstructorService.GetInstructorPostLoginDetails(Id);
                if(objInstructorRegisterModel!=null)
                {
                    ViewBag.Token = JwtManager.GenerateToken(Id.ToString());
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
        [AsyncTimeout(150)]
        [HttpGet]
        public async Task<PartialViewResult> Account()
        {

            int Id = (int)Session["instructor_id"];
            InstructorRegisterModel objModel = await objInstructorService.GetInstructorDetails(Id);
            if(objModel==null)
            {
                return PartialView("Error");
            }
            
            return PartialView(objModel);
        }
        [AsyncTimeout(150)]
        [AllowAnonymous]
        [HttpGet]
        public ActionResult RegistrationSuccessful(string UserId)
        {
            ViewBag.UserMail = UserId;
            ViewBag.IsRegistered = true;
            return View();
        }
        [AsyncTimeout(150)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateDetails(FormCollection collection)
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
                if (await objInstructorService.UpdateInstructorDetails(objRegiserModel))
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
        [AsyncTimeout(150)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChnagePassword(FormCollection collection)
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
                if (await objInstructorService.UpdateInstructorDetails(objRegiserModel))
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
        [AsyncTimeout(150)][AllowAnonymous][HttpPost][ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(FormCollection collection)
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
               
                var objInstructorAccountRegisterValidator = new InstructorAccountRegisterValidator();
                {
                    var ValidationResult = await objInstructorAccountRegisterValidator.ValidateAsync(objRegiserModel);
                    if (ValidationResult.IsValid)
                    {
                        Regex.Replace(objRegiserModel.m_strPassword, @"\s+", "");
                        Regex.Replace(objRegiserModel.m_strEmail, @"\s+", "");
                        Regex.Replace(objRegiserModel.m_strPhoneNo, @"\s+", "");
                        if (await objInstructorService.RegisterNewUser(objRegiserModel))
                        {
                            ViewBag.IsRegistered = true;
                            Response.Redirect("./RegistrationSuccessful?UserId=" + objRegiserModel.m_strEmail);
                        }
                        else
                        {
                            ViewBag.IsRegistered = false;
                        }
                    }
                    else
                    {
                        StringBuilder m_strStringBuilder = new StringBuilder();
                        foreach (var failure in ValidationResult.Errors)
                        {
                            m_strStringBuilder.Append("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                        }
                        ViewBag.ErrorMessage = m_strStringBuilder.ToString();
                    }
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
        [AsyncTimeout(150)][ValidateAntiForgeryToken][AllowAnonymous][HttpPost]
        public async Task<ActionResult> ValidateLogin(FormCollection collection)
        {
            string strCurrentMethodName = "Register";
            string ViewName = "";
            try
            {
                InstructorRegisterModel objInstructorRegiserModel = new InstructorRegisterModel();
                objInstructorRegiserModel.m_strEmail = collection["userEmail"];
                objInstructorRegiserModel.m_strPassword = collection["userPassword"];
                if (collection["remeberMe"] != null)
                {
                    objInstructorRegiserModel.m_bIsRememberMe = collection["remeberMe"].Equals("true") ? true : false;
                }
                var objInstructorLoginValidator = new InstructorLoginValidator();
                {
                    var ValidationResult = await objInstructorLoginValidator.ValidateAsync(objInstructorRegiserModel);
                    if (ValidationResult.IsValid)
                    {
                        if (await objInstructorService.ValidateLoginDetails(objInstructorRegiserModel))
                        {
                            //objJWTTokenIssuer.GenerateToken(objInstructorRegiserModel.m_iInstructorId.ToString());
                            ViewName = "Home";
                            ViewBag.InstructorUserName = objInstructorRegiserModel.m_strEmail;
                            ViewBag.IsLoggedIn = true;
                            Session["instructor_id"] = objInstructorRegiserModel.m_iInstructorId;
                            Session["instructor_user_name"] = objInstructorRegiserModel.m_strEmail;
                           
                            return RedirectToAction("Home");
                        }
                        else
                        {
                            ViewName = "Index";
                            ViewBag.IsLoggedIn = false;
                        }
                    }
                    else
                    {
                        StringBuilder m_strStringBuilder = new StringBuilder();
                        foreach (var failure in ValidationResult.Errors)
                        {
                            m_strStringBuilder.Append("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                        }
                        ViewBag.ErrorMessage = m_strStringBuilder.ToString();
                    }
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
        [AsyncTimeout(150)]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> ResetPassword(FormCollection collection)
        {
            string strCurrentMethodName = "ResetPassword";
            string ViewName = "";
            try
            {
                InstructorRegisterModel objInstructorRegisterModel = new InstructorRegisterModel();
                objInstructorRegisterModel.m_strEmail = collection["userEmail"];
                string token = await objInstructorService.InsertPasswordRecovery(objInstructorRegisterModel.m_strEmail);
                if (token != null && token != string.Empty)
                {
                    string sid = objInstructorRegisterModel.m_strEmail;
                    return RedirectToAction("PasswordAuthRequest", new { sid, token });
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
        [AsyncTimeout(150)]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> RequestStartMeeting(FormCollection collection)
        {
            string strCurrentMethodName = "ResetPassword";
            string ViewName = "Error";
            try
            {
                JitsiMeetingModal objModal = new JitsiMeetingModal();
                long.TryParse(collection["classroom_id"],out objModal.m_llClassroomId);
                if (await objInstructorService.InertNewMeetingToClassroom(objModal))
                {
                    return Redirect("StartMeeting?ClassroomId="+ objModal.m_llClassroomId);
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                
            }
            return View(ViewName);

        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult PasswordAuthRequest(string sid, string token)
        {
            string strCurrentMethodName = "PasswordAuthRequest";
            try
            {
                ViewBag.StudnentUserName = sid;
                ViewBag.Token = token;
                return View();
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
            }
            return View("Error");

        }
        [HttpGet]
        public PartialViewResult CreateNewCourse()
        {
            try
            {
                return PartialView("NewCourse");
            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "CreateNewCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }
        }
        [HttpGet]
        public PartialViewResult CreateClassRoom()
        {
            try
            {
                return PartialView("CreateClassRoom");
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "CreateNewCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }
        }
        [HttpGet]
        public PartialViewResult Classrooms()
        {
            try
            {
                return PartialView("Classrooms");
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "CreateNewCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }
        }
        [HttpGet]
        public PartialViewResult CreateTest()
        {
            try
            {
                return PartialView();
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "CreateTest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }
        }
        [HttpGet]
        public PartialViewResult StartMetting()
        {
            try
            {
                return PartialView();
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "CreateTest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }
        }
        [HttpGet]
        public async Task<PartialViewResult> StartMeeting(long ClassroomId)
        {
            try
            {
                JitsiMeetingModal objJitsiMeetingModal = await objInstructorService.GetClassroomMeetingDetails(ClassroomId,-1);
                return PartialView(objJitsiMeetingModal);
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "CreateTest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }
        }
        [HttpGet]
        public PartialViewResult CreateAssignment()
        {
            try
            {
                return PartialView();
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "CreateAssignment", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }
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
            string strCurrentMethodName = "Courses";
            try
            {
                return PartialView();
            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Errors");
            }
        }
        [HttpGet]
        public PartialViewResult Assignments()
        {
            string strCurrentMethodName = "Assignments";
            try
            {
                return PartialView();
            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Errors");
            }
        }
        [HttpGet]
        public PartialViewResult Tests()
        {
            string strCurrentMethodName = "Tests";
            try
            {
                return PartialView();
            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Errors");
            }
        }
        [HttpGet]
        public PartialViewResult Activity()
        {
            string strCurrentMethodName = "Activity";
            try
            {
                return PartialView();
            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Errors");
            }
            
        }
        [HttpGet]
        public async Task<ActionResult> ViewCourse(long id)
        {
            string strCurrentMethodName = "ViewCourse";
            try
            {
                if(await objHomeService.CheckCourseIdExistsForInstrcutor((int)Session["instructor_id"],id))
                {
                    ViewBag.CourseId = id;
                }
                else
                {
                    return RedirectToAction("Home");
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Errors");
            }
            return PartialView();
        }
        [HttpGet]
        public async Task<PartialViewResult> ClassroomDashboard(long id)
        {
            string strCurrentMethodName = "ClassroomDashboard";
            try
            {
                if(await objInstructorService.CheckClassroomAccess(id, (int)Session["instructor_id"])){
                    ClassRoomModal objClassRoomModal = await objInstructorService.GetClassroomDetailsForInstructor(id,
                    (int)Session["instructor_id"]);
                    return PartialView(objClassRoomModal);
                }
            }
            catch (Exception Ex)
            {

                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Errors");
            }
            return PartialView("UnAuthorizedAccess");
        }
        [HttpGet]
        public PartialViewResult PreviewCourse(int id)
        {
            string strCurrentMethodName = "PreviewCourse";
            try
            {
                return PartialView();
            }
            catch(Exception Ex)
            {

                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Errors");
            }
        }
        [HttpGet]
        public PartialViewResult ViewAssignment(long id)
        {
            string strCurrentMethodName = "ViewAssignment";
            try
            {
                ViewBag.AssignmnetId = id;
                return PartialView();
            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Errors");
            }
           
        }
        [HttpGet]
        public PartialViewResult ViewTest(long id)
        {
            string strCurrentMethodName = "ViewTest";
            try
            {
                ViewBag.TestId = id;
                return PartialView();
            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Errors");
            }
           
        }
        [HttpGet]
        public PartialViewResult AssignmentSubmissions(long id)
        {
            string strCurrentMethodName = "AssignmentSubmissions";
            try
            {
                ViewBag.TestId = id;
                return PartialView();
            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Errors");
            }
           
        }
        [HttpGet]
        public PartialViewResult TestSubmissions(long id)
        {
            string strCurrentMethodName = "TestSubmissions";
            try
            {
                ViewBag.id = id;
                return PartialView();
            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Errors");
            }
          
        }
        [HttpGet]
        public PartialViewResult StudentsJoined()
        {
            string strCurrentMethodName = "StudentsJoined";
            try
            {
                return PartialView();
            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Errors");
            }
           
        }
        [HttpGet]
        public PartialViewResult AssignmentResponse(long id,long AssId,long viewer)
        {
            string strCurrentMethodName = "StudentsJoined";
            try
            {
                ViewBag.AssignmentId = AssId;
                ViewBag.id = id;
                ViewBag.StudentId = viewer;
                return PartialView();
            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Errors");
            }
           
        }
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> SubmitUpdatePasswordOtp(FormCollection collection)
        {
            string strCurrentMethodName = "SubmitUpdatePasswordOtp";
            string ViewName = "";
            try
            {
                StudentUpdatePasswordRequestModal objStudentUpdatePasswordRequestModal = new StudentUpdatePasswordRequestModal();
                objStudentUpdatePasswordRequestModal.m_strUserName = collection["userName"];
                objStudentUpdatePasswordRequestModal.m_strToken = collection["token"];
                objStudentUpdatePasswordRequestModal.m_strOtp = collection["otp"];
                string sid = objStudentUpdatePasswordRequestModal.m_strUserName;
                string token = objStudentUpdatePasswordRequestModal.m_strToken;
                if (await objInstructorService.ValidatePasswordRecodevrtOtp(objStudentUpdatePasswordRequestModal))
                {

                    return RedirectToAction("ChangePassword", new { sid, token });
                }
                else
                {
                    ViewBag.StudnentUserName = sid;
                    ViewBag.Token = token;
                    ViewBag.InvalidOtp = true;
                    return View("PasswordAuthRequest");
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
        public PartialViewResult TestResponse(long id,long TestId,long viewer)
        {
            string strCurrentMethodName = "StudentsJoined";
            try
            {
                ViewBag.id = id;
                ViewBag.TestId = TestId;
                ViewBag.StudentId = viewer;
                return PartialView();
            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Errors");
            }
        }
        [HttpGet]
        public PartialViewResult CreateInterativeCourse()
        {
            string strCurrentMethodName = "CreateInterativeCourse";
            try
            {
                return PartialView();
            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Errors");
            }
           
        }
        [HttpGet]
        public PartialViewResult Search(string q)
        {
            string strCurrentMethodName = "Search";
            try
            {
                ViewBag.q = q;
                return PartialView();
            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", strCurrentMethodName, Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Errors");
            }
        }
        [HttpGet]
        public ActionResult PasswordUpdatedSuccessfully(string UserId)
        {
            ViewBag.UserMail = UserId;
            ViewBag.IsRegistered = true;
            return View();
        }
        [HttpGet]
        public PartialViewResult ChangePassword(string sid, string token)
        {
            ViewBag.StudentId = sid;
            ViewBag.Token = token;
            return PartialView();
        }
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> ChangePasswordNow(FormCollection collection)
        {
            string strCurrentMethodName = "ChangePasswordNow";
            string ViewName = "";
            try
            {
                StudentUpdatePasswordRequestModal objStudentUpdatePasswordRequestModal = new StudentUpdatePasswordRequestModal();
                objStudentUpdatePasswordRequestModal.m_strUserName = collection["userName"];
                objStudentUpdatePasswordRequestModal.m_strToken = collection["token"];
                objStudentUpdatePasswordRequestModal.m_strPassword = collection["password"];
                objStudentUpdatePasswordRequestModal.m_strMatchPassword = collection["confirmPassword"];
                string sid = objStudentUpdatePasswordRequestModal.m_strUserName;
                string token = objStudentUpdatePasswordRequestModal.m_strToken;
                if (await objInstructorService.ChangePasswordAfterAuth(objStudentUpdatePasswordRequestModal))
                {
                    string UserId = objStudentUpdatePasswordRequestModal.m_strUserName;
                    return RedirectToAction("PasswordUpdatedSuccessfully", new { UserId });
                }
                else
                {
                    ViewBag.StudnentUserName = sid;
                    ViewBag.Token = token;
                    ViewBag.InvalidOtp = true;
                    return View("PasswordAuthRequest");
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
    }
}