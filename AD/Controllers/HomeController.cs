using AD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AD.BLL.ModelsDTO;
using AD.BLL.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.DirectoryServices.AccountManagement;
using AD.AttributeValidate;
using AD.Codes;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace AD.Controllers
{  

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _UserService;
        private readonly IMapper _mapper;
        private UserViewModel userView=new UserViewModel();
        
        AccountManager _AM = new AccountManager();
        public HomeController(ILogger<HomeController> logger, IUserService userService, IMapper mapper)
        {
            _logger = logger;
            _UserService = userService;
            _mapper = mapper;
            //_AM = AM;
        }
        public IActionResult Register()
        {           
            return null;
        }

        public IActionResult Index()
        {
            var u = HttpContext.User.Identity.Name;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

      [RoleValidateAttribute("Admin")]
        public async Task<IActionResult> Role()
        {
           
           // var ps = _AM.GetInfoBoutUser(Environment.UserName);

            var user = await _UserService.FindUser(Environment.UserName);
            var sr = _mapper.Map<IdentityUser>(user);
          
            if (!await  _UserService.HaveRole("User"))
            {
                await _UserService.AddRole("User");
            }
            
            if (user == null) {

                userView.UserName = Environment.UserName;
                var result = await _UserService.CreateUser(userView);

                if (!await _UserService.IsInRole(user, "User"))
                    await _UserService.AddToRole(user, "User");
            }
            var role = await _UserService.GetAllRoles(user);
            ViewBag.Roles = role;



            return View(user);
            
        }
        [HttpPost]
       
        public async Task<IActionResult> Creation(UserViewModel userView)
        {
            var us =await _UserService.FindUser(userView.UserName);

            if (!us.IsAdmin) { 
                await  _UserService.AddToRole(us, "Admin");
                us.IsAdmin = true;
                await _UserService.UpdateUser(us);
            }

            return Redirect("/Home/Role");
        }
        [HttpPost]
        public async Task<IActionResult> Remove(UserViewModel userView)
        {
            var us = await _UserService.FindUser(userView.UserName);

            if (us.IsAdmin)
            {
                await _UserService.RemoveFromUser(us, "Admin");
                us.IsAdmin = false;
                await _UserService.UpdateUser(us);
            }

            return Json(new { userView });
        }

        public async Task<IActionResult> Bip(UserViewModel model)
        {
         


            return null;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }


  
    }
}