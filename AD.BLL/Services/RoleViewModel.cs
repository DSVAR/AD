using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AD.BLL.Services
{
    public class RoleViewModel
    {
        [Required(ErrorMessage ="Need some role")]
        string RoleName{ get; set; }

        [Required(ErrorMessage ="Need name")]
        string UserName { get; set; }
    }
}
