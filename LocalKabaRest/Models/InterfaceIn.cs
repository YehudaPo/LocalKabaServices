using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocalKabaRest.Models
{
    public class InterfaceIn
    {
        public int InterfaceInID;
        public int InterfaceID;
        public DateTime RequestDate;
        public string RequestData;
        public string ErrorMsg;
        public int StatusId;
    }
}