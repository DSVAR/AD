using AD.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AD.AttributeValidate
{
    public class RequirementFilterAlttribute:ActionFilterAttribute
    {
        readonly UserService _userService;
        private string _role;
        public RequirementFilterAlttribute(UserService userService, string role)
        {
            _userService = userService;
            _role = role;

        }



        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var user = Environment.UserName;
            var view = _userService.FindUser(user).Result;

            if (_userService.IsInRole(view, _role).Result)
            {
               //context.Result = new RedirectResult("www.ya.ru");
                return;

            }
            else
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary (
                    new
                    {
                        controller="Error",
                        action="NotAcces"
                    }
                )) ;
                return;
            }
            
        }
    }
}
