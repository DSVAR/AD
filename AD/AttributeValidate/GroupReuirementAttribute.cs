using AD.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace AD.AttributeValidate
{
    public class GroupReuirementAttribute : ActionFilterAttribute
    { 
        private UserService _userService { get; }
        private string[] _groups=null;
        private string _group;
           
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

            if (_group != null)
            {
                if (user.Departaments.ToUpper().Contains(_group.ToUpper()))
                {
                    return;
                }
            }
            else
            {
                foreach (var d in _groups)
                {
                    if (user.Departaments.ToUpper().Contains(d.ToUpper()))
                    {
                        return;
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
