﻿using StudentDashboard.Models.Student;
using StudentDashboard.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StudentDashboard.Controllers
{
    public class JoinLiveClassController : Controller
    {
        DocumentService objDocumentService;
        public JoinLiveClassController()
        {
            objDocumentService = new DocumentService();
        }
        // GET: JoinLiveClass
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Details(string id)
        {
            ClassroomJoinDetailsModal classroomJoinDetailsModal;
            if (Session["user_id"] != null)
            {
                Response.Redirect(MvcApplication._strApplicationBaseUrl + "/Student/JoinNow?Id=" + id );
            }
            classroomJoinDetailsModal = await objDocumentService.GetLiveClassDetailsForStudent(id);
            ViewBag.ReturnUrl = MvcApplication._strApplicationBaseUrl + "/student?return_url=" + MvcApplication._strApplicationBaseUrl + "/Student/PreviewClassroom?ClassroomId=" + id + "&&access_code=";
            ViewBag.Id = classroomJoinDetailsModal.m_llClassroomId;
            return View(classroomJoinDetailsModal);
        }
    }
}