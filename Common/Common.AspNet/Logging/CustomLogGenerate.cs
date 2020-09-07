using Common.Logging;
using Common.Platform.AspNet.Logging;
using log4net;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.AspNet.Logging
{
    public static class CustomLogGenerate
    {
        public static CustomLogManager EnsureLogger(CustomLogManager logger, ILog logBase)
        {   
            if(logger == null) logger = new CustomLogManager(logBase, string.Empty.GetTransaction());
            return logger;
        }
    }
}
