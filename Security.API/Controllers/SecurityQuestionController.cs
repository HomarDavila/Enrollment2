using AutoMapper;
using Common;
using Common.Generic.HttpHelpers;
using Common.Logging;
using Security.API.Helpers;
using Security.API.Model.Response;
using Service.DependecyInjection;
using Service.Interfaces;
using Service.Interfaces.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Security.API.Controllers
{
    [RoutePrefix("api/identity/SecurityQuestion/v1")]
    public class SecurityQuestionController : ApiController
    {
        private readonly IQuestionService service;
        private readonly IConfigurationLib config;
        private readonly ICustomLog logger;

        public SecurityQuestionController()
        {
            service = DependencyFactory.GetInstance<IQuestionService>();
            config = DependencyFactory.GetInstance<IConfigurationLib>();
            logger = DependencyFactory.GetInstance<ICustomLog>();
        }


        [Route("Get")]
        [HttpGet]
        public EResponseBase<SecurityQuestion_Response_v1> Get()
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                ConfigureService();
                logger.Print_InitMethod();
                try
                {
                    logger.Print_Request(null);
                    EResponseBase<Domain.Custom_Models.SecurityAnswer> responseJSON = service.GetSecurityQuestions();
                    logger.Print_Response(responseJSON);
                    EResponseBase<SecurityQuestion_Response_v1> response = Mapper.Map<EResponseBase<SecurityQuestion_Response_v1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<SecurityQuestion_Response_v1>(config).setResponseBaseForException(ex);
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
            CustomHeader header = RequestUtility.GetHeaders();
            logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, header.Transaction);
            logger.Header = RequestHelpers.AuditUserData(header);
            return header;
        }

    }
}
