using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShakeMaker.Models;
using System.Diagnostics;
using ShakeMaker.Dal;

namespace ShakeMaker.ModelBinders
{
    // Binder class for login form
    public class SuperUserBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            HttpContextBase objContext = controllerContext.HttpContext;
            string userName = objContext.Request.Form["userName"];
            string password = objContext.Request.Form["userPassword"];
            string userType = objContext.Request.Form["userType"];

            SuperUser user;

            if (userType == "regularUser")
            {
                UserDal dal = new UserDal();
                user = dal.findUser(userName);
            }
            else
            {
                AdminDal dal = new AdminDal();
                user = dal.findUser(userName);
            }

            return user;
        }
    }
}