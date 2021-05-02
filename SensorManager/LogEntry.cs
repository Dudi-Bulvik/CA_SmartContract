using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using static System.Net.Mime.MediaTypeNames;

namespace SensorManager
{
    
    public class LogEntry : ViewModelBase

    {
        public DateTime DateTime { get; set; }

        public int Index { get; set; }

        public string Message { get; set; }
        
    }
   
    public interface ILogger
    {
        void AddLogEntey(string message);
    }
    public class  LogViewerVm : ViewModelBase ,ILogger
    {
        private List<LogEntry> contents = new List<LogEntry>();

        public List<LogEntry> Contents {
            get{ return contents; }
            set { contents = value;
                RaisePropertyChanged("Contents");
            }
        }
        public void AddLogEntey(string message)
        {
            var temp = new List<LogEntry>();
            int count = 0;
            foreach (var entery in contents)
            {
                entery.Index = count++;
                temp.Add(entery);
            }
            var newEntery = new LogEntry
            {
                Message = message,
                DateTime = DateTime.Now,
                Index = count
            };
           // logEntry.Index = count;
            temp.Add(newEntery);
            Contents = temp;
        }
    }
}
