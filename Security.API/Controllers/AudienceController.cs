using AutoMapper;
using Common;
using Service.DependecyInjection;
using Service.Interfaces;
using System;
using System.Web.Http;
using Security.API.Helpers;
using Security.API.Model.Response;
using Security.API.Model.Request;
using System.Security.Cryptography;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Common.Generic.HttpHelpers;
using Common.Logging;

namespace Security.API.Controllers
{
    [RoutePrefix("api/identity/Option/v1")]
    public class AudienceController : ApiController
    {
        private readonly IAudienceService service;
        private readonly IConfigurationLib config;
        private readonly ICustomLog logger;

        public AudienceController()
        {
            service = DependencyFactory.GetInstance<IAudienceService>();
            config = DependencyFactory.GetInstance<IConfigurationLib>();
            logger = DependencyFactory.GetInstance<ICustomLog>();
        }
      

        [Route("Register")]
        [HttpPost]
        public EResponseBase<Audience_Response_v1> Register([FromBody] Audience_Request_v1 request)
        {
            var customHeader = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(customHeader)))
            {
                logger.Print_InitMethod();
                ConfigureService();                
                try
                {
                    logger.Print_Request(request);
                    var key = new byte[32];
                    RNGCryptoServiceProvider.Create().GetBytes(key);
                    var base64Secret = TextEncodings.Base64Url.Encode(key);
                    var responseJSON = service.Register(request.Name, base64Secret);
                    logger.Print_Response(responseJSON);
                    var response = Mapper.Map<EResponseBase<Audience_Response_v1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {    
                    logger.Error(ex);
                    return new UtilitariesResponse<Audience_Response_v1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        private void ConfigureService()
        {
            service.Transaction = RequestUtility.GetHeaders().Transaction;
            service.Logger = logger;
        }

        private CustomHeader ConfigureLogHeader()
        {
            var header = RequestUtility.GetHeaders();            
            logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, header.Transaction);
            logger.Header = RequestHelpers.AuditUserData(header);
            return header;
        }

    }
}
