using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace ProAsp.Core.Services
{
    public interface ILoggerService
    {
        void LogInfo(string info);
    }

    public class LoggerService : ILoggerService
    {
        private static readonly Logger Logger;
        private static readonly Type WrapperType;

        static LoggerService()
        {
            Logger = LogManager.GetCurrentClassLogger();
            WrapperType = typeof(LoggerService);
        }

        public void LogInfo(string info)
        {
            LogEventInfo logEvent = LogEventInfo.Create(LogLevel.Info, Logger.Name, info);

            Logger.Log(typeof(LoggerService), logEvent);
        }

        public void LogError(Exception exception, string errorInfo = "")
        {
            LogEventInfo logEvent = LogEventInfo.Create(LogLevel.Error, Logger.Name, exception, null, errorInfo);

            Logger.Log(WrapperType, logEvent);
        }
    }
}
