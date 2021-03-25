using NLog;
using Proje.ToDo.Business.Interfaces;

namespace Proje.ToDo.Business.CustomLogger
{
    public class NlogLogger : ICustomLogger
    {
        public void LogError(string message)
        {
            var logger = LogManager.GetLogger("loggerFile");
            logger.Log(LogLevel.Error, message);
        }
    }
}
