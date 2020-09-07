using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// Generic Keys of WebConfig for Application
    /// </summary>
    public class ConfigurationLib : IConfigurationLib
    {

        public int CodigoExito => Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CodigoExito"]);
        public string MensajeExitoES => System.Configuration.ConfigurationManager.AppSettings["MensajeExitoES"];
        public string MensajeExitoEN => System.Configuration.ConfigurationManager.AppSettings["MensajeExitoEN"];

        public int CodigoErrorNoDataFound => Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CodigoErrorNoDataFound"]);
        public string NoDataFoundES => System.Configuration.ConfigurationManager.AppSettings["NoDataFoundES"];
        public string NoDataFoundEN => System.Configuration.ConfigurationManager.AppSettings["NoDataFoundEN"];

        public string Language => System.Configuration.ConfigurationManager.AppSettings["Language"];

        public int CodigoParametrosNoValido => Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CodigoParametrosNoValido"]);
        public string MensajeParametrosNoValidoEN => System.Configuration.ConfigurationManager.AppSettings["MensajeParametrosNoValidoEN"];
        public string MensajeParametrosNoValidoES => System.Configuration.ConfigurationManager.AppSettings["MensajeParametrosNoValidoES"];

        public int CodigoErrorBD => Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CodigoErrorBD"]);
        public string MensajeErrorBDEN => System.Configuration.ConfigurationManager.AppSettings["MensajeErrorBDEN"];
        public string MensajeErrorBDES => System.Configuration.ConfigurationManager.AppSettings["MensajeErrorBDES"];


        public int CodigoErrorTimeOut => Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CodigoErrorTimeOut"]);
        public string MensajeErrorTimeOutEN => System.Configuration.ConfigurationManager.AppSettings["MensajeErrorTimeOutEN"];
        public string MensajeErrorTimeOutES => System.Configuration.ConfigurationManager.AppSettings["MensajeErrorTimeOutES"];

        public int CodigoErrorNoEspecificado => Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CodigoErrorNoEspecificado"]);
        public string MensajeErrorNoEspecificadoEN => System.Configuration.ConfigurationManager.AppSettings["MensajeErrorNoEspecificadoEN"];
        public string MensajeErrorNoEspecificadoES => System.Configuration.ConfigurationManager.AppSettings["MensajeErrorNoEspecificadoES"];

        public int CodigoErrorNoFound => Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CodigoErrorNoFound"]);
        public string MensajeErrorNoFoundEN => System.Configuration.ConfigurationManager.AppSettings["MensajeErrorNoFoundEN"];
        public string MensajeErrorNoFoundES => System.Configuration.ConfigurationManager.AppSettings["MensajeErrorNoFoundES"];

        public int CodigoErrorNoAuthorized => Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CodigoErrorNoAuthorized"]);
        public string MensajeErrorNoAuthorizedEN => System.Configuration.ConfigurationManager.AppSettings["MensajeErrorNoAuthorizedEN"];
        public string MensajeErrorNoAuthorizedES => System.Configuration.ConfigurationManager.AppSettings["MensajeErrorNoAuthorizedES"];

        public int CodigoUserExist => Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CodigoUserExist"]);
        public string MensajeUserExistEN => System.Configuration.ConfigurationManager.AppSettings["MensajeUserExistEN"];
        public string MensajeUserExistES => System.Configuration.ConfigurationManager.AppSettings["MensajeUserExistES"];

        public int CodigoUserNoExist => Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CodigoUserNoExist"]);
        public string MensajeUserNoExistEN => System.Configuration.ConfigurationManager.AppSettings["MensajeUserNoExistEN"];
        public string MensajeUserNoExistES => System.Configuration.ConfigurationManager.AppSettings["MensajeUserNoExistES"];

        public int CodigoUserNoEnabled => Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CodigoUserNoEnabled"]);
        public string MensajeUserNoEnabeldES => System.Configuration.ConfigurationManager.AppSettings["MensajeUserNoEnabledES"];
        public string MensajeUserNoEnabledEN => System.Configuration.ConfigurationManager.AppSettings["MensajeUserNoEnabledEN"];

        public int CodigoUserExistInMediti => Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CodigoUserExistInMediti"]);
        public string MensajeUserExistInMeditiES => System.Configuration.ConfigurationManager.AppSettings["MensajeUserExistInMeditiES"];
        public string MensajeUserExistInMeditiEN => System.Configuration.ConfigurationManager.AppSettings["MensajeUserExistInMeditiEN"];

        public int CodigoWrongSecurityAnswers => Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CodigoWrongSecurityAnswers"]);
        public string MensajeWrongSecurityAnswersES => System.Configuration.ConfigurationManager.AppSettings["MensajeWrongSecurityAnswersES"];
        public string MensajeWrongSecurityAnswersEN => System.Configuration.ConfigurationManager.AppSettings["MensajeWrongSecurityAnswersEN"];

        public int CodigoForWithoutRoles => Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CodigoForWithoutRoles"]);
        public string MensajeForWithoutRolesES => System.Configuration.ConfigurationManager.AppSettings["MensajeForWithoutRolesES"];
        public string MensajeForWithoutRolesEN => System.Configuration.ConfigurationManager.AppSettings["MensajeForWithoutRolesEN"];

        public string SessionEnrollmentHistory => "SessionEnrollmentHistory";



        public int SecondsTimeOutBD => Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["SecondsTimeOutBD"]);
        public string TamanioMaximoEnrollmentFile => System.Configuration.ConfigurationManager.AppSettings["TamanioMaximoEnrollmentFile"];
        public string ExtensionValidEnrollmentFile => System.Configuration.ConfigurationManager.AppSettings["ExtensionValidEnrollmentFile"];
        public string PathEnrollmentFile => System.Configuration.ConfigurationManager.AppSettings["PathEnrollmentFile"];
        public string PathEnrollmentCreatePDF => System.Configuration.ConfigurationManager.AppSettings["PathEnrollmentCreatePDF"];
        //public string FTPServer => System.Configuration.ConfigurationManager.AppSettings["FTPServer"];
        //public string FTPUser => System.Configuration.ConfigurationManager.AppSettings["FTPUser"];
        //public string FTPPassword => System.Configuration.ConfigurationManager.AppSettings["FTPPassword"];
        //public int FTPPort => Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["FTPPort"]);
        //public string FTPUploadFile => System.Configuration.ConfigurationManager.AppSettings["FTPUploadFile"];
        public bool LogSQLQueries => Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["LogSQLQueries"]);

        public int CodigoMoreThanOneUserFound => Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CodigoMoreThanOneUserFound"]);
        public string MensajeMoreThanOneUserFoundES => System.Configuration.ConfigurationManager.AppSettings["MensajeMoreThanOneUserFoundES"];
        public string MensajeMoreThanOneUserFoundEN => System.Configuration.ConfigurationManager.AppSettings["MensajeMoreThanOneUserFoundEN"];

        public int CodigoUserNoCorrect => Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CodigoUserNoCorrect"]);
        public string MensajeUserNoCorrectES => System.Configuration.ConfigurationManager.AppSettings["MensajeUserNoCorrectES"];
        public string MensajeUserNoCorrectEN => System.Configuration.ConfigurationManager.AppSettings["MensajeUserNoCorrectEN"];

        public int CodigoUserQuestionPending => Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CodigoUserQuestionPending"]);
        public string MensajeUserQuestionPendingES => System.Configuration.ConfigurationManager.AppSettings["MensajeUserQuestionPendingES"];
        public string MensajeQuestionPendingEN => System.Configuration.ConfigurationManager.AppSettings["MensajeQuestionPendingEN"];

        public int CodigoSMSError => Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CodigoSMSError"]);
        public string MensajeSMSErrorES => System.Configuration.ConfigurationManager.AppSettings["MensajeSMSErrorES"];
        public string MensajeSMSErrorEN => System.Configuration.ConfigurationManager.AppSettings["MensajeSMSErrorEN"];

        public int CodigoMPIInCorrect => Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["CodigoMPIInCorrect"]);
        public string MensajeMPIInCorrectES => System.Configuration.ConfigurationManager.AppSettings["MensajeMPIInCorrectES"];
        public string MensajeMPIInCorrectEN => System.Configuration.ConfigurationManager.AppSettings["MensajeMPIInCorrectEN"];

        public string GetMessageEnByCode(int errorCode)
        {
            if (errorCode.Equals(CodigoExito)) return MensajeExitoEN;
            if (errorCode.Equals(CodigoErrorNoDataFound)) return NoDataFoundEN;
            if (errorCode.Equals(CodigoParametrosNoValido)) return MensajeParametrosNoValidoEN;
            if (errorCode.Equals(CodigoErrorBD)) return MensajeErrorBDEN;
            if (errorCode.Equals(CodigoErrorTimeOut)) return MensajeErrorTimeOutEN;
            if (errorCode.Equals(CodigoErrorNoEspecificado)) return MensajeErrorNoEspecificadoEN;
            if (errorCode.Equals(CodigoErrorNoFound)) return MensajeErrorNoFoundEN;
            if (errorCode.Equals(CodigoErrorNoAuthorized)) return MensajeErrorNoAuthorizedEN;
            if (errorCode.Equals(CodigoUserExist)) return MensajeUserExistEN;
            if (errorCode.Equals(CodigoUserNoExist)) return MensajeUserNoExistEN;
            if (errorCode.Equals(CodigoUserNoEnabled)) return MensajeUserNoEnabledEN;
            if (errorCode.Equals(CodigoMoreThanOneUserFound)) return MensajeMoreThanOneUserFoundEN;
            if (errorCode.Equals(CodigoUserNoCorrect)) return MensajeUserNoCorrectEN;
            return string.Empty;
        }

        public string GetMessageEsByCode(int errorCode)
        {
            if (errorCode.Equals(CodigoExito)) return MensajeExitoES;
            if (errorCode.Equals(CodigoErrorNoDataFound)) return NoDataFoundES;
            if (errorCode.Equals(CodigoParametrosNoValido)) return MensajeParametrosNoValidoES;
            if (errorCode.Equals(CodigoErrorBD)) return MensajeErrorBDES;
            if (errorCode.Equals(CodigoErrorTimeOut)) return MensajeErrorTimeOutES;
            if (errorCode.Equals(CodigoErrorNoEspecificado)) return MensajeErrorNoEspecificadoES;
            if (errorCode.Equals(CodigoErrorNoFound)) return MensajeErrorNoFoundES;
            if (errorCode.Equals(CodigoErrorNoAuthorized)) return MensajeErrorNoAuthorizedES;
            if (errorCode.Equals(CodigoUserExist)) return MensajeUserExistES;
            if (errorCode.Equals(CodigoUserNoExist)) return MensajeUserNoExistES;
            if (errorCode.Equals(CodigoUserNoEnabled)) return MensajeUserNoEnabeldES;
            if (errorCode.Equals(CodigoMoreThanOneUserFound)) return MensajeMoreThanOneUserFoundES;
            if (errorCode.Equals(CodigoUserNoCorrect)) return MensajeUserNoCorrectES;
            return string.Empty;
        }
    }
}
