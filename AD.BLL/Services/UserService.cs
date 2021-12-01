using AD.BLL.Interfaces;
using AD.BLL.ModelsDTO;
using AD.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AD.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        public UserService(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<IdentityResult> AddRole(string name)
        {
           return await _roleManager.CreateAsync(new IdentityRole(name));
        }

        public async Task<IdentityResult> AddToRole(UserViewModel user, string role)
        {
            return await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<IdentityResult> DeleteRole(string name)
        {
            return await _roleManager.DeleteAsync(new IdentityRole(name));
        }

        public async Task<bool> IsInRole(UserViewModel user, string role)
        {
            return await _userManager.IsInRoleAsync(user, role);
        }
    }
}
