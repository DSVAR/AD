using Microsoft.AspNetCore.Identity;

namespace AD.Data.Models
{
    public class User:IdentityUser
    {
        string Nickname { get; set; }
    }
}