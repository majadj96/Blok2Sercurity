using Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysLog
{
    public class Log : ILog
    {

        static EventLog newLog;
        static string SourceName = "SysLog";
        static string LogName = "LogServis";

        static Log()
        {
            if (!EventLog.SourceExists(SourceName))
            {
                EventLog.CreateEventSource(SourceName, LogName);
            }
            newLog = new EventLog(LogName, Environment.MachineName, SourceName);
        }

        public void Logging(string methodName, string userName)
        {
            newLog.WriteEntry("Korisnik " + userName + " successfully accessed to " + methodName);
        }

        public void LoggingFail(string methodName, string userName, string reason)
        {
            newLog.WriteEntry("User " + userName + " failed to access " + methodName + ". Reason: " + reason);
        }
    }
}
