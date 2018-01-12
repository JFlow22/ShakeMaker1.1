﻿using System;
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

    }
}