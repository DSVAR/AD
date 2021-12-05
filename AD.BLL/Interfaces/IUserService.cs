using AD.BLL.ModelsDTO;
using AD.BLL.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AD.Data.Models;

namespace AD.BLL.Interfaces
{
   public interface IUserService
    {
        Task<IdentityResult> AddRole(string name);
        Task<IdentityResult> DeleteRole(string name);
        Task<bool> IsInRole(UserViewModel user, string role);
        Task<IdentityResult> AddToRole(UserViewModel user,string role);
        Task<IdentityResult> CreateUser(UserViewModel user);
        Task<IdentityResult> RemoveFromUser(UserViewModel user, string role);
        Task<bool> HaveRole(string name);
        Task<UserViewModel> FindUser(string name);
        Task<IdentityResult> UpdateUser(UserViewModel user);
        Task<List<User>> GetAllUser();
        Task<List<IdentityRole>> GetAllRoles();
    }
}
