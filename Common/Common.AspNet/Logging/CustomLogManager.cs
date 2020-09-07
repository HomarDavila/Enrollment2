using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.AspNet.Logging;
using Common.Extensions;
using Common.Logging;
using log4net;
using log4net.Core;
using Newtonsoft.Json;

namespace Common.Platform.AspNet.Logging
{
    public class CustomLog4NetManager : ICustomLog
    {
        private readonly log4net.ILog logger;
        private readonly ITransaction Transaction;
       // public string Header { get; set; }

        public CustomLog4NetManager(ILog logger, ITransaction _Transaction) 
        {
            this.logger = logger;
            Transaction = _Transaction;
            Console.WriteLine("Server Logger initializing...");
            if (logger != null)
            {
                Console.WriteLine("Server Logger initialized");
                Console.WriteLine(string.Format("Debug: {0}, Error: {1}, Info: {2}, Warning {3}", logger.IsDebugEnabled, logger.IsErrorEnabled, logger.IsInfoEnabled, logger.IsWarnEnabled));
            }
            else
            {
                Console.WriteLine("Failed initializing Server Logger");
            }
        }


        public bool IsDebugEnabled => logger.IsDebugEnabled;

        public bool IsInfoEnabled => logger.IsInfoEnabled;

        public bool IsWarnEnabled => logger.IsWarnEnabled;

        public bool IsErrorEnabled => logger.IsErrorEnabled;

        public bool IsFatalEnabled => logger.IsFatalEnabled;

        public ILogger Logger => logger.Logger;

