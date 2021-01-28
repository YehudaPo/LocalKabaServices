using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocalKabaRest.Models.NetworkStructs
{
    public class GetPrvCaseDetailsResponse
    {
      
        public int InterfaceKey;
        public int ExternalInterfaceKey;
        public int Status;
        public string ErrorMsg;
        public List<PreventionCase> prvCases;

        public GetPrvCaseDetailsResponse()
        {
            Status = General.Constants.REQUEST_STATUS_OK;
            ErrorMsg = "";
        }
    }
}