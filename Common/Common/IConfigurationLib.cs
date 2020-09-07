namespace Common
{
    /// <summary>
    /// Generic Keys of WebConfig for Application
    /// </summary>
    public interface IConfigurationLib
    {
        int CodigoExito { get; }
        string MensajeExitoES { get; }
        string MensajeExitoEN { get; }

        int CodigoErrorNoDataFound { get; }
        string NoDataFoundES { get; }
        string NoDataFoundEN { get; }

        int CodigoParametrosNoValido { get; }
        string MensajeParametrosNoValidoEN { get; }
        string MensajeParametrosNoValidoES { get; }

        int CodigoErrorBD { get; }
        string MensajeErrorBDEN { get; }
        string MensajeErrorBDES { get; }


        int CodigoErrorTimeOut { get; }
        string MensajeErrorTimeOutEN { get; }
        string MensajeErrorTimeOutES { get; }

        int CodigoErrorNoEspecificado { get; }
        string MensajeErrorNoEspecificadoEN { get; }
        string MensajeErrorNoEspecificadoES { get; }

        int CodigoErrorNoFound { get; }
        string MensajeErrorNoFoundEN { get; }
        string MensajeErrorNoFoundES { get; }

        int CodigoErrorNoAuthorized { get; }
        string MensajeErrorNoAuthorizedEN { get; }
        string MensajeErrorNoAuthorizedES { get; }

        int CodigoUserExist { get; }
        string MensajeUserExistEN { get; }
        string MensajeUserExistES { get; }

        int CodigoUserQuestionPending { get; }
        string MensajeUserQuestionPendingES { get; }
        string MensajeQuestionPendingEN { get; }

        int CodigoUserNoExist { get; }
        string MensajeUserNoExistEN { get; }
        string MensajeUserNoExistES { get; }

        int CodigoUserNoEnabled { get; }
        string MensajeUserNoEnabeldES { get; }
        string MensajeUserNoEnabledEN { get; }

        int CodigoUserExistInMediti { get; }
        string MensajeUserExistInMeditiES { get; }
        string MensajeUserExistInMeditiEN { get; }

        int CodigoWrongSecurityAnswers { get; }
        string MensajeWrongSecurityAnswersES { get; }
        string MensajeWrongSecurityAnswersEN { get; }

        int SecondsTimeOutBD { get; }
        bool LogSQLQueries { get; }

        int CodigoMoreThanOneUserFound { get; }
        string MensajeMoreThanOneUserFoundES { get; }
        string MensajeMoreThanOneUserFoundEN { get; }

        int CodigoForWithoutRoles { get; }
        string MensajeForWithoutRolesES { get; }
        string MensajeForWithoutRolesEN { get; }

        

        int CodigoUserNoCorrect { get; }
        string MensajeUserNoCorrectES { get; }
        string MensajeUserNoCorrectEN { get; }

        string GetMessageEsByCode(int errorCode);
        string GetMessageEnByCode(int errorCode);
        string PathEnrollmentCreatePDF { get; }


        int CodigoSMSError{ get; }
        string MensajeSMSErrorES { get; }
        string MensajeSMSErrorEN { get; }

        string Language { get; }
    }
}
