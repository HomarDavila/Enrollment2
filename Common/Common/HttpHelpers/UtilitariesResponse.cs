using Common.Exceptions;
using Common.HttpHelpers;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class UtilitariesResponse<T> where T : class, new()
    {
        private IConfigurationLib ConfigurationLib = null;

        public UtilitariesResponse(IConfigurationLib _configurationLib)
        {
            ConfigurationLib = _configurationLib;
        }

        public EResponseBase<T> setResponseBaseForExecuteSQLCommand(int result)
        {
            EResponseBase<T> response = new EResponseBase<T>();
            if (result >= 0)
            {
                response.Code = ConfigurationLib.CodigoExito;
                response.Message = ConfigurationLib.MensajeExitoES;
                response.MessageEN = ConfigurationLib.MensajeExitoEN;
            }
            else
            {
                response.Code = ConfigurationLib.CodigoErrorNoDataFound;
                response.Message = ConfigurationLib.NoDataFoundES;
                response.MessageEN = ConfigurationLib.NoDataFoundEN;
            }
            return response;
        }
        public EResponseBase<T> setResponseBaseForList(IQueryable<T> query)
        {
            EResponseBase<T> response = new EResponseBase<T>();
            if (query.Any())
            {
                List<T> list = query.ToList();
                response.Code = ConfigurationLib.CodigoExito;
                response.Message = ConfigurationLib.MensajeExitoES;
                response.MessageEN = ConfigurationLib.MensajeExitoEN;
                response.listado = list;
            }
            else
            {
                response.Code = ConfigurationLib.CodigoErrorNoDataFound;
                response.Message = ConfigurationLib.NoDataFoundES;
                response.MessageEN = ConfigurationLib.NoDataFoundEN;
            }
            return response;
        }
        public EResponseBase<T> setResponseBaseForList(IList<T> query)
        {
            EResponseBase<T> response = new EResponseBase<T>();
            if (query != null)
            {
                if (query.Any())
                {
                    response.Code = ConfigurationLib.CodigoExito;
                    response.Message = ConfigurationLib.MensajeExitoES;
                    response.MessageEN = ConfigurationLib.MensajeExitoEN;
                    response.listado = query.ToList();
                    response.IsResultList = true;
                }
                else
                {
                    response.Code = ConfigurationLib.CodigoErrorNoDataFound;
                    response.Message = ConfigurationLib.NoDataFoundES;
                    response.MessageEN = ConfigurationLib.NoDataFoundEN;
                }
            }
            else
            {
                response.Code = ConfigurationLib.CodigoErrorNoDataFound;
                response.Message = ConfigurationLib.NoDataFoundES;
                response.MessageEN = ConfigurationLib.NoDataFoundEN;
            }

            return response;
        }
        public EResponseBase<T> setResponseBaseForObj(T obj)
        {
            EResponseBase<T> response = new EResponseBase<T>();
            if (obj != null)
            {
                response.Code = ConfigurationLib.CodigoExito;
                response.Message = ConfigurationLib.MensajeExitoES;
                response.MessageEN = ConfigurationLib.MensajeExitoEN;
                response.objeto = obj;
            }
            else
            {
                response.Code = ConfigurationLib.CodigoErrorNoDataFound;
                response.Message = ConfigurationLib.NoDataFoundES;
                response.MessageEN = ConfigurationLib.NoDataFoundEN;
            }
            return response;
        }
        public EResponseBase<T> setResponseBaseForBoolean(bool responseWS)
        {
            EResponseBase<T> response = new EResponseBase<T>
            {
                Code = ConfigurationLib.CodigoExito,
                Message = ConfigurationLib.MensajeExitoES,
                MessageEN = ConfigurationLib.MensajeExitoEN,
                IsOK = responseWS
            };
            return response;
        }
        public EResponseBase<T> setResponseBaseForValidationExceptionString(List<string> list)
        {
            EResponseBase<T> response = new EResponseBase<T>
            {
                Code = ConfigurationLib.CodigoParametrosNoValido,
                Message = ConfigurationLib.MensajeParametrosNoValidoES,
                MessageEN = ConfigurationLib.MensajeParametrosNoValidoEN,
                FunctionalErrors = new List<SimpleEntity>()
            };
            foreach (string obj in list)
            {
                response.FunctionalErrors.Add(new SimpleEntity() { Id = 0, NameEN = obj, NameES = obj });
            }
            return response;
        }
        public EResponseBase<T> setResponseBaseForValidationExceptionString(IList<SimpleEntity> errors)
        {
            EResponseBase<T> response = new EResponseBase<T>
            {
                Code = ConfigurationLib.CodigoParametrosNoValido,
                Message = ConfigurationLib.MensajeParametrosNoValidoES,
                MessageEN = ConfigurationLib.MensajeParametrosNoValidoEN,
                FunctionalErrors = errors.ToList()
            };
            return response;
        }
        public EResponseBase<T> setResponseBaseForOK()
        {
            EResponseBase<T> response = new EResponseBase<T>
            {
                Code = ConfigurationLib.CodigoExito,
                Message = ConfigurationLib.MensajeExitoES,
                MessageEN = ConfigurationLib.MensajeExitoEN
            };
            return response;
        }
        public EResponseBase<T> setResponseBaseForOK(T obj)
        {
            EResponseBase<T> response = new EResponseBase<T>
            {
                Code = ConfigurationLib.CodigoExito,
                Message = ConfigurationLib.MensajeExitoES,
                MessageEN = ConfigurationLib.MensajeExitoEN
            };
            if (obj != null) response.objeto = obj;
            return response;
        }
        public EResponseBase<T> setResponseBaseForOK(IEnumerable<T> obj)
        {
            EResponseBase<T> response = new EResponseBase<T>
            {
                Code = ConfigurationLib.CodigoExito,
                Message = ConfigurationLib.MensajeExitoES,
                MessageEN = ConfigurationLib.MensajeExitoEN
            };
            if (obj.Any()) { response.listado = obj; response.IsResultList = true; }
            return response;
        }
        public EResponseBase<T> setResponseBaseForExceptionUnexpected()
        {
            EResponseBase<T> response = new EResponseBase<T>
            {
                Code = ConfigurationLib.CodigoErrorNoEspecificado,
                Message = ConfigurationLib.MensajeErrorNoEspecificadoES,
                MessageEN = ConfigurationLib.MensajeErrorNoEspecificadoEN
            };
            return response;
        }
        public EResponseBase<T> setResponseBaseForException(Exception ex)
        {
            EResponseBase<T> response = new EResponseBase<T>();
            if (ex is TimeoutException)
            {
                response.Code = ConfigurationLib.CodigoErrorTimeOut;
                response.Message = ConfigurationLib.MensajeErrorTimeOutES;
                response.MessageEN = ConfigurationLib.MensajeErrorTimeOutEN;
            }
            else if (ex is HttpRequestException)
            {
                response.Code = ConfigurationLib.CodigoErrorTimeOut;
                response.Message = ConfigurationLib.MensajeErrorTimeOutES;
                response.MessageEN = ConfigurationLib.MensajeErrorTimeOutEN;
            }
            else if (ex is WSNotAuthorized)
            {
                response.Code = ConfigurationLib.CodigoErrorNoAuthorized;
                response.Message = ConfigurationLib.MensajeErrorNoAuthorizedES;
                response.MessageEN = ConfigurationLib.MensajeErrorNoAuthorizedEN;
            }
            else if (ex is WSNotFoundException)
            {
                response.Code = ConfigurationLib.CodigoErrorNoFound;
                response.Message = ConfigurationLib.MensajeErrorNoFoundES;
                response.MessageEN = ConfigurationLib.MensajeErrorNoFoundEN;
            }
            else
            {
                response.Code = ConfigurationLib.CodigoErrorNoEspecificado;
                response.Message = ConfigurationLib.MensajeErrorNoEspecificadoES;
                response.MessageEN = ConfigurationLib.MensajeErrorNoEspecificadoEN;
            }
            response.TechnicalErrors = ex;
            return response;
        }
        public EResponseBase<T> setResponseBaseForNoAuthorized()
        {
            EResponseBase<T> response = new EResponseBase<T>
            {
                Code = ConfigurationLib.CodigoErrorNoAuthorized,
                Message = ConfigurationLib.MensajeErrorNoAuthorizedES,
                MessageEN = ConfigurationLib.MensajeErrorNoAuthorizedEN,
                listado = new List<T>()
            };
            return response;
        }
        public EResponseBase<T> setResponseBaseForMoreThanOneUserFound()
        {
            EResponseBase<T> response = new EResponseBase<T>
            {
                Code = ConfigurationLib.CodigoMoreThanOneUserFound,
                Message = ConfigurationLib.MensajeMoreThanOneUserFoundES,
                MessageEN = ConfigurationLib.MensajeMoreThanOneUserFoundEN,
                listado = new List<T>()
            };
            return response;
        }
        public EResponseBase<T> setResponseBaseForWithouRoles()
        {
            EResponseBase<T> response = new EResponseBase<T>
            {
                Code = ConfigurationLib.CodigoForWithoutRoles,
                Message = ConfigurationLib.MensajeForWithoutRolesES,
                MessageEN = ConfigurationLib.MensajeForWithoutRolesEN,
                listado = new List<T>()
            };
            return response;
        }        
        public EResponseBase<T> setResponseBaseForNoAuthorized(TokenErrorResponse error)
        {
            EResponseBase<T> response = new EResponseBase<T>
            {
                Code = ConfigurationLib.CodigoErrorNoAuthorized,
                Message = ConfigurationLib.MensajeErrorNoAuthorizedES,
                MessageEN = ConfigurationLib.MensajeErrorNoAuthorizedEN,
                listado = new List<T>()
            };
            List<string> errorResponse = new List<string>();
            int errorCode = Int32.Parse(error.ErrorDescription);
            string messageEs = ConfigurationLib.GetMessageEsByCode(errorCode);
            string messageEn = ConfigurationLib.GetMessageEnByCode(errorCode);
            SimpleEntity functionalError = new SimpleEntity();
            response.FunctionalErrors = new List<SimpleEntity>() { functionalError };
            return response;
        }
        public EResponseBase<T> setResponseBaseForNoDataFound()
        {
            EResponseBase<T> response = new EResponseBase<T>
            {
                Code = ConfigurationLib.CodigoErrorNoDataFound,
                Message = ConfigurationLib.NoDataFoundES,
                MessageEN = ConfigurationLib.NoDataFoundEN,
                listado = new List<T>()
            };
            return response;
        }
        public EResponseBase<T> setResponseBaseForWrongAnswers()
        {
            EResponseBase<T> response = new EResponseBase<T>
            {
                Code = ConfigurationLib.CodigoWrongSecurityAnswers,
                Message = ConfigurationLib.MensajeWrongSecurityAnswersES,
                MessageEN = ConfigurationLib.MensajeWrongSecurityAnswersEN,
                listado = new List<T>()
            };
            return response;
        }
        public EResponseBase<T> setResponseBaseForParameterNoValid()
        {
            EResponseBase<T> response = new EResponseBase<T>
            {
                Code = ConfigurationLib.CodigoParametrosNoValido,
                Message = ConfigurationLib.MensajeParametrosNoValidoES,
                MessageEN = ConfigurationLib.MensajeParametrosNoValidoEN,
                listado = new List<T>()
            };
            return response;
        }
        public EResponseBase<T> setResponseBaseForUserExist()
        {
            EResponseBase<T> response = new EResponseBase<T>
            {
                Code = ConfigurationLib.CodigoUserExist,
                Message = ConfigurationLib.MensajeUserExistES,
                MessageEN = ConfigurationLib.MensajeUserExistEN,
                listado = new List<T>()
            };
            return response;
        }
        public EResponseBase<T> setResponseBaseForUserExistInMediti()
        {
            EResponseBase<T> response = new EResponseBase<T>
            {
                Code = ConfigurationLib.CodigoUserExistInMediti,
                Message = ConfigurationLib.MensajeUserExistInMeditiES,
                MessageEN = ConfigurationLib.MensajeUserExistInMeditiEN,
                listado = new List<T>()
            };
            return response;
        }
        public EResponseBase<T> setResponseBaseForUserNoExist()
        {
            EResponseBase<T> response = new EResponseBase<T>
            {
                Code = ConfigurationLib.CodigoUserNoExist,
                Message = ConfigurationLib.MensajeUserNoExistES,
                MessageEN = ConfigurationLib.MensajeUserNoExistEN,
                listado = new List<T>()
            };
            return response;
        }
        public EResponseBase<T> setResponseBaseForUserNoEnabled()
        {
            EResponseBase<T> response = new EResponseBase<T>
            {
                Code = ConfigurationLib.CodigoUserNoEnabled,
                Message = ConfigurationLib.MensajeUserNoEnabeldES,
                MessageEN = ConfigurationLib.MensajeUserNoEnabledEN,
                listado = new List<T>()
            };
            return response;
        }
        public EResponseBase<T> setResponseBaseForSmsError()
        {
            EResponseBase<T> response = new EResponseBase<T>
            {
                Code = ConfigurationLib.CodigoSMSError,
                Message = ConfigurationLib.MensajeSMSErrorES,
                MessageEN = ConfigurationLib.MensajeSMSErrorEN,
                listado = new List<T>()
            };
            return response;
        }
    }
}
