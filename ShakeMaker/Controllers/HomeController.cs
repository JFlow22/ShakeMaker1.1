using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShakeMaker.Models;

namespace ShakeMaker.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Submit()
        {
            //for checking the login to the system...
            //the user is called Yarden
            regularUser regTempUser = new regularUser
            {
                userName = "Yarden",
                userPassword = "1"
            };

            if (Request.Form["userName"] == regTempUser.userName)
            {
                Session["tempUser"] = regTempUser;
                Session["tempUserType"] = regTempUser.getType();
            }
            else
            {
                Session["tempUser"] = null;
                Session["tempUserType"] = null;
            }
            return RedirectToAction("Index", "Home");
        }
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
            return View();
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