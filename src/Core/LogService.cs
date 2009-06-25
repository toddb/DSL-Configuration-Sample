using System;
using log4net;
using log4net.Config;

namespace Core
{
    public class LogService
    {
        public static ILog log = LogManager.GetLogger(typeof(LogService)); // see http://www.designforge.com/df/design/log4net.htm for usage tutorial

        /// <summary>
        /// This should be used for debugging. Note Debugging levels are turned on and off in the app.config file
        /// </summary>
        /// <param name="message"></param>
        public static void Debug(object message)
        {
            XmlConfigurator.Configure();
            if (log.IsDebugEnabled) log.Debug(message);
        }

        /// <summary>
        /// This should be used where an operation cannot be performed
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Fatal(object message, Exception exception)
        {
            XmlConfigurator.Configure();
            if (log.IsFatalEnabled) log.Fatal(message, exception);
        }

        /// <summary>
        /// This should be used where an operation cannot be performed but is recoverable
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Warn(object message, Exception exception)
        {
            XmlConfigurator.Configure();
            if (log.IsWarnEnabled) log.Warn(message, exception);
        }

        /// <summary>
        /// This should be used where an operation cannot be performed but is recoverable
        /// </summary>
        /// <param name="message"></param>
        public static void Warn(object message)
        {
            XmlConfigurator.Configure();
            if (log.IsWarnEnabled) log.Warn(message);
        }

        /// <summary>
        /// This should be used sparingly for general administration messages to track errors.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public static void Info(object message, Exception exception)
        {
            XmlConfigurator.Configure();
            if (log.IsInfoEnabled) log.Info(message, exception);
        }

        /// <summary>
        /// This should be used sparingly for general administration messages to track usage.
        /// </summary>
        /// <param name="message"></param>
        public static void Info(object message)
        {
            XmlConfigurator.Configure();
            if (log.IsInfoEnabled) log.Info(message);
        }

    }
}
