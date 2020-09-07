using Common.Logging;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Common.AspNet.Logging
{
    public class CustomLog4Net : ICustomLog
    {
        private ITransaction Transaction;
        private log4net.ILog logger;
        private static CustomLog4Net instance = null;

        public CustomLog4Net()
        {
            //Esto solo lo hacemos para asegurar el log en caso no se haya utilizado el metodo InitializeLog
            logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            Transaction = string.Empty.GetTransaction();
        }

        public static CustomLog4Net Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CustomLog4Net();
                }
                return instance;
            }
        }

        public string Header { get; set; }

        public bool IsDebugEnabled => logger.IsDebugEnabled;

        public bool IsInfoEnabled => logger.IsInfoEnabled;

        public bool IsWarnEnabled => logger.IsWarnEnabled;

        public bool IsErrorEnabled => logger.IsErrorEnabled;

        public bool IsFatalEnabled => logger.IsFatalEnabled;



        public void InitializeLog(Type t, ITransaction transaction)
        {
            logger = log4net.LogManager.GetLogger(t);
            Transaction = transaction;
        }

        public void Debug(object message, [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            if (IsDebugEnabled)
            {
                logger.Debug(CustomMessage(message.ToString(), fileName, lineNumber, caller));
            }
        }

        public void Debug(object message, Exception exception, [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            if (IsDebugEnabled)
            {
                logger.Debug(CustomErrorMessage(message, exception, fileName, lineNumber, caller));
            }
        }

        public void Error(object message, [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            logger.Error(CustomErrorMessage(message, null, fileName, lineNumber, caller));
        }

        public void Error(object message, Exception exception, [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            logger.Error(CustomErrorMessage(message, exception, fileName, lineNumber, caller));
        }

        public void Fatal(object message, [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            logger.Fatal(CustomMessage(message.ToString(), fileName, lineNumber, caller));
        }

        public void Fatal(object message, Exception exception, [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            logger.Fatal(CustomErrorMessage(message.ToString(), null, fileName, lineNumber, caller));
        }

        public void Info(object message, [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            logger.Info(CustomMessage(message.ToString(), fileName, lineNumber, caller));
        }

        public void Info(object message, Exception exception, [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            logger.Info(CustomErrorMessage(message, exception, fileName, lineNumber, caller));
        }

        public void Warn(object message, [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            logger.Warn(CustomMessage(message.ToString(), fileName, lineNumber, caller));
        }

        public void Warn(object message, Exception exception, [CallerFilePath] string fileName = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null)
        {
            logger.Warn(CustomErrorMessage(message.ToString(), exception, fileName, lineNumber, caller));
        }

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
                if (IsDebugEnabled) logger.Debug(CustomMessage(string.Format("Response: {0}", JsonConvert.SerializeObject(response)), fileName, lineNumber, caller));
            }
            else
            {

                if (!string.IsNullOrEmpty(JsonConvert.SerializeObject(response).ToString()))
                {
                    if (JsonConvert.SerializeObject(response).ToString().Length <= 300)
                    {
                        logger.Info(CustomMessage(string.Format("Response: {0}", JsonConvert.SerializeObject(response)), fileName, lineNumber, caller));
                    }
                    else
                    {
                        logger.Info(CustomMessage(string.Format("Response: {0} {1}", JsonConvert.SerializeObject(response).ToString().Substring(0, 300), "**No se puede mostrar el response completo debido a que excede la longitud maxima"), fileName, lineNumber, caller));
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
            string language = currentCulture.TwoLetterISOLanguageName;
            string lineSearch = (language == "en" ? ":line " : (language == "es" ? ":línea " : "notgetlanguage"));
            string innerExceptionMessage = string.Empty;
            string messageEx = string.Empty;
            if (ex != null)
            {
                int index = ex.StackTrace.LastIndexOf(lineSearch);
                if (index != -1)
                {
                    string lineNumberText = ex.StackTrace.Substring(index + lineSearch.Length);
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
            return string.Format("TransactionId: [{0}] CurrentMethod: [{1}:{2}({3})] ErrorMessage: [{4}] InnerExceptionMessage: [{5}]", Transaction.Id, vFileName, caller, lineNumber.ToString(), messageEx, innerExceptionMessage);
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
            return string.Format("TransactionId: [{0}] CurrentMethod: [{1}:{2}({3})]  InfoMessage: [{4}]", Transaction.Id, vFileName, caller, lineNumber.ToString(), message);
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
