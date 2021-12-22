using AD.BLL.ModelsDTO;
using AD.BLL.Services;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.DirectoryServices.AccountManagement;
using AD.BLL.JsonPattern;
using System.DirectoryServices;

namespace AD.BLL.Methods
{
    public class UserMethods
    {
        private UserService _userService { get; }
        private JsonHttpRespone _jsonPattern;
        private UserViewModel user=new UserViewModel();

        public UserMethods(UserService userService, JsonHttpRespone jsonPattern)
        {
            _userService = userService;
            _jsonPattern = jsonPattern;
        }

        //добавление пользователя возрат Json
        public async Task<string> AddUserBD()
        {
            //добавление пользователя в бд
            try
            {
                var user = GetInfoBoutUser();
                var foundUser = await _userService.FindUserByEmail(user.Email);
                if ( foundUser == null)
                {

                    var result = await _userService.CreateUser(user);
                    if (result.Succeeded) { 
                         await _userService.AddToRole(user, "User");
                    }
                    else
                    {
                        return await _jsonPattern.HttpResponse(403, "Some problems", result.Errors);
                    }
                }

                return await _jsonPattern.HttpResponse(201, "Was add");
            }
            catch (Exception ex)
            {

                return await _jsonPattern.HttpResponse(403, "Was some errors", ex);
            }
        }
        //получение информации с АД
        public UserViewModel GetInfoBoutUser()
        {
            
            var userName = _userService.GetUserName();
            
            using (var context = new PrincipalContext(ContextType.Domain))
            {
                List<object> mylist = new List<object>();
                List<object> de = new List<object>();

                
                var users = UserPrincipal.FindByIdentity(context, userName);
                var directoryEntry = users.GetUnderlyingObject() as DirectoryEntry;
                var directorySearches = new PrincipalSearcher(new UserPrincipal(context));

                var group = users.GetAuthorizationGroups();

          
                foreach (var i in group) {
                    user.Departaments += " "+i+ " ;";                   
                }
                //title, company

                user.PhoneNumber = ((UserPrincipal)users).VoiceTelephoneNumber;
                user.Email = ((UserPrincipal)users).EmailAddress;
                user.UserName = directoryEntry.Properties["mailNickname"].Value.ToString();
                user.FullName=  ((UserPrincipal)users).Name;
                user.Company = directoryEntry.Properties["company"].Value.ToString();
                user.Title= directoryEntry.Properties["title"].Value.ToString();
                return user;
            }

        }

        public async Task<List<UserViewModel>> FindUser(string fullname)
        {
            var users = await _userService.GetUsers();
            var result = users.Where(u=>u.FullName.ToLower().Contains(fullname.ToLower())).ToList();          

            return result;
        }

    }
}
