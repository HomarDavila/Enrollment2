using Common.Logging;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Common.Logging
{
    public interface ICustomLog
    {
        string Header { get; set; }

        bool IsFatalEnabled { get; }
        bool IsWarnEnabled { get; }
        bool IsInfoEnabled { get; }
        bool IsDebugEnabled { get; }
        bool IsErrorEnabled { get; }

        void InitializeLog(Type t, ITransaction transaction);
        void Debug(object message, [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        void Debug(object message, Exception exception, [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        void Error(object message, [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        void Error(object message, Exception exception, [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        void Fatal(object message, [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        void Fatal(object message, Exception exception, [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        void Info(object message, [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        void Info(object message, Exception exception, [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        void Warn(object message, [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        void Warn(object message, Exception exception, [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);

        void Print_InitMethod([CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);
        void Print_Request(object request, [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null, bool printDebug = false);
        void Print_Response(object response, [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null, bool printDebug = false);
        void Print_EndMethod([CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null);

    }
}
