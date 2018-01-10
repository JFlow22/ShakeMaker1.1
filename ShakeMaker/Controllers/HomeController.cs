using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShakeMaker.Models;
using ShakeMaker.Dal;
using System.Diagnostics;

namespace ShakeMaker.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View("~/Views/User/Login.cshtml");
        }

        public ActionResult regUserProfile()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session["tempUser"] = null;
            Session["tempUserType"] = null;
            return RedirectToAction("Login");
        }
    }
}