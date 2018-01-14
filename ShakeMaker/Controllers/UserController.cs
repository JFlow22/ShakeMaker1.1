using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShakeMaker.Dal;
using ShakeMaker.Models;
using ShakeMaker.ModelBinders;
using System.Web.UI.HtmlControls;
using System.Diagnostics;
using System.Net.Mail;

namespace ShakeMaker.Controllers
{
    public class UserController : Controller
    {

        public ActionResult Register(RegularUser user)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Message = "";
                if (!checkRegisterForm(user))
                    return View("Register", user);

                if (user.password != Request.Form["passwordRetype"])
                {
                    ViewBag.Message = "The passwords don't match";
                    user.password = null;
                    return View("Register", user);
                }

                MailAddress mail = new MailAddress(user.email);
                if (mail == null)
                {
                    user.password = null;
                    return View("Register", user);
                }

                Session["tempUser"] = user;
                Session["tempUserType"] = "regularUser";

                UserDal dal = new UserDal();
                dal.addUser(user);

                return RedirectToAction("index", "Home");
            }
            else
            {
                user.password = null;
                return View("Register", user);
            }
        }

        public ActionResult Login([ModelBinder(typeof(SuperUserBinder))] SuperUser user)
        {

            if (!checkLoginForm(user))
            {
                return View("Login");
            }

            Session["tempUser"] = user;

            if (user.getType() == "RegularUser")
                Session["tempUserType"] = "regularUser";

            else if (user.getType() == "Admin")
                Session["tempUserType"] = "adminUser";

            return RedirectToAction("Index", "Home");
        }

        private bool checkRegisterForm(RegularUser user)
        {
            if (user.userName == "") return false;
            if (user.password == "") return false;
            if (user.email == "") return false;
            UserDal dal = new UserDal();
            if (dal.locate(user)) return false;
            if (dal.locateEmail(user.email)) return false;
            AdminDal adDal = new AdminDal();
            Admin admin = new Admin(user.userName, user.password);
            if (adDal.locate(admin)) return false;
            return true;
        }

        private bool checkLoginForm(SuperUser user)
        {
            if (user.userName=="") return false;
            if (user.password == "") return false;
            if (user.getType()=="RegularUser")
            {
                UserDal dal = new UserDal();
                if (!dal.locate((RegularUser)user))
                    return false;
                else if (dal.findUser(user.userName).password != user.password)
                    return false;
            }
            if (user.getType()=="Admin")
            {
                AdminDal dal = new AdminDal();
                if (!dal.locate((Admin)user))
                    return false;
                else if (dal.findUser(user.userName).password != user.password)
                    return false;
            }
            
            return true;
        }

        public ActionResult adminProfile()
        {
            return View();
        }

        public ActionResult createCocktailForm()
        {
            return View("createCocktail");
        }

        [HttpPost]
        public ActionResult createCocktail()
        {
            string cocktailName = Request.Form["cocktailName"];
            string category = Request.Form["category"];
            string preperation = Request.Form["preperation"];
            string video = Request.Form["video"];
            List<Ingredient> ingredients = new List<Ingredient>();
            for (int i=0; ; i++)
            {
                if (Request.Form["name" + i.ToString()] == null)
                    break;
                string name = Request.Form["name" + i.ToString()];
                string amount = Request.Form["amount" + i.ToString()];
                ingredients.Add(new Ingredient(name, amount));
            }
            int cid = generateUniqueId();
            Cocktails coc = new Cocktails(cid, ingredients, convertToCat(category), preperation, video, cocktailName);
            if (!checkCocktailForm(coc))
            {
                return View("~/Views/Home/index.cshtml");
            }
            CocktailDal dal = new CocktailDal();
            dal.addCocktail(coc);
            foreach (Ingredient ing in ingredients)
                dal.addIngredient(ing, cid);
            return View();
        }

        public Category convertToCat(string category)
        {
            if (category == "classic") return Category.classic;
            if (category == "holiday") return Category.holiday;
            if (category == "spring") return Category.spring;
            if (category == "frozenNblended") return Category.frozenNblended;
            if (category == "hotAlcoholic") return Category.hotAlcoholic;
            if (category == "tikiNtropical") return Category.tikiNtropical;
            return Category.classic;
        }

        public bool checkCocktailForm(Cocktails cocktail)
        {
            CocktailDal dal = new CocktailDal();
            if (cocktail.name == "") return false;
            if (cocktail.preperation == "") return false;
            if (dal.locate(cocktail.name)) return false;
            IngredientDal ingdal = new IngredientDal();
            foreach (Ingredient ing in cocktail.ing)
            {
                if (ing.name=="" || ing.amount=="" || ingdal.locate(ing.name, cocktail.cid))
                    return false;
            }
            return true;
        }

        public int generateUniqueId()
        {
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);
            DateTime dtNow = DateTime.Now;
            TimeSpan result = dtNow.Subtract(dt);
            int seconds = Convert.ToInt32(result.TotalSeconds);
            return seconds;
        }

        public ActionResult getUserList()
        {
            UserDal dal = new UserDal();
            return Json(dal.users, JsonRequestBehavior.AllowGet);
        }

        public void addCocktailToFavourites(int cid, string userName)
        {
            UserDal dal = new UserDal();
            dal.addFavCocktail(userName, cid);
        }
    }
}