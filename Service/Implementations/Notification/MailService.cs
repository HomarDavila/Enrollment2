using Common;
using Common.HttpHelpers;
using Common.Logging;
using Service.Helpers;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Mail;

namespace Service.Implementations
{
    public class MailService : IMailService
    {
        private readonly IConfigurationLib config;
        public ITransaction Transaction { get; set; }
        public ICustomLog Logger { get; set; }

        public MailService(IConfigurationLib _config)
        {
            config = _config;
        }

        public EResponseBase<SimpleEntity> SendMail(string from,
                                                    string to,
                                                    string cc,
                                                    string bcc,
                                                    string customSubject,
                                                    string bodyHTML,
                                                    List<Array> attachPaths,
                                                    List<string> ContentToReplace,
                                                    List<string> FormatToReplace)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<SimpleEntity> result = new EResponseBase<SimpleEntity>();
            try
            {
                Logger.Print_InitMethod();
                Logger.Print_Request(null, printDebug: true);
                MailMessage mailMessage = GetHeaderMail(from, to, cc, bcc, customSubject);
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = GetBodyHTML(ContentToReplace, bodyHTML);
                if (attachPaths!=null)
                {
                    AttachFiles(attachPaths, ref mailMessage);
                }
                
                using (SmtpClient smtp = new SmtpClient())
                {
                    try
                    {
                        ConfigurateSMTP(smtp);
                        smtp.Send(mailMessage);
                        //smtp.Send(mailMessage);
                        //smtp.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
                        result = new UtilitariesResponse<SimpleEntity>(config).setResponseBaseForOK();
                        //DisposeAttachments(mailMessage);
                    }
                    catch (Exception ex)
                    {
                        result = new UtilitariesResponse<SimpleEntity>(config).setResponseBaseForException(ex);
                        Logger.Error(ex.Message);
                    }

                }
                Logger.Print_Response(result, printDebug: true);
                Logger.Print_EndMethod();
            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<SimpleEntity>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }
            return result;
        }

        private static bool mailSent = false;
        private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the unique identifier for this asynchronous operation.
            String token = (string)e.UserState;

            if (e.Cancelled)
            {
                Console.WriteLine("[{0}] Send canceled.", token);
            }
            if (e.Error != null)
            {
                Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
            }
            else
            {
                Console.WriteLine("Message sent.");
            }
            mailSent = true;
        }

        //Utilities
        private MailMessage GetHeaderMail(string from, string to, string cc, string bcc, string customSubject)
        {
            List<string> listCC = new List<string>();
            List<string> listBCC = new List<string>();
            List<string> listTO = new List<string>();

            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(from)
            };
            if (!string.IsNullOrEmpty(to)) listTO = to.Split(';').ToList();
            if (!string.IsNullOrEmpty(cc)) listCC = cc.Split(';').ToList();
            if (!string.IsNullOrEmpty(bcc)) listBCC = bcc.Split(';').ToList();
            if (listTO.Any()) listTO.ForEach(x => mailMessage.To.Add(x));
            if (listCC.Any()) listCC.ForEach(x => mailMessage.CC.Add(x));
            if (listBCC.Any()) listBCC.ForEach(x => mailMessage.Bcc.Add(x));
            if (!String.IsNullOrEmpty(customSubject)) mailMessage.Subject = customSubject;
            return mailMessage;
        }
        private string GetBodyHTML(List<string> ContentToReplace, List<string> FormatToReplace, string customBodyHTML)
        {
            string bodyFormatReplaced = GetBodyHTML(FormatToReplace, customBodyHTML);
            string returnHTML = GetBodyHTML(ContentToReplace, bodyFormatReplaced);
            return returnHTML;
        }
        private string GetBodyHTML(List<string> stringsToReplace, string bodyHTML)
        {
            //Esto es un pequeño hack para reemplazar valores de la plantilla
            //Los strings a reemplazar deben estar en formato key%value 
            //Example : user%abc
            if (stringsToReplace != null)
            {
                Dictionary<string, string> keyValuePairs = stringsToReplace.Select(value => value.Split('%'))
                                                          .ToDictionary(pair => pair[0], pair => pair[1]);
                foreach (KeyValuePair<string, string> entry in keyValuePairs)
                {
                    bodyHTML = bodyHTML.Replace(entry.Key, entry.Value);
                }
            }

            return bodyHTML;
        }
        private void ConfigurateSMTP(SmtpClient smtp)
        {
            smtp.Host = CustomConfigurationLib.Host;
            smtp.Port = CustomConfigurationLib.Port;
            string userName = CustomConfigurationLib.UserName;
            string password = CustomConfigurationLib.Password;
            smtp.Credentials = new System.Net.NetworkCredential(userName, password);
            smtp.EnableSsl = CustomConfigurationLib.EnableSSL;
        }
        private void AttachFiles(List<Array> attachPaths, ref MailMessage mailMessage)
        {
            foreach (object[] strFile in attachPaths)
            {
                mailMessage.Attachments.Add(new Attachment((Stream)strFile[0], Convert.ToString(strFile[1])));
            }
        }
        private static void DisposeAttachments(MailMessage mailMessage)
        {
            if (mailMessage.Attachments != null)
            {
                for (Int32 i = mailMessage.Attachments.Count - 1; i >= 0; i--)
                {
                    mailMessage.Attachments[i].Dispose();
                }
                mailMessage.Attachments.Clear();
                mailMessage.Attachments.Dispose();
            }
            mailMessage.Dispose();
        }


    }
}
