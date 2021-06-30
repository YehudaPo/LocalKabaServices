using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocalKabaRest.Models.NetworkStructs
{
    public class SmsSendResponse
    {     
        public string Status;
        public string ErrorMsg;
 
        public SmsSendResponse()
        {
            Status = "";
            ErrorMsg = "";
        }
    }
}