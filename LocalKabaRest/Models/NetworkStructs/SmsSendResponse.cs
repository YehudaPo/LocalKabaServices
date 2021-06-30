using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocalKabaRest.Models.NetworkStructs
{
    public class SmsSendResponse
    {     
        public int Status;
        public string ErrorMsg;
 
        public SmsSendResponse()
        {
            Status = General.Constants.REQUEST_STATUS_OK;
            ErrorMsg = "";
        }
    }
}