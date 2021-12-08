using AD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AD.BLL.ModelsDTO;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using AD.AttributeValidate;
using AD.Codes;
using AD.BLL.Services;

namespace AD.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //private readonly UserService _UserService;
        private readonly IMapper _mapper;
        UserService _UserService { get; }
        private UserViewModel userView=new UserViewModel();


        public HomeController(ILogger<HomeController> logger, IMapper mapper, UserService userService)
        {
            _logger = logger;
            _UserService = userService;
            _mapper = mapper;
        }
       

        public IActionResult Register()
        {

            return Json(new { } );
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

      [RoleValidateAttribute("User")]
        public async Task<IActionResult> Role()
        {

            var tremor = _UserService.GetUserName();
            var user = await _UserService.FindUser(User.Identity.Name);
          
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

      
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }


  
    }
}