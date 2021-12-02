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

namespace AD.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _UserService;
        private readonly IMapper _mapper;
        private UserViewModel userView=new UserViewModel();
        public HomeController(ILogger<HomeController> logger, IUserService userService, IMapper mapper)
        {
            _logger = logger;
            _UserService = userService;
            _mapper = mapper;;
        }
        public IActionResult Register()
        {
           

            return null;
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
            var user = await _UserService.FindUser(Environment.UserName);

            if (user == null) {

                userView.UserName = Environment.UserName;
                var result = await _UserService.CreateUser(userView);
                if (result.Succeeded)
                {
                   
                    if (await _UserService.HaveNotRole("user"))
                            await _UserService.AddToRole(user, "user");

                    return Redirect("/Home/Role");
                }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                }

                return (IActionResult)ModelState;
            }
            }


            return View(user);
            
        }
        [HttpPost]
       
        public IActionResult Creation(UserViewModel userView)
        {
            _UserService.AddToRole(userView, "Admin");


            return null;
        }
        [HttpPost]
        public IActionResult Remove(UserViewModel userView)
        {
            
            return null;
        }
        public async Task<IActionResult> Create(RoleViewModel role)
        {
            
            var result = await _UserService.AddRole(role.NameRole);
            return Redirect("/Home/Role");
        }


        public async Task<IActionResult> CreateUser(UserViewModel user)
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