using Microsoft.AspNetCore.Mvc;

namespace AD.AttributeValidate
{

    public class RoleValidateAttribute :TypeFilterAttribute
    {

        public RoleValidateAttribute(string role) : base(typeof(RequirementFilterAlttribute))
        {
            
             Arguments = new object[] {new string(role)};
        }

    }   
    
    
   
}
