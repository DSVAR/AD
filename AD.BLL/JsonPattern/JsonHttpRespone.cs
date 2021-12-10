using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AD.BLL.JsonPattern
{
    public class JsonHttpRespone
    {

        public async Task<string> HttpResponse(int code, string message = null, dynamic errors = null)
        {
            return await  Task.Run( () =>
            {
                return JsonConvert.SerializeObject(new
                {
                    CodeStatus = code,
                    Message = message,
                    Errors = errors
                });
            });
        }
    }
}
