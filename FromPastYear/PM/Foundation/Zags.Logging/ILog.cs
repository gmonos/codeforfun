using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zags.Logging.Events;

namespace Zags.Logging
{
    public interface ILog
    {
        void Log(LogEvent @event);

        void Debug(string message);

        void Info(string message);

        void Info(string message, params object[] properties);

        void Warn(string message, Exception exception);

        void Error(string message);

        void Error(string message, Exception exception);

        void Fatal(string message);

        void Fatal(string message, Exception exception);

        IDisposable PushProperty(string property, object value);

    }
}
