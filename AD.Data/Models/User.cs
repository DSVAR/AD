using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AD.Data.Models
{
    public class User : IdentityUser
    {
        public string Nickname { get; set; }
        public bool IsAdmin { get; set; }
        public string FullName { get; set; }

    }
}