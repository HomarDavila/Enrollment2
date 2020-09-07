using AutoMapper;
using Common;
using Common.Generic.HttpHelpers;
using Common.Logging;
using Domain.Entity_Models.Identity;
using Security.API.Helpers;
using Security.API.Model.Request;
using Security.API.Model.Response;
using Service.DependecyInjection;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Security.API.Controllers
{
    [RoutePrefix("api/identity/OptionRol/v1")]
    public class OptionRolController : ApiController
    {
        private readonly IOptionRolService service;
        private readonly IConfigurationLib config;
        private readonly ICustomLog logger;


        public OptionRolController()
        {
            service = DependencyFactory.GetInstance<IOptionRolService>();
            config = DependencyFactory.GetInstance<IConfigurationLib>();
            logger = DependencyFactory.GetInstance<ICustomLog>();
        }

        [Authorize]
        [Route("Register")]
        [HttpPost]
        public EResponseBase<OptionRol_Response_v1> Register([FromBody] List<OptionRol_Request_v1> request)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                ConfigureService();
                logger.Print_InitMethod();
                try
                {
                    logger.Print_Request(request);
                    List<OptionRol> requestConvert = Mapper.Map<List<OptionRol_Request_v1>, List<OptionRol>>(request);
                    EResponseBase<OptionRol> responseJSON = service.Insert(requestConvert);
                    logger.Print_Response(responseJSON);
                    EResponseBase<OptionRol_Response_v1> response = Mapper.Map<EResponseBase<OptionRol_Response_v1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<OptionRol_Response_v1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        [Authorize]
        [Route("Delete")]
        [HttpDelete]
        public EResponseBase<OptionRol_Response_v1> Delete([FromBody] OptionRol_Request_v1 request)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                ConfigureService();
                logger.Print_InitMethod();
                try
                {
                    logger.Print_Request(request);
                    OptionRol requestConvert = Mapper.Map<OptionRol>(request);
                    EResponseBase<OptionRol> responseJSON = service.Delete(requestConvert);
                    logger.Print_Response(responseJSON);
                    EResponseBase<OptionRol_Response_v1> response = Mapper.Map<EResponseBase<OptionRol_Response_v1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<OptionRol_Response_v1>(config).setResponseBaseForException(ex);
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
