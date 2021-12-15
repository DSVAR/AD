using AD.BLL.ModelsDTO;
using AD.Data.Interfaces;
using AD.Data.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace AD.BLL.Services
{
    public class UserService 
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _imapper;
        private readonly IRepository<User> _repo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccsessor;
        public UserService(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, IMapper imapper, IRepository<User> repo, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccsessor)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _imapper = imapper;
            _unitOfWork = unitOfWork;
            _repo = repo;
            _httpContextAccsessor= httpContextAccsessor;
        }
        public async Task<IdentityResult> AddRole(string name)
        {
           return await _roleManager.CreateAsync(new IdentityRole(name));
        }

        public async Task<IdentityResult> AddToRole(UserViewModel user, string role)
        {
            var local= _unitOfWork._context.Set<User>().Local.FirstOrDefault(entry => entry.Id.Equals(user.Id));
            if (local != null)
            {
                _repo.Detach(local);
            }

            return await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<IdentityResult> CreateUser(UserViewModel user)
        {
            return await _userManager.CreateAsync(user);  
        }

        public async Task<IdentityResult> RemoveFromUser(UserViewModel user, string role)
        {
            return await _userManager.RemoveFromRoleAsync(user, role);
        }

        public async Task<IdentityResult> DeleteRole(string name)
        {
            return await _roleManager.DeleteAsync(new IdentityRole(name));
        }

        public async Task<UserViewModel> FindUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return _imapper.Map<UserViewModel>(user);
        }

        public async Task<UserViewModel> FindUserByUserName(string email)
        {
            var user = await _userManager.FindByNameAsync(email);
            return _imapper.Map<UserViewModel>(user);
        }



        public async Task<bool> IsInRole(UserViewModel user, string role)
        {
            return await _userManager.IsInRoleAsync(user, role);
        }

        public async Task<bool> HaveRole(string name)
        {            
            return await _roleManager.RoleExistsAsync(name);;
        }

        public async Task<IdentityResult> UpdateUser(UserViewModel user)
        {
            return await _userManager.UpdateAsync(user);
        }


        public async Task<IList<System.Security.Claims.Claim>> GetClaimsUserAsync(UserViewModel user)
        {
            return await _userManager.GetClaimsAsync(user);
        }

        public string GetUserName()
        {
            
            return _httpContextAccsessor.HttpContext.User.Identity.Name.Remove(0, _httpContextAccsessor.HttpContext.User.Identity.Name.IndexOf("\\")).Replace("\\", "");
        }


        public  async Task<List<UserViewModel>> GetUsers()
        {
            var listUser = new List<UserViewModel>();
            var users = await _userManager.Users.ToListAsync();
            foreach(var user in users)
            {
                listUser.Add(_imapper.Map<UserViewModel>(user));
            }

            return listUser;
        }
      
    }
}
