using Microsoft.AspNetCore.Identity;

namespace AD.Data.Models
{
    public class User:IdentityUser
    {
       public string Nickname { get; set; }
       public bool IsAdmin { get; set; }
    }
}