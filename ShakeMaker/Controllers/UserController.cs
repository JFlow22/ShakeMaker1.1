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
                ViewData["userNameError"] = "";
                ViewData["emailError"] = "";
                ViewData["missingInputError"] = "";

                AdminDal adDal = new AdminDal();
                Admin admin = new Admin(user.userName, user.password);
                UserDal dal = new UserDal();
                if (dal.locate(user) || adDal.locate(admin))
                {
                    ViewData["userNameError"] = "User name is already exist!";
                    return View("Register", user);
                }

                if (dal.locateEmail(user.email))
                {
                    ViewData["emailError"] = "E-mail is already exist!";
                    return View("Register", user);
                }

                if (!checkRegisterForm(user))
                {
                    ViewData["missingInputError"] = "One of the inputs is missing!";
                    return View("Register", user);
                }

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

                UserDal dalAdding = new UserDal();
                dalAdding.addUser(user);

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
            Session["tempUser"] = null;
            ViewData["inputError"] = "";
            if (Request.Form["userName"] != "" && Request.Form["userPassword"] != "")
            {
                if (user == null)
                {
                    ViewData["inputError"] = "User does not exist.";
                    return View("Login");
                }

                if ((string)Request.Form["userPassword"] != user.password)
                {
                    ViewData["inputError"] = "Password is incorrect.";
                    return View("Login");
                }

                Session["tempUser"] = user;

                if (user.getType() == "RegularUser")
                {
                    Session["tempUserType"] = "regularUser";
                }

                else if (user.getType() == "Admin")
                {
                    Session["tempUserType"] = "adminUser";
                }

                return RedirectToAction("Index", "Home");
            }
            else
                return View("Login");
        }

        private bool checkRegisterForm(RegularUser user)
        {
            if (user.password == "") return false;
            if (user.email == null) return false;
            return true;
        }

        private bool checkLoginForm(SuperUser user)
        {
            if (user == null) return false;
            if (user.userName=="") return false;
            if (user.password == "") return false;            
            return true;
        }

        public ActionResult adminProfile()
        {
            return View();
        }

        public ActionResult createCocktailForm()
        {
            return View("createCocktail", new Cocktails());
        }

        [HttpPost]
        public ActionResult createCocktail(Cocktails coc)
        {
            ViewData["missingPrepInput"] = "";
            ViewData["missingIngInput"] = "";
            ViewData["missingNameInput"] = "";
            if (Request.Form["name"] != "" && Request.Form["preperation"] != "" && Request.Form["name0"] != null)
            {
                string cocktailName = Request.Form["name"];
                string category = Request.Form["category"];
                string preperation = Request.Form["preperation"];
                string video = Request.Form["video"];

                CocktailDal dal1 = new CocktailDal();
                if (dal1.locate(cocktailName))
                {
                    ViewData["cocktailExistError"] = "Cocktail is already exist";
                    return View("createCocktail", coc);
                }

                List<Ingredient> ingredients = new List<Ingredient>();
                for (int i = 0; ; i++)
                {
                    if (Request.Form["name" + i.ToString()] == null)
                        break;
                    string name = Request.Form["name" + i.ToString()];
                    string amount = Request.Form["amount" + i.ToString()];
                    ingredients.Add(new Ingredient(name, amount));
                }
                int cid = generateUniqueId();
                coc = new Cocktails(cid, ingredients, convertToCat(category), preperation, video, cocktailName);
                if (!checkCocktailForm(coc))
                {
                    return View("createCocktail", coc);
                }
                CocktailDal dal = new CocktailDal();
                dal.addCocktail(coc);
                foreach (Ingredient ing in ingredients)
                    dal.addIngredient(ing, cid);
                ViewData["addedMessage"] = "Cocktail was inserted successfully! You can see it in homepage.";
                return View("createCocktail", new Cocktails());
            }
            if (Request.Form["preperation"] == "")
            {
                ViewData["missingPrepInput"] = "Preperation was not inserted.";
            }
            if (Request.Form["name0"] == null)
            {
                ViewData["missingIngInput"] = "Ingredients was not inserted.";
            }
            if (Request.Form["name"] == "")
            {
                ViewData["missingNameInput"] = "Name was not inserted.";
            }
            return View("createCocktail", coc);

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

        public ActionResult addCocktailToFavourites(string userName, int cid)
        {
            UserDal dal = new UserDal();
            dal.addFavCocktail(userName, cid);
            RegularUser user = Session["tempUser"] as RegularUser;
            CocktailDal coc = new CocktailDal();
            Cocktails cocktail = coc.findCocktail(cid);
            user.addFavCocktail(cocktail);
            return View("~/Views/Home/CocktailInfo.cshtml",cocktail);
        }
    }
}