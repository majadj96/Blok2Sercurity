using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SysLog
{
    public class Log : ILog
    {

         EventLog newLog;
         string SourceName = "RBAC";
         string LogName = "SysLoger";

        public Log()
        {
            if (!EventLog.SourceExists(SourceName))
            {
                EventLog.CreateEventSource(SourceName, LogName);
            }
            newLog = new EventLog(LogName, Environment.MachineName, SourceName);

        }

        public void Logging(string rbac)
        {
            newLog.WriteEntry("User " + rbac + " successfully accessed to Change() at " + DateTime.Now ,EventLogEntryType.Information);
        }


        public void LoggingFail(string rbac)
        {
            newLog.WriteEntry("User " + rbac + " failed to access to Change() at " + DateTime.Now,EventLogEntryType.Error);
        }




    }
}
