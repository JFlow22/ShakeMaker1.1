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

namespace ShakeMaker.Controllers
{
    public class UserController : Controller
    {

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

        bool checkLoginForm(SuperUser user)
        {
            if (user.userName=="") return false;
            if (user.userPassword == "") return false;
            if (user.getType()=="RegularUser")
            {
                GetRegularUsers get = new GetRegularUsers();
                if (!get.locate((RegularUser)user))
                    return false;
                else if (get.findUser(user.userName).userPassword != user.userPassword)
                    return false;
            }
            if (user.getType()=="Admin")
            {
                GetAdmins get = new GetAdmins();
                if (!get.locate((Admin)user))
                    return false;
                else if (get.findAdmin(user.userName).userPassword != user.userPassword)
                    return false;
            }
            
            return true;
        }

    }
}