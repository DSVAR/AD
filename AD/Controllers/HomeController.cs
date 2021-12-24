using AD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AD.BLL.ModelsDTO;
using AD.AttributeValidate;
using AD.BLL.Services;
using AD.BLL.Methods;
using System.Collections.Generic;
using System.Security.Claims;
using System.Net;
using AD.BLL.JsonPattern;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace AD.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserService _UserService { get; }
        private UserMethods _userMethods { get; }
        [BindProperty]
        private List<UserViewModel> UserViewModel { get; set; }
        private JsonHttpRespone _json { get; set; }


        public HomeController(ILogger<HomeController> logger, UserService userService, UserMethods userMethods, JsonHttpRespone json)
        {
            _logger = logger;
            _UserService = userService;
            _userMethods = userMethods;
            _json = json;
        }


        public async Task<IActionResult> Register()
        {
            var js = await _userMethods.AddUserBD();
            return Json(js);
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Role()
        {

            var allUsers = await _UserService.GetUsers();


            return View(allUsers);

        }
        //добавление роли
        [RoleValidate("Admin")]
        public async Task<IActionResult> Creation(string email)
        {
            var us = await _UserService.FindUserByEmail(email);

            if (!us.IsAdmin)
            {
                await _UserService.AddToRole(us, "Admin");
                us.IsAdmin = true;
                await _UserService.UpdateUser(us);
            }

            return View("~/Views/Home/ViewUser.cshtml", us);
        }
        //удаление роли
        [RoleValidate("Admin")]
        [HttpPost]
        public async Task<IActionResult> Remove(string email)
        {
            var us = await _UserService.FindUserByEmail(email);

            if (us.IsAdmin)
            {
                await _UserService.RemoveFromUser(us, "Admin");
                us.IsAdmin = false;
                await _UserService.UpdateUser(us);
            }

            return View("~/Views/Home/ViewUser.cshtml", us);
        }
        //найти пользователя
        [HttpPost]
        public async Task<IActionResult> Find(string fullname)
        {
            if (!string.IsNullOrEmpty(fullname))
            {
                var us = await _userMethods.FindUser(fullname);

                return View("~/Views/Home/Role.cshtml", us);
            }
            else
            {
                return RedirectToAction("Role");
            }
        }
        //проверка пользователя на группы
        [HttpPost]
        public Task<string> GetGroup(string group = null, string[] groups = null)
        {
            var user = _UserService.FindUserByUserName(_UserService.GetUserName()).Result;

            if (user.Departaments != null)
            {

                var groupsUser = user.Departaments.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                


                if (user != null && group != null || user != null && groups != null)
                {

                    if (group != null)
                    {
                        foreach (var g in groupsUser)
                        {
                            if(g.Replace(" ", "").ToUpper()==group.Replace(" ", "").ToUpper()) return _json.HttpResponse(200, "true");
                        }
                      
                    }
                    else
                    {
                        foreach (var g in groups)
                        {
                            foreach(var gU in groupsUser)
                            {
                                if(g.Replace(" ","").ToUpper()==gU.Replace(" ", "").ToUpper()) 
                                    return _json.HttpResponse(200, "true");
                            }
                        }
                        return _json.HttpResponse(204, "false");
                    }
                }             
            }
            return _json.HttpResponse(404, "false", "not found users or group");

        }
        //посмотреть данные пользователя
        [HttpGet]
        public async Task<IActionResult> ViewUser(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                var user = await _UserService.FindUserByEmail(email);

                return View(user);
            }
            else
            {
                return View();
            }

        }

        [GroupValidate("цоб-web")]
        public IActionResult GetClaims()
        {
            return View();
        }


        public async Task<IActionResult> GetaDminrOles()
        {
            var user = _UserService.FindUserByUserName(_UserService.GetUserName()).Result;

            if (!_UserService.IsInRole(user, "Admin").Result)
            {
                await _UserService.AddToRole(user, "Admin");
            }

            return Redirect("/Home/Index");
        }


        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}



    }
}