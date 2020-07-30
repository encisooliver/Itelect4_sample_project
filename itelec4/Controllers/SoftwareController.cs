using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace itelec4.Controllers
{
    public class SoftwareController : Controller
    {
        Data.ITElec4dbDataContext db = new Data.ITElec4dbDataContext();

        // GET: Software
        public ActionResult Index()
        {
            return View();
        }

        // GET: Software/Course
        public ActionResult Course()
        {

            return View();

            //if (User.Identity.GetUserId() != null)
            //{
            //}
            //else
            //{
            //    return Redirect("/Software");
            //}
        }

        // GET: Software/Student
        public ActionResult Student()
        {
            return View();
        }

        // GET: Software/Subject
        public ActionResult Subject()
        {
            return View();
        }

    }
}