using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocalKabaRest.Models
{
    public class PreventionCase
    {
        public int PreventionCaseId;
        public string CaseName;
        public double AirDistanceFromPoint;
        public string CaseAddress;
        public string CaseType;
        public bool DialerAuthorized;

        public List<CaseContact> contacts;
        public List<FileLink> files;
        public List<OperationInfo> operations;
    }
}