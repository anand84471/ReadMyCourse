using StudentDashboard.Models;
using StudentDashboard.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace StudentDashboard.Controllers
{
    public class HomeController : Controller
    {
        HomeService objHomeService = new HomeService();
        public ActionResult Index()
        {
            return View();
        }
        //[HttpGet]
        //[Route("About")]
        //public ActionResult About()
        //{
        //    return View();
        //}

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
       
        public ActionResult ForgotPassword()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> RegisterNow(FormCollection collection)
        {
            try
            {

                StaeModel objRegiserModel = new StaeModel();
                objRegiserModel.strFisrtName = collection["firstName"];
                objRegiserModel.strLastName = collection["lastName"];
                objRegiserModel.strPassword = collection["password"];
                objRegiserModel.strEmail = collection["email"];
                objRegiserModel.strPhoneNo = collection["phoneNo"];
                if(await objHomeService.RegisterNewUser(objRegiserModel))
                {
                    ViewBag.IsRegistered = true;
                }
                else
                {
                    ViewBag.IsRegistered = false;
                }
                
                return View("Register");
            }
            catch(Exception Ex)
            {
                return View("Error");
            }
        }
        [HttpPost]
        public async Task<ActionResult> LoginNow(FormCollection collection)
        {
            string ViewName = "";
            try
            {
                StaeModel objRegiserModel = new StaeModel();
                objRegiserModel.strEmail = collection["userEmail"];
                objRegiserModel.strPassword = collection["userPassword"];
                if (await objHomeService.ValidateLoginDetails(objRegiserModel))
                {
                    ViewName = "Home";
                    ViewBag.IsLoggedIn = true;
                    //Session["userName"] = objRegiserModel.strFisrtName + objRegiserModel.strLastName;
                    Session.Add("userName", objRegiserModel.strFisrtName + objRegiserModel.strLastName);
                }
                else
                {
                    ViewName = "Index";
                    ViewBag.IsLoggedIn = false;
                }
            }
            catch (Exception Ex)
            {
                ViewName = "Error";
            }
            return View(ViewName);
        }
    }
}