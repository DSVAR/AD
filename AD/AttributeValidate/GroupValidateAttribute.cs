using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AD.AttributeValidate
{
    public class GroupValidateAttribute: TypeFilterAttribute
    {
        public GroupValidateAttribute(string[] groups=null) : base(typeof(GroupReuirementAttribute))
        {
            Arguments = new object[] {groups};
        }

        public GroupValidateAttribute(string group) : base(typeof(GroupReuirementAttribute))
        {
            Arguments = new object[] { new string(group) };
        }
    }
}
