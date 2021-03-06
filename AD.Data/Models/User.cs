using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AD.Data.Models
{
    public class User : IdentityUser
    {
        public string Nickname { get; set; }
        public bool IsAdmin { get; set; }
        public string FullName { get; set; }
        public string Company { get; set; }
        public string Title { get; set; }
        public string Departaments { get; set; }
    }
}