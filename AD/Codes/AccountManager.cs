using AD.BLL.ModelsDTO;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AD.Codes
{
    public class AccountManager
    {
        UserViewModel user = new UserViewModel();
        public UserViewModel GetInfoBoutUser(string userName=null)
        {
            if (userName != null) { 
                using(var context =new PrincipalContext(ContextType.Domain))
                {
                    var justList = new List<string>();
                    var users = Principal.FindByIdentity(context, userName);

                    var sw = users.DisplayName;
                    var phone = ((UserPrincipal)users).VoiceTelephoneNumber;

                    user.PhoneNumber = ((UserPrincipal)users).VoiceTelephoneNumber;
                    user.Email = ((UserPrincipal)users).EmailAddress;
                    user.UserName = ((UserPrincipal)users).Name;
                    user.Id = ((UserPrincipal)users).EmployeeId;
                    return user;
                }
            }

            return null;
        }
    }
}
