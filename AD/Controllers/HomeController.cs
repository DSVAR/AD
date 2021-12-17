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

namespace AD.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserService _UserService { get; }
        private UserMethods _userMethods { get; }
        [BindProperty]
        public List<UserViewModel> UserViewModel { get; set; }




        public HomeController(ILogger<HomeController> logger, UserService userService, UserMethods userMethods)
        {
            _logger = logger;
            _UserService = userService;
            _userMethods = userMethods;
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

        public bool GetGroup()
        {
            return true;
        }
       
        [HttpGet]
        public async Task<IActionResult> ViewUser(string email)
        {
            if (!string.IsNullOrEmpty(email)) { 
            var user = await _UserService.FindUserByEmail(email);
            
            return View(user);
            }
            else
            {
                return View();
            }

        }


        public  IActionResult GetClaims()
        {
            
            return View();
        }


        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}



    }
}