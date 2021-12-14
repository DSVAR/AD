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


        public async Task<string> AddUserBD()
        {
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

        public UserViewModel GetInfoBoutUser()
        {

            var userName = _userService.GetUserName();
            
            using (var context = new PrincipalContext(ContextType.Domain))
            {
                List<object> mylist = new List<object>();

                var users = Principal.FindByIdentity(context, userName);
                var directoryEntry = users.GetUnderlyingObject() as DirectoryEntry;
                
                foreach(var i in directoryEntry.Properties) {
                    mylist.Add(i);
                   
                }
                //title, company

                user.PhoneNumber = ((UserPrincipal)users).VoiceTelephoneNumber;
                user.Email = ((UserPrincipal)users).EmailAddress;
                user.UserName = ((UserPrincipal)users).EmailAddress;
                user.FullName=  ((UserPrincipal)users).Name;
                user.Company = directoryEntry.Properties["company"].Value.ToString();
                user.Title= directoryEntry.Properties["title"].Value.ToString();
                user.Nickname = directoryEntry.Properties["mailNickname"].Value.ToString();
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
