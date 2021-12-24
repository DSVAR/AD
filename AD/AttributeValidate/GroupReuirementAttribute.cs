using AD.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AD.AttributeValidate
{
    public class GroupReuirementAttribute : ActionFilterAttribute
    { 
        private UserService _userService { get; }
        private string[] _groups=null;
        private string _group;
        private List<string> _groupOfUser=new List<string>();
        
        public GroupReuirementAttribute( UserService userService, string group=null, string[] groups=null)
        {
            _userService = userService;
            _group = group;
            _groups = groups;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userName = _userService.GetUserName();
            var user = _userService.FindUserByUserName(userName).Result;

            if (user.Departaments != null)
            {

                var groupsUser = user.Departaments.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);



                if (user != null && _group != null || user != null && _groups != null)
                {

                    if (_group != null)
                    {
                        foreach (var g in groupsUser)
                        {
                            if (g.Replace(" ", "").ToUpper() == _group.Replace(" ", "").ToUpper()) return ;
                        }

                    }
                    else
                    {
                        foreach (var g in _groups)
                        {
                            foreach (var gU in groupsUser)
                            {
                                if (g.Replace(" ", "").ToUpper() == gU.Replace(" ", "").ToUpper())
                                    return ;
                            }
                        }
                    }
                }
            }

            context.Result = new RedirectToRouteResult(new RouteValueDictionary(
                new
                {
                    controller = "Error",
                    action = "NotAcces"
                }
                ));
        }

    }
}