        public void CustomDebug(object message, [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            if (IsDebugEnabled)
            {
                logger.Debug(CustomMessage(message.ToString(), fileName, lineNumber, caller));
            }
            
        }

        public void CustomDebug(object message, Exception exception, [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            if (IsDebugEnabled)
            {
                logger.Debug(CustomErrorMessage(message, exception, fileName, lineNumber, caller));
            }             
        }

        public void DebugFormat(string format, params object[] args)
        {
            logger.DebugFormat(format, args);
        }

        public void DebugFormat(string format, object arg0)
        {
            logger.DebugFormat(format, arg0);
        }

        public void DebugFormat(string format, object arg0, object arg1)
        {
            logger.DebugFormat(format, arg0, arg1);
        }

        public void DebugFormat(string format, object arg0, object arg1, object arg2)
        {
            logger.DebugFormat(format, arg0, arg1, arg2);
        }

        public void DebugFormat(IFormatProvider provider, string format, params object[] args)
        {
            logger.DebugFormat(provider, format, args);
        }

        public void CustomError(object message, [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {            
            logger.Error(CustomErrorMessage(message, null, fileName, lineNumber,caller));
        }

        public void CustomError(object message, Exception exception, [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            logger.Error(CustomErrorMessage(message, exception, fileName, lineNumber, caller));
        }

        public void ErrorFormat(string format, params object[] args)
        {
            logger.ErrorFormat(format, args);
        }

        public void ErrorFormat(string format, object arg0)
        {
            logger.ErrorFormat(format, arg0);
        }

        public void ErrorFormat(string format, object arg0, object arg1)
        {
            logger.ErrorFormat(format, arg0, arg1);
        }

        public void ErrorFormat(string format, object arg0, object arg1, object arg2)
        {
            logger.ErrorFormat(format, arg0, arg1, arg2);
        }

        public void ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
            logger.ErrorFormat(provider, format, args);
        }

        public void CustomFatal(object message, [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            logger.Fatal(CustomMessage(message.ToString(), fileName, lineNumber, caller));
        }

        public void CustomFatal(object message, Exception exception, [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            logger.Fatal(CustomErrorMessage(message.ToString(), null, fileName, lineNumber, caller));
        }

        public void FatalFormat(string format, params object[] args)
        {
            logger.FatalFormat(format, args);
        }

        public void FatalFormat(string format, object arg0)
        {
            logger.FatalFormat(format, arg0);
        }

        public void FatalFormat(string format, object arg0, object arg1)
        {
            logger.FatalFormat(format, arg0, arg1);
        }

        public void FatalFormat(string format, object arg0, object arg1, object arg2)
        {
            logger.FatalFormat(format, arg0, arg1, arg2);
        }

        public void FatalFormat(IFormatProvider provider, string format, params object[] args)
        {
            logger.FatalFormat(provider, format, args);
        }

        public void CustomInfo(object message, [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {            
            logger.Info(CustomMessage(message.ToString(), fileName, lineNumber, caller));
        }

        public void CustomInfo(object message, Exception exception, [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            logger.Info(CustomErrorMessage(message, exception, fileName, lineNumber, caller));
        }

        public void InfoFormat(string format, params object[] args)
        {
            logger.InfoFormat(format, args);
        }

        public void InfoFormat(string format, object arg0)
        {
            logger.InfoFormat(format, arg0);
        }

        public void InfoFormat(string format, object arg0, object arg1)
        {
            logger.InfoFormat(format, arg0, arg1);
        }

        public void InfoFormat(string format, object arg0, object arg1, object arg2)
        {
            logger.InfoFormat(format, arg0, arg1, arg2);
        }

        public void InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
            logger.InfoFormat(provider, format, args);
        }

        public void CustomWarn(object message, [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            logger.Warn(CustomMessage(message.ToString(), fileName, lineNumber, caller));
        }

        public void CustomWarn(object message, Exception exception, [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            logger.Warn(CustomErrorMessage(message.ToString(), exception, fileName, lineNumber, caller));
        }

        public void WarnFormat(string format, params object[] args)
        {
            logger.WarnFormat(format, args);
        }

        public void WarnFormat(string format, object arg0)
        {
            logger.WarnFormat(format, arg0);
        }

        public void WarnFormat(string format, object arg0, object arg1)
        {
            logger.WarnFormat(format, arg0, arg1);
        }

        public void WarnFormat(string format, object arg0, object arg1, object arg2)
        {
            logger.WarnFormat(format, arg0, arg1, arg2);
        }

        public void WarnFormat(IFormatProvider provider, string format, params object[] args)
        {
            logger.WarnFormat(provider, format, args);
        }

      
        public void Debug(object message)
        {
            logger.Debug(message);
        }

        public void Debug(object message, Exception exception)
        {
            logger.Debug(message, exception);
        }

        public void Info(object message)
        {
            logger.Info(message);
        }

        public void Info(object message, Exception exception)
        {
            logger.Info(message,exception);
        }

        public void Warn(object message)
        {
            logger.Warn(message);
        }

        public void Warn(object message, Exception exception)
        {
            logger.Warn(message, exception);
        }

        public void Error(object message)
        {
            logger.Error(message);
        }

        public void Error(object message, Exception exception)
        {
            logger.Error(message, exception);
        }

        public void Fatal(object message)
        {
            logger.Fatal(message);
        }

        public void Fatal(object message, Exception exception)
        {
            logger.Fatal(message, exception);
        }

        //Custom Methods
        public void Print_InitMethod([CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            if (IsDebugEnabled)
            {
                logger.Debug(CustomTittleMethod("Inicio", fileName, lineNumber, caller));
            }
            else
            {
                logger.Info(CustomTittleMethod("Inicio", fileName, lineNumber, caller));
            }
            
        }
        public void Print_Request(object request, [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null, bool printDebug = false)
        {
            logger.Info(CustomMessage(string.Format("Request: {0}", JsonConvert.SerializeObject(request)), fileName, lineNumber, caller));
            logger.Info(CustomMessage("Procesando ...", fileName, lineNumber, caller));            
        }
        public void Print_Response(object response, [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null, bool printDebug = false)
        {
            if (printDebug)
            {
               if(IsDebugEnabled) logger.Debug(CustomMessage(string.Format("Response: {0}", JsonConvert.SerializeObject(response)), fileName, lineNumber, caller));
            }
            else
            {
                
                if (string.IsNullOrEmpty(JsonConvert.SerializeObject(response).ToString()))
                {
                    if (JsonConvert.SerializeObject(response).ToString().Length <= 1500)
                    {
                        logger.Info(CustomMessage(string.Format("Response: {0}", JsonConvert.SerializeObject(response)), fileName, lineNumber, caller));
                    }
                    else
                    {
                        logger.Info(CustomMessage(string.Format("Response: {0} {1}", JsonConvert.SerializeObject(response).ToString().Substring(0,1500), "**No se puede mostrar el response completo debido a que excede la longitud maxima, favor de habilitar el modo debug en el log4net para poder visualizarlo"), fileName, lineNumber, caller));
                    }
                }
                else
                {
                    logger.Info(CustomMessage(string.Format("Response: {0}", JsonConvert.SerializeObject(response)), fileName, lineNumber, caller));
                }
            }
        }
        public void Print_EndMethod([CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            if (IsDebugEnabled)
            {
                logger.Debug(CustomTittleMethod("Fin", fileName, lineNumber, caller));
            }
            else
            {
                logger.Info(CustomTittleMethod("Fin", fileName, lineNumber, caller));
            }            
        }

        //Private methods
        private string CustomErrorMessage(object message, Exception ex = null, [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            CultureInfo currentCulture = Thread.CurrentThread.CurrentUICulture;
            var language = currentCulture.TwoLetterISOLanguageName;            
            string lineSearch = (language == "en" ? ":line " : (language == "es" ? ":línea " : "notgetlanguage"));
            string innerExceptionMessage = string.Empty;
            string messageEx = string.Empty;
            if (ex != null)
            {
                var index = ex.StackTrace.LastIndexOf(lineSearch);
                if (index != -1)
                {
                    var lineNumberText = ex.StackTrace.Substring(index + lineSearch.Length);
                    if (int.TryParse(lineNumberText, out lineNumber))
                    {
                        lineNumber = Convert.ToInt32(lineNumberText);
                    }
                }
                messageEx = ex.Message;
                innerExceptionMessage = (ex.InnerException != null ? ex.InnerException.Message : "No Message Found");
            }
            else
            {
                messageEx = message.ToString();
            }
            string vFileName = fileName;
            try
            {
                vFileName = Path.GetFileName(fileName);
            }
            catch
            {

            }
            return string.Format("TransactionId: [{0}] CurrentMethod: [{1}:{2}({3})] ErrorMessage: [{4}] InnerExceptionMessage: [{5}]", Transaction.Id, vFileName, caller,  lineNumber.ToString(), messageEx, innerExceptionMessage);
        }
        private string CustomMessage(string message, [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            string vFileName = fileName;
            try
            {
                vFileName = Path.GetFileName(fileName);
            }
            catch
            {

            }
            return string.Format("TransactionId: [{0}] CurrentMethod: [{1}:{2}({3})]  InfoMessage: [{4}]", Transaction.Id, vFileName, caller, lineNumber.ToString(),  message);
        }
        private string CustomTittleMethod(string tittle, [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            string vFileName = fileName;
            try
            {
                vFileName = Path.GetFileName(fileName);
            }
            catch
            {

            }
            return string.Format("TransactionId: [{0}] CurrentMethod: [{1}:{2}({3})]    =================== {4} del metodo {5} =================== ", Transaction.Id, vFileName, caller, lineNumber, tittle, caller);
        }
    }
}
