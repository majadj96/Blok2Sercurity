﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public static class Audit
    {
        static string SourceName = OperationContext.Current.IncomingMessageHeaders.Action;
        static string LogName = "LogServis";
        static EventLog newLog;

        static Audit()
        {
            if (!EventLog.SourceExists(SourceName))
            {
                EventLog.CreateEventSource(SourceName, LogName);
            }
            newLog = new EventLog(LogName, Environment.MachineName, SourceName);
        }

        public static void LoggingSuccess(string korisnickoIme, string imeMetode)
        {
            newLog.WriteEntry("User " + korisnickoIme + " successfully accessed to " + imeMetode,EventLogEntryType.Information);

        }

        public static void LoggingFail(string korisnickoIme, string imeMetode, string razlog)
        {
            newLog.WriteEntry("User " + korisnickoIme + " failed to access to " + imeMetode + ". Reason: " + razlog,EventLogEntryType.Error);
        }



    }
}
