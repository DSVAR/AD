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

namespace AD.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserService _UserService { get; }
        private UserMethods _userMethods { get; }
        


        public HomeController(ILogger<HomeController> logger,  UserService userService, UserMethods userMethods)
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

      [RoleValidate("Admin")]
        public async Task<IActionResult> Role()
        {

            var allUsers = await _UserService.GetUsers();

        
            return View(allUsers);
            
        }
        [RoleValidate("Admin")]

        public async Task<IActionResult> Creation(string email)
        {
            var us =await _UserService.FindUserByEmail(email);

            if (!us.IsAdmin) { 
                await  _UserService.AddToRole(us, "Admin");
                us.IsAdmin = true;
                await _UserService.UpdateUser(us);
            }

            return Redirect("/Home/Role");
        }
        [RoleValidate("Admin")]
        public async Task<IActionResult> Remove(string email)
        {
            var us = await _UserService.FindUserByEmail(email);

            if (us.IsAdmin)
            {
                await _UserService.RemoveFromUser(us, "Admin");
                us.IsAdmin = false;
                await _UserService.UpdateUser(us);
            }

            return Redirect("/Home/Role");
        }

      
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }


  
    }
}