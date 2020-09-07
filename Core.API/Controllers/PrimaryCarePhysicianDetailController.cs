using Audit.WebApi;
using AutoMapper;
using Common;
using Common.Generic.HttpHelpers;
using Common.HttpHelpers;
using Common.Logging;
using Core.API.Helpers;
using Core.API.Model;
using Core.API.Model.Request;
using Core.API.Model.Response;
using Domain.Custom_Models;
using Service.DependecyInjection;
using Service.Interfaces;
using System;
using System.Linq;
using System.Web.Http;

namespace Core.API.Controllers
{
    [RoutePrefix("api/PrimaryCarePhysicianDetail/v1")]
    public class PrimaryCarePhysicianDetailController : ApiController
    {
        private readonly IPrimaryCarePhysicianDetailServices PrimaryCarePhysicianDetailServices = DependencyFactory.GetInstance<IPrimaryCarePhysicianDetailServices>();
        private readonly IConfigurationLib config = DependencyFactory.GetInstance<IConfigurationLib>();
        private readonly ICustomLog logger;

        public PrimaryCarePhysicianDetailController()
        {
            PrimaryCarePhysicianDetailServices = DependencyFactory.GetInstance<IPrimaryCarePhysicianDetailServices>();
            config = DependencyFactory.GetInstance<IConfigurationLib>();
            logger = DependencyFactory.GetInstance<ICustomLog>();
        }
        // GET: PrimaryCarePhysicianDetail
        //[Authorize]
        [Route("GetByFiltersToList")]
        [HttpPost]
        public EResponseBase<PrimaryCarePhysicianDetailCustomModel> GetByFiltersToList([FromBody] PrimaryCarePhysicianDetailRequestV1 request)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    logger.Print_Request(request);
                    EResponseBase<PrimaryCarePhysicianDetailCustomModel> responseJSON = PrimaryCarePhysicianDetailServices.GetByFiltersToList(request.PersonId, request.SpecialityId, request.PmgId, request.MunicipalityId);
                    logger.Print_Response(responseJSON);
                    EResponseBase<PrimaryCarePhysicianDetailCustomModel> response = Mapper.Map<EResponseBase<PrimaryCarePhysicianDetailCustomModel>>(responseJSON);
                    //if (response.listado != null) response.listado = response.listado.Any() ? response.listado.Take(100).ToList() : response.listado;//1198 max
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    return new UtilitariesResponse<PrimaryCarePhysicianDetailCustomModel>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }

        }

        private void ConfigureService()
        {
            PrimaryCarePhysicianDetailServices.Transaction = RequestUtility.GetHeaders().Transaction;
            PrimaryCarePhysicianDetailServices.Logger = logger;
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