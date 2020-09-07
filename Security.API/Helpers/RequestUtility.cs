using Common;
using Common.Extensions;
using Common.Generic.HttpHelpers;
using Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Security.API.Helpers
{
    public static class RequestUtility
    {

        public static CustomHeader GetHeaders()
        {
            HttpRequestMessage Request = System.Web.HttpContext.Current.Items["MS_HttpRequestMessage"] as HttpRequestMessage;
            CustomHeader customHeader = new CustomHeader();
            if (Request != null)
            {
                if (Request.Headers != null)
                {
                    string transactionId = string.Empty;
                    if (Request.Headers.Contains(AppConstants.TransactionId))
                    {
                        transactionId = Request.Headers.GetValues(AppConstants.TransactionId).First();
                    }
                    if (Request.Headers.Contains(AppConstants.Origin))
                    {
                        customHeader.Origin = Request.Headers.GetValues(AppConstants.Origin).First();
                    }
                    customHeader.Transaction = transactionId.GetTransaction();
                }
                else
                {
                    customHeader.Transaction = string.Empty.GetTransaction();
                    customHeader.Origin = AppConstants.Unknow;
                }
            }
            else
            {
                customHeader.Transaction = string.Empty.GetTransaction();
                customHeader.Origin = AppConstants.Unknow;
            }
            return customHeader;
        }
    }
}
