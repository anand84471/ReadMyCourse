using StudentDashboard.HttpResponse;
using StudentDashboard.Models.Student;
using StudentDashboard.Security;
using StudentDashboard.Security.Student;
using StudentDashboard.ServiceLayer;
using StudentDashboard.Utilities;
using StudentDashboard.ValidationHandler;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace StudentDashboard.Controllers
{
    [RoutePrefix("Student")]
    [StudentAuthenticationFilter]
    public class StudentController : Controller
    {
        // GET: Student
        StringBuilder m_strLogMessage = new StringBuilder();
        StudentService objStudentService = new StudentService();
        public ActionResult Index(string return_url=null)
        {
            if(return_url!=null)
            {
                ViewBag.ReturnUrl = "/Student/ValidateLogin?return_url="+return_url;
            }
            else
            {
                ViewBag.ReturnUrl = "/Student/ValidateLogin";
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
        public async Task<ActionResult> Register(FormCollection collection)
        {
            try
            {
                StudentRegisterModal objRegiserModel = new StudentRegisterModal();
                objRegiserModel.m_strFirstName = collection["firstName"];
                objRegiserModel.m_strLastName = collection["lastName"];
                objRegiserModel.m_strPassword = collection["password"];
                objRegiserModel.m_strEmail = collection["email"];
                objRegiserModel.m_strPhoneNo = collection["phoneNo"];
                var objDynamicRoutingAPIRequestValidator = new StudentAccountRegisterValidator();
                {
                    var result=await objDynamicRoutingAPIRequestValidator.ValidateAsync(objRegiserModel);
                    if (result.IsValid)
                    {
                        if (await objStudentService.RegisterNewStudent(objRegiserModel))
                        {
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
                        foreach (var failure in result.Errors)
                        {
                            m_strStringBuilder.Append("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                        }
                        ViewBag.ErrorMessage = m_strStringBuilder.ToString();
                    }
                }
                return View("Join");
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "Register", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return View("Error");
            }
        }
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [HttpPost]
        public ActionResult ValidateLogin(FormCollection collection,string return_url=null)
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
                var objStudentLoginValidator = new StudentLoginValidator();
                {
                    var ValidationResult =objStudentLoginValidator.Validate(objStudentRegisterModal);
                    if (ValidationResult.IsValid)
                    {
                        if (objStudentService.ValidateLogin(objStudentRegisterModal))
                        {
                            ViewName = "Home";
                            ViewBag.Token = JwtManager.GenerateToken(objStudentRegisterModal.m_strUserId);
                            ViewBag.InstructorUserName = objStudentRegisterModal.m_strUserId;
                            ViewBag.IsLoggedIn = true;
                            Session["student_email"] = objStudentRegisterModal.m_strUserId;
                            Session["user_id"] = objStudentRegisterModal.m_llStudentId;
                            if(return_url!=null)
                            {
                                return Redirect(return_url);
                            }
                            else
                            {
                                return RedirectToAction("Home");

                            }
                            
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
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> ResetPassword(FormCollection collection)
        {
            string strCurrentMethodName = "ResetPassword";
            string ViewName = "";
            try
            {
                StudentRegisterModal objStudentRegisterModal = new StudentRegisterModal();
                objStudentRegisterModal.m_strUserId = collection["userEmail"];
                string token = await objStudentService.InsertPasswordRecovery(objStudentRegisterModal.m_strUserId);
                if (token != null&& token != string.Empty)
                {
                    string sid = objStudentRegisterModal.m_strUserId;
                    return RedirectToAction("PasswordAuthRequest",new { sid, token });
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
                if (await objStudentService.ValidatePasswordRecodevrtOtp(objStudentUpdatePasswordRequestModal))
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
                if (await objStudentService.ChangePasswordAfterAuth(objStudentUpdatePasswordRequestModal))
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
        [HttpGet]
        public PartialViewResult ChangePassword(string sid,string token)
        {
            ViewBag.StudentId = sid;
            ViewBag.Token = token;
            return PartialView();
        }
        [HttpGet]
        public PartialViewResult ForgotPassword()
        {
            return PartialView();
        }
        
        [AllowAnonymous]
        [HttpGet]
        public ActionResult PasswordAuthRequest(string sid,string token)
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
        public ActionResult PasswordUpdatedSuccessfully(string UserId)
        {
            ViewBag.UserMail = UserId;
            ViewBag.IsRegistered = true;
            return View();
        }
        [HttpGet]
        public ActionResult RegistrationSuccessful(string UserId)
        {
            ViewBag.UserMail = UserId;
            ViewBag.IsRegistered = true;
            return View();
        }
        public async Task<PartialViewResult> Home()
        {
            try
            {
                long StudentId = (long)Session["user_id"];

                StudentHomeModal objStudentHomeModal =await objStudentService.GetStudentHomeDetails(StudentId);
                if (objStudentHomeModal != null)
                {
                    ViewBag.Token = JwtManager.GenerateToken(StudentId.ToString());
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
                Session.Remove("student_user_name");
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
        public async Task<PartialViewResult> Account()
        {

            long Id = (long)Session["user_id"];
            StudentRegisterModal objModel = await objStudentService.GetStudentDetails(Id);
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
        public async Task<ActionResult> UpdateDetails(FormCollection collection)
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
                if (await objStudentService.UpdateStudentDetails(objRegiserModel))
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
            try
            {
                return PartialView();
            }
            catch(Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "JoinNewCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            } 
        }
        [HttpGet]
        public async Task<ActionResult> JoinCourse(long id)
        {
            try
            {
                if (!await objStudentService.CheckIsStudentHasJoinedTheCourse(long.Parse(Session["user_id"].ToString()), id))
                {
                    Session["course_id"] = id;
                    ViewBag.id = id;
                    return PartialView();
                }
                else
                {
                    return RedirectToAction("./LearnCourse", new { id });
                }
                
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "JoinCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }

        }
        [HttpGet]
        public PartialViewResult MyCourses()
        {
            try
            {
                return PartialView();
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "MyCourses", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }
            
        }
        [HttpGet]
        public PartialViewResult Instructors()
        {
            try
            {
                return PartialView();
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "Instructors", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }  
        }
        [HttpGet]
        public PartialViewResult JoinInstructor()
        {
            try
            {
                return PartialView();
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "JoinInstructor", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }
        }
        [HttpGet]
        public PartialViewResult AssignmentSubmissions()
        {
            try
            {
                return PartialView();
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "AssignmentSubmissions", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }
           
        }
        [HttpGet]
        public PartialViewResult NewAssignment()
        {
            try
            {
                return PartialView();
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "AssignmentSubmissions", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }
        }
        [HttpGet]
        public async Task<ActionResult> GiveAssignment(long id)
        {

            if (!await objStudentService.CheckIsStudentHasSubmittedTheAssignment((long)Session["user_id"], id))
            {
                ViewBag.id = id;
                return PartialView();
            }
            else
            {
                return RedirectToAction("AssignmentResponse",new { id });
            }
        }
        [HttpGet]
        public async Task<ActionResult> AssignmentResponse(long id)
        {
            try
            {
                if(await objStudentService.CheckIsAssignmentSubmissionIdExsitsForStudent((long)Session["user_id"], id))
                {
                    ViewBag.id = id;
                    return PartialView();
                }
                else
                {
                    return RedirectToAction("Home");
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "AssignmentResponse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }
        }
        [HttpGet]
        public PartialViewResult MyAssignments()
        {
            try
            {
                return PartialView();
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "MyAssignments", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }
        }
        [HttpGet]
        public PartialViewResult NewTest()
        {
            try
            {
                return PartialView();
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "NewTest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }
        }
        [HttpGet]
        public async Task<ActionResult> StartTest(long id)
        {
            try
            {
                if (!await objStudentService.CheckIsStudentHasSubmittedTheTest((long)Session["user_id"], id))
                {
                    ViewBag.id = id;
                    return PartialView();
                }
                else
                {
                    return RedirectToAction("TestResponse", new { id });
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "StartTest", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }
        }
        [HttpGet]
        public PartialViewResult TestSubmissions()
        {
            try
            {
                return PartialView();
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "TestSubmissions", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }
           
        }
        public async Task<ActionResult> TestResponse(long id)
        {
            try
            {
                if (await objStudentService.CheckIsTestSubmissionIdExsitsForStudent((long)Session["user_id"], id))
                {
                    ViewBag.id = id;
                    return PartialView();
                }
                else
                {
                    return RedirectToAction("Home");
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "TestResponse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }
        }
        [HttpGet]
        public async Task<ActionResult> LearnCourse(long id)
        {
            try
            {
                if(await objStudentService.CheckIsStudentHasJoinedTheCourse(long.Parse(Session["user_id"].ToString()),id))
                {
                    ViewBag.id = id;
                    return PartialView();
                }
                else
                {
                    return RedirectToAction("./JoinCourse",new { id});
                }
                
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "LearnCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }
          
        }
        [HttpGet]
        public async Task<ActionResult> TeacherProfile(int id)
        {
            try
            {
                InstructorProfileDetailsModal objInstructorProfileDetailsModal = await objStudentService.GetInstructorProfileDetails(id);
                if(objInstructorProfileDetailsModal!=null)
                {
                    objInstructorProfileDetailsModal.m_lsCourses = await objStudentService.GetAllCourseDetailsForInstructor(id);
                    return PartialView(objInstructorProfileDetailsModal);
                }
                else
                {
                    return RedirectToAction("Home");
                }
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "LearnCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }
        }
        [HttpGet]
        public  ActionResult ViewAssignment(int id)
        {
            try
            {
                return View();
            }
            catch (Exception Ex)
            {
                m_strLogMessage.Append("\n ----------------------------Exception Stack Trace--------------------------------------");
                m_strLogMessage = m_strLogMessage.AppendFormat("[Method] : {0}  {1} ", "LearnCourse", Ex.ToString());
                m_strLogMessage.Append("Exception occured in method :" + Ex.TargetSite);
                MainLogger.Error(m_strLogMessage);
                return PartialView("Error");
            }
        }
    }
}