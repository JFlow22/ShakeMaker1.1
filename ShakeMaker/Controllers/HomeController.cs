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
            CocktailDal coc = new CocktailDal();
            List<Cocktails> cocktails = new List<Cocktails>();
            foreach (ModelBinders.CocktailDataBaseBinder cocktail in coc.cocktails)
            {
                cocktails.Add(cocktail.getCocktail());
            }
            return View(cocktails);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Find and make your favourite cocktails!!";

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View("~/Views/User/Login.cshtml");
        }

        public ActionResult Register()
        {
            return View("~/Views/User/Register.cshtml", new RegularUser());
        }

        public ActionResult regUserProfile()
        {
            return View(Session["tempUser"] as RegularUser);
        }

        public ActionResult Logout()
        {
            Session["tempUser"] = null;
            Session["tempUserType"] = null;
            return RedirectToAction("Login");
        }

        public ActionResult FromUserToCocktailInfo(int cid)
        {
            CocktailDal dal = new CocktailDal();
            Cocktails coc = dal.findCocktail(cid);
            return View("CocktailInfo", coc);
        }

        public ActionResult CocktailInfo()
        {
            string cocktailName = Request.Form["cocktailName"];
            CocktailDal dal = new CocktailDal();
            Cocktails coc = dal.findCocktail(cocktailName);
            return View(coc);
        }
    }
}