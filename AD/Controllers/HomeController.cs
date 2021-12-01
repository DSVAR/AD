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
using Microsoft.AspNetCore.Identity;

namespace AD.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Role()
        {
            UserViewModel us=new UserViewModel();
            us.UserName=Dns.GetHostEntry(HttpContext.Connection.RemoteIpAddress).HostName;
            us.Email = System.Environment.UserName;
            us.Nickname = User.Identity.Name;




            return View(us);
        }
        [HttpPost]
        public IActionResult Creation(UserViewModel us)
        {
            var sw = us;
            return null;
        }
        [HttpPost]
        public IActionResult Remove(UserViewModel us)
        {
            var sw = us;
            return null;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}