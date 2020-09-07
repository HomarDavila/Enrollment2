using Common;
using Common.HttpHelpers;
using Common.Logging;
using System;
using System.Collections.Generic;

namespace Service.Interfaces
{
    public interface IMailService
    {
        EResponseBase<SimpleEntity> SendMail(string from, string to, string cc, string bcc, string customSubject, string bodyHTML, List<Array> attachPaths, List<string> ContentToReplace, List<string> FormatToReplace);
        ITransaction Transaction { get; set; }
        ICustomLog Logger { get; set; }
    }
}
