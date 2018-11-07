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
         string SourceName = "lalalala";
         string LogName = "MAJA";

        public Log()
        {
            if (!EventLog.SourceExists(SourceName))
            {
                Console.WriteLine("NE POSTOJI");
                EventLog.CreateEventSource(SourceName, LogName);
            }
            newLog = new EventLog(LogName, Environment.MachineName, SourceName);

        }

        public void Logging(string rbac)
        {
            newLog.WriteEntry("Korisnik " + rbac + " je uspesno izvrsio metodu Change() u " + DateTime.Now ,EventLogEntryType.Information);
        }


        public void LoggingFail(string rbac)
        {
            newLog.WriteEntry("Korisnik " + rbac + " je nije uspesno izvrsio metodu Change() u " + DateTime.Now,EventLogEntryType.Error);
        }




    }
}
