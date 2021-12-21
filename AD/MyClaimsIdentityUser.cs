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
        //Заглушка для авторизации пользователя с базы.
        private readonly IHttpContextAccessor _httpContextAccsessor;
        private readonly UserService _userService;

        public MyClaimsIdentityUser(IHttpContextAccessor httpContextAccsessor, UserService userService)
        {
            _httpContextAccsessor = httpContextAccsessor;
            _userService = userService;
        }

        //выполнение после загрузки
        public void OnActionExecuted(ActionExecutedContext context)
        {

            var userName = _userService.GetUserName();
            var user = _userService.FindUserByUserName(userName).Result;
            if (user != null)
            {
                var roles = _userService.GetRoles().Result;
                var userIdentity = new ClaimsIdentity(user.Id);

                
                foreach (var role in roles)
                {
                    if (_userService.IsInRole(user, role.Name).Result)
                    {
                        userIdentity.AddClaim(new Claim(ClaimTypes.Role, role.Name));
                    }
                }

                userIdentity.AddClaim(new Claim(ClaimTypes.Name, userName));
                userIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier,user.Id));

                var userPrincipal = new ClaimsPrincipal(userIdentity);

                _httpContextAccsessor.HttpContext.User = userPrincipal;
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }
    }
}
