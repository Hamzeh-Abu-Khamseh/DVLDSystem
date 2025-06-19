using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class clsErrorsLogging
    {
        public static void LogError(string Message,EventLogEntryType LogType, string SourceName="DVLD_App")
        {
            try
            {

                if (!EventLog.SourceExists(SourceName))
                {
                    EventLog.CreateEventSource(SourceName, "Application");

                }
                EventLog.WriteEntry(SourceName, Message, LogType);

            }
            catch (Exception ex)
            {
                LogError(ex.Message, EventLogEntryType.Error, "DVLD_App_Error");
            }
            
        }
    }
}
