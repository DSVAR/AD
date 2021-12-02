using AD.BLL.ModelsDTO;
using AD.BLL.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AD.BLL.Interfaces
{
   public interface IUserService
    {
        Task<IdentityResult> AddRole(string name);
        Task<IdentityResult> DeleteRole(string name);
        Task<bool> IsInRole(UserViewModel user, string role);
        Task<IdentityResult> AddToRole(UserViewModel user,string role);
        Task<IdentityResult> CreateUser(UserViewModel user);
        Task<bool> HaveNotRole(string name);
        Task<UserViewModel> FindUser(string name);
    }
}
