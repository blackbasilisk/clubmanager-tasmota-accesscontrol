using SM.ClubManager.Library.Base.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.ClubManager.Library.Base.Infrastructure
{
    public class EventlogHelper : IDisposable
    {
        private string _applicationName = "";

        public EventlogHelper(string __applicationName)
        {
            _applicationName = __applicationName;
        }

        public EventlogHelper()
            : this("")
        { }

        public void AddEntry(string applicationName, string message, int EventId = 99, EventLogEntryType type = EventLogEntryType.Information, Exception ex = null)
        {
            try
            {
                string messageTemplate = "";
                messageTemplate += Environment.NewLine;
                messageTemplate += "EXCEPTION: ";
                messageTemplate += Environment.NewLine;
                messageTemplate += "{0}";
                messageTemplate += Environment.NewLine;
                messageTemplate += "STACKTRACE:";
                messageTemplate += Environment.NewLine;
                messageTemplate += "{1}";

                if (!EventLog.Exists(Constants.EventLogName))
                {
                    EventLog.CreateEventSource(applicationName, Constants.EventLogName, ".");
                }
                EventLog eventlog = new EventLog(Constants.EventLogName);

                eventlog.Source = applicationName;
                if (ex != null)
                {
                    message = string.Format(messageTemplate, ex.Message, ex.StackTrace);
                    if (ex.InnerException != null)
                    {
                        Exception innerException = new Exception();
                        innerException = GetInnerException(ex);
                        message += Environment.NewLine;
                        message += "------------BASE INNER EXCEPTION DETAILS";
                        message += string.Format(messageTemplate, innerException.Message, ex.StackTrace);
                    }
                        
                }
                eventlog.WriteEntry(message, type, EventId);
            }
            catch (Exception exe)
            {
                //MessageBox.Show(exe.Message);
                throw exe;
            }
        }

        public void AddEntry(string message, int EventId = 99, EventLogEntryType type = EventLogEntryType.Information, Exception ex = null)
        {
            if (string.IsNullOrEmpty(_applicationName.Trim()))
            {
                _applicationName = System.Windows.Forms.Application.ProductName;
            }

            AddEntry(_applicationName, message, EventId, type, ex);
        }

        //recursive function to return the most innerexception 
        private Exception GetInnerException(Exception ex) 
        {
            Exception innerException;

            if (ex.InnerException == null)
                return ex;

            innerException = ex.InnerException;
            while (ex.InnerException != null)
            {
                ex = GetInnerException(innerException);
            }

            return ex;
        }

        public void Dispose() { }
    }
}