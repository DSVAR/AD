using AD.BLL.ModelsDTO;
using AD.BLL.Services;
using AD.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AD.Init
{
    public class FirstAdd
    {
        private UserService _userService { get; }

        private static string departaments=" ЦОБ-Все ; sterminal ; Работники ЦОБ ; Internet-Standard ; SCSMRDP ;"+
            "Service asserted identity ; WiFi_Internet_and_KAMAZ ; WiFi_Kamaz_Only ; High Mandatory Level ;";

        private readonly static UserViewModel _admin = new UserViewModel()
        {
            Email = "Admin@gmail.com",
            UserName = "Admin",
            FullName = "Админ Элитный",
            PhoneNumber = "78965851486",
            Company = "SomeCompany",
            Title = "Магистр эллектронных магистралей",
            Departaments = departaments
        };
        private readonly static UserViewModel _testUser = new UserViewModel()
        {
            Email = "User@gmail.com",
            UserName = "User",
            FullName = "Юзверь Боковской",
            PhoneNumber = "78965851486",
            Company = "SomeCompany",
            Title = "Падаван эллектронных магистралей",
            Departaments = departaments
        };


        List<string> Roles = new List<string>() { "User", "Admin" };
        List<UserViewModel> Users = new List<UserViewModel>() { _admin, _testUser };

        public FirstAdd(UserService userService)
        {
            _userService = userService;
        }

        public async Task DeffAddUserRole()
        {
            foreach (var role in Roles)
            {
                if (!await _userService.HaveRole(role))
                {
                    await _userService.AddRole(role);
                }
            }
            foreach (var user in Users)
            {
                var findUser = await _userService.FindUserByEmail(user.Email);
                if (findUser == null)
                {
                    await _userService.CreateUser(user);
                }
            }


            foreach (var user in Users)
            {
                var foundUser = await _userService.FindUserByEmail(user.Email);
                if (foundUser != null)
                    foreach (var role in Roles)
                    {
                        if (!await _userService.IsInRole(foundUser, role))
                        {
                            await _userService.AddToRole(foundUser, role);
                        }
                    }
            }


        }

    }
}
