using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShakeMaker.Models;
using System.Diagnostics;

namespace ShakeMaker.ModelBinders
{
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
                user = new RegularUser(userName, password);
            }
            else user = new Admin(userName, password);

            return user;
        }
    }
}