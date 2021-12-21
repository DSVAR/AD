using AD.BLL.Methods;
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
        UserMethods _userMethods;
        public RequirementFilterAlttribute(UserService userService, string role,UserMethods userMethods)
        {
            _userService = userService;
            _role = role;
            _userMethods = userMethods;

        }



        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var user = _userMethods.GetInfoBoutUser();
            var view = _userService.FindUserByEmail(user.Email).Result;

            if (_userService.IsInRole(view, _role).Result)
            {
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
            }
            
        }
    }
}
