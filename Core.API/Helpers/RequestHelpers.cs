using Common;
using Common.Generic.HttpHelpers;
using Common.Logging;
using System;
using System.Web;

public class RequestHelpers
{
    public static string AuditUserData(CustomHeader header)
    {
        string userData = string.Empty;
        try
        {
            string origen = header.Origin.GetOrigin();
            if (String.IsNullOrEmpty(origen)) origen = AppConstants.Unknow;
            userData = "Origen: [" + origen + "] Browser: [" + HttpContext.Current.Request.Browser.Type + "]";
        }
        catch
        {
            userData = "Origen: [" + AppConstants.Unknow + "] Browser: [" + AppConstants.Unknow + "]";
        }
        return userData;
    }
}
