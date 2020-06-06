using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONLibrary
{
    public class APIResponse
    {
        private int code = 200;
        public APIResponse(int code)
        {
            this.code = code;
        }

        public string GetDetailedResponse()
        {
            switch (this.code)
            {
                case 401:
                    return "API key invalid or missing";
                case 404:
                    return "The specified resource was not found (Name is case sensitive)";
                case 415:
                    return "Content type incorrect or not specified";
                case 429:
                    return "Too many requests";
                default:
                    return string.Empty;
            }
        }
    }
}
