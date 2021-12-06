using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AD.BLL.Interfaces;
using AD.BLL.ModelsDTO;
using AD.BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RoleViewModel = AD.BLL.Services.RoleViewModel;

namespace AD.AttributeValidate
{
    
    public class RoleValidateAttribute :TypeFilterAttribute
    {

        public RoleValidateAttribute(string role) : base(typeof(ClaimRequirementFilterAttribute))
        {
            
             Arguments = new object[] {new string(role)};
        }

    }
    
    
    
    public class ClaimRequirementFilterAttribute : ActionFilterAttribute
    {
        readonly IUserService _userService;
        private string _role;
        public ClaimRequirementFilterAttribute(IUserService userService,string role)
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
                context.Result = new RedirectResult("www.ya.ru");
                return;
                
            }
            else
            {
                context.Result = new RedirectResult("NAHYI");
                return;
            }
            base.OnActionExecuting(context);
        }
    }
}
