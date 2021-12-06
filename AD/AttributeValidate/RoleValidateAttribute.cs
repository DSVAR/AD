using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AD.AttributeValidate
{
    public class RoleValidateAttribute :Attribute
    {
        public string UserName { get; set; }
        public string Role { get; set; }
        RoleValidateAttribute()
        {
        }
        RoleValidateAttribute(string role, string userName)
        {

        }
        RoleValidateAttribute(string role)
        {

        }
        
    }
}
