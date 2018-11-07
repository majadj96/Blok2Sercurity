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
        static string SourceName = "MyApp";
        static string LogName = "LogMyApp";

        static Log()
        {
            if (!EventLog.SourceExists(SourceName))
            {
                EventLog.CreateEventSource(SourceName, LogName);
            }
            newLog = new EventLog(LogName, Environment.MachineName, SourceName);
        }

        public void Logging(string methoodName)
        {
            throw new NotImplementedException();
        }
    }
}
