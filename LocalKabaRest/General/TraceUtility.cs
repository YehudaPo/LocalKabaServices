using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace LocalKabaRest.General
{
    public class TraceUtility
    {
        private TraceSource trcSrc;

        public TraceUtility(string traceSourceName)
        {
            if (string.IsNullOrEmpty(traceSourceName))
            {
                throw new ArgumentNullException(traceSourceName);
            }

            trcSrc = new TraceSource(traceSourceName);
        }

        public void TraceError(int id, string message)
        {
            trcSrc.TraceEvent(TraceEventType.Error, id, message);
        }

        public void TraceError(int id, string message, params object[] args)
        {
            trcSrc.TraceEvent(TraceEventType.Error, id, message, args);
        }

        public void TraceError(string message)
        {
            TraceError(0, message);
        }

        public void TraceError(string message, params object[] args)
        {
            TraceError(0, message, args);
        }

    }
}