using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace devarts.Helpers
{
    public class NLogConfig
    {
        // konfiguracja Logger-a
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public void logError(string msg)
        {
            logger.Error("   " + msg);
        }

        public void logInfo(string msg)
        {
            logger.Info("   " + msg);
        }

        public void logWarn(string msg)
        {
            logger.Warn("   " + msg);
        }
    }
}