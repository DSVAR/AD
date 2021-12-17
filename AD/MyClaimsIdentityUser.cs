using AD.BLL.ModelsDTO;
using AD.BLL.Services;
using AD.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AD
{
    public class MyClaimsIdentityUser : IActionFilter
    {

        private readonly IHttpContextAccessor _httpContextAccsessor;
        private readonly UserService _userService;

        public MyClaimsIdentityUser(IHttpContextAccessor httpContextAccsessor, UserService userService)
        {
            _httpContextAccsessor = httpContextAccsessor;
            _userService = userService;
        }


        public void OnActionExecuted(ActionExecutedContext context)
        {
          
            var userName = _userService.GetUserName();
            var user = _userService.FindUserByUserName(userName).Result;
            var roles = _userService.GetRoles().Result;
            var userIdentity = new ClaimsIdentity();


            foreach (var role in roles)
            {
                if (_userService.IsInRole(user, role.Name ).Result)
                {
                    //userIdentity +role claim
                    userIdentity.AddClaim(new Claim(ClaimTypes.Role, role.Name));
                }
            }
           
               var userClaim = new Claim(ClaimTypes.Name, userName);


            userIdentity.AddClaim(userClaim);
            //var usCLaims = _UserService.GetClaimsUserAsync(user).Result;

            var userPrincipal = new ClaimsPrincipal(userIdentity);
            _httpContextAccsessor.HttpContext.User = userPrincipal;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
           
        }
    }
}
