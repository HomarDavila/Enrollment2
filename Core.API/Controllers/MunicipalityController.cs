using AutoMapper;
using Common;
using Common.Generic.HttpHelpers;
using Common.Logging;
using Core.API.Helpers;
using Core.API.Model.Response;
using Domain.Entity_Models;
using Service.DependecyInjection;
using Service.Interfaces;
using System;
using System.Web.Http;

namespace Core.API.Controllers
{
    [RoutePrefix("api/Municipality/v1")]
    public class MunicipalityController : ApiController
    {
        private readonly IMunicipalityServices MunicipalityServices = DependencyFactory.GetInstance<IMunicipalityServices>();
        private readonly IConfigurationLib config = DependencyFactory.GetInstance<IConfigurationLib>();
        private readonly ICustomLog logger;
        // GET: Municipality
        public MunicipalityController()
        {
            MunicipalityServices = DependencyFactory.GetInstance<IMunicipalityServices>();
            config = DependencyFactory.GetInstance<IConfigurationLib>();
            logger = DependencyFactory.GetInstance<ICustomLog>();
        }

        //[Authorize]
        [Route("Get")]
        [HttpGet]
        public EResponseBase<MunicipalityResponseV1> Get()
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    logger.Print_Request(null);
                    EResponseBase<Municipality> responseJSON = MunicipalityServices.Get();
                    logger.Print_Response(responseJSON);
                    EResponseBase<MunicipalityResponseV1> response = Mapper.Map<EResponseBase<MunicipalityResponseV1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    return new UtilitariesResponse<MunicipalityResponseV1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        private void ConfigureService()
        {
            MunicipalityServices.Transaction = RequestUtility.GetHeaders().Transaction;
            MunicipalityServices.Logger = logger;
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