using Audit.WebApi;
using AutoMapper;
using Common;
using Common.Generic.HttpHelpers;
using Common.Logging;
using Core.API.Helpers;
using Core.API.Model.Response;
using Domain.Entity_Models.Core;
using Service.DependecyInjection;
using Service.Interfaces;
using Service.Interfaces.Core;
using Sms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Core.API.Controllers
{
    //[AuditApi(EventTypeName = "{controller}/{action} ({verb})", IncludeResponseBody = true, IncludeRequestBody = true, IncludeModelState = true)]
    [RoutePrefix("api/Puntuation/v1")]
    public class PuntuationController: ApiController
    {

        private readonly IPuntuationServices puntuationServices = DependencyFactory.GetInstance<IPuntuationServices>();
        private readonly IConfigurationLib config = DependencyFactory.GetInstance<IConfigurationLib>();
        private readonly ICustomLog logger;

        public PuntuationController()
        {
            puntuationServices = DependencyFactory.GetInstance<IPuntuationServices>();
            config = DependencyFactory.GetInstance<IConfigurationLib>();
            logger = DependencyFactory.GetInstance<ICustomLog>();
        }

        [Route("Get")]
        [HttpGet]
        public EResponseBase<PuntuationResponseV1> Get()
        {
            
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    logger.Print_Request(null);
                    EResponseBase<Puntuation> responseJSON = puntuationServices.Get();
                    logger.Print_Response(responseJSON);
                    EResponseBase<PuntuationResponseV1> response = Mapper.Map<EResponseBase<PuntuationResponseV1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    return new UtilitariesResponse<PuntuationResponseV1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        [Route("GetById/{Id}")]
        [HttpGet]
        public EResponseBase<PuntuationResponseV1> GetById(int Id)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    logger.Print_Request(Id);
                    EResponseBase<Puntuation> responseJSON = puntuationServices.GetById(Id);
                    logger.Print_Response(responseJSON);
                    EResponseBase<PuntuationResponseV1> response = Mapper.Map<EResponseBase<PuntuationResponseV1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    return new UtilitariesResponse<PuntuationResponseV1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        private void ConfigureService()
        {
            puntuationServices.Transaction = RequestUtility.GetHeaders().Transaction;
            puntuationServices.Logger = logger;
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