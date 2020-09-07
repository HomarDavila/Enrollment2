using Audit.WebApi;
using AutoMapper;
using Common;
using Common.Generic.HttpHelpers;
using Common.HttpHelpers;
using Common.Logging;
using Core.API.Helpers;
using Domain.Custom_Models;
using Newtonsoft.Json;
using Service.DependecyInjection;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Core.API.Controllers
{
    //[AuditApi(EventTypeName = "{controller}/{action} ({verb})", IncludeResponseBody = true, IncludeRequestBody = true, IncludeModelState = true)]
    [RoutePrefix("api/OverCapacity/v1")]
    public class OverCapacityController : ApiController
    {
        private readonly IPcpServices pcpServices = DependencyFactory.GetInstance<IPcpServices>();
        private readonly IPmgServices pmgServices = DependencyFactory.GetInstance<IPmgServices>();
        private readonly IMcoServices mcoServices = DependencyFactory.GetInstance<IMcoServices>();
        private readonly IConfigurationLib config = DependencyFactory.GetInstance<IConfigurationLib>();
        private readonly ICustomLog logger;

        public OverCapacityController()
        {
            pcpServices = DependencyFactory.GetInstance<IPcpServices>();
            pmgServices = DependencyFactory.GetInstance<IPmgServices>();
            mcoServices = DependencyFactory.GetInstance<IMcoServices>();
            config = DependencyFactory.GetInstance<IConfigurationLib>();
            logger = DependencyFactory.GetInstance<ICustomLog>();
        }

        //[Authorize]
        [Route("GetAllOvercapacity")]
        [HttpGet]
        public EResponseBase<OverCapacityResponseV1> Get()
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                OverCapacityResponseV1 result = new OverCapacityResponseV1();
                try
                {
                    logger.Print_Request(null);
                    EResponseBase<Domain.Entity_Models.PersonPrimaryCarePhysician> responseJSON_Pcp = pcpServices.Get(true);
                    logger.Print_Response("Response Pcp:" + responseJSON_Pcp);
                    EResponseBase<Domain.Entity_Models.PrimaryMedicalGroup> responseJSON_Pmg = pmgServices.Get(true);
                    logger.Print_Response("Response Pmg:" + responseJSON_Pmg);
                    EResponseBase<Domain.Entity_Models.ManagedCareOrganization> responseJSON_Mco = mcoServices.Get(false);
                    logger.Print_Response("Response Mco:" + responseJSON_Mco);

                    result.lstMcoOverCapacity = new List<OverCapacityResponseV1.McoOverCapacity>();
                    foreach (Domain.Entity_Models.ManagedCareOrganization item in responseJSON_Mco.listado)
                    {
                        result.lstMcoOverCapacity.Add(new OverCapacityResponseV1.McoOverCapacity()
                        {
                            IdMco = item.Id,
                            OverCapacityMco = item.OverCapacity
                        });
                    }

                    result.lstPmgOverCapacity = new List<OverCapacityResponseV1.PmgOverCapacity>();
                    foreach (Domain.Entity_Models.PrimaryMedicalGroup item in responseJSON_Pmg.listado)
                    {
                        result.lstPmgOverCapacity.Add(new OverCapacityResponseV1.PmgOverCapacity()
                        {
                            IdPmg = item.Id,
                            OverCapacityPmg = item.OverCapacity
                        });
                    }

                    result.lstPcpOverCapacity = new List<OverCapacityResponseV1.PcpOverCapacity>();
                    foreach (Domain.Entity_Models.PersonPrimaryCarePhysician item in responseJSON_Pcp.listado)
                    {
                        result.lstPcpOverCapacity.Add(new OverCapacityResponseV1.PcpOverCapacity()
                        {
                            IdPcp = item.PcpId,
                            OverCapacityPcp = item.OverCapacity
                        });
                    }
                    logger.Print_Response(result);
                    return new UtilitariesResponse<OverCapacityResponseV1>(config).setResponseBaseForObj(result);
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    return new UtilitariesResponse<OverCapacityResponseV1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        private void ConfigureService()
        {
            mcoServices.Transaction = RequestUtility.GetHeaders().Transaction;
            mcoServices.Logger = logger;
            pcpServices.Transaction = RequestUtility.GetHeaders().Transaction;
            pcpServices.Logger = logger;
            pmgServices.Transaction = RequestUtility.GetHeaders().Transaction;
            pmgServices.Logger = logger;
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
