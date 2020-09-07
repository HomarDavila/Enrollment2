using AutoMapper;
using Common;
using Common.Generic.HttpHelpers;
using Common.Logging;
using Core.API.Helpers;
using Core.API.Model.Request;
using Core.API.Model.Response;
using Domain.Entity_Models;
using Service.DependecyInjection;
using Service.Interfaces;
using System;
using System.Web.Http;

namespace Core.API.Controllers
{
    //[AuditApi(EventTypeName = "{controller}/{action} ({verb})", IncludeResponseBody = true, IncludeRequestBody = true, IncludeModelState = true)]
    [RoutePrefix("api/ReasonJustCause/v1")]
    public class ReasonJustCauseController : ApiController
    {
        private readonly IReasonJustCauseServices ReasonJustCauseServices = DependencyFactory.GetInstance<IReasonJustCauseServices>();
        private readonly IConfigurationLib config = DependencyFactory.GetInstance<IConfigurationLib>();
        private readonly ICustomLog logger;
        // GET: ReasonJustCause
        public ReasonJustCauseController()
        {
            ReasonJustCauseServices = DependencyFactory.GetInstance<IReasonJustCauseServices>();
            config = DependencyFactory.GetInstance<IConfigurationLib>();
            logger = DependencyFactory.GetInstance<ICustomLog>();
        }

        //[Authorize]
        [Route("Get")]
        [HttpGet]
        public EResponseBase<ReasonJustCauseResponseV1> Get()
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    logger.Print_Request(null);
                    EResponseBase<ReasonJustCause> responseJSON = ReasonJustCauseServices.Get();
                    logger.Print_Response(responseJSON);
                    EResponseBase<ReasonJustCauseResponseV1> response = Mapper.Map<EResponseBase<ReasonJustCauseResponseV1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    return new UtilitariesResponse<ReasonJustCauseResponseV1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        //[Authorize]
        [Route("GetById/{Id}")]
        [HttpGet]
        public EResponseBase<ReasonJustCauseResponseV1> GetById(int Id)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    logger.Print_Request(Id);
                    EResponseBase<ReasonJustCause> responseJSON = ReasonJustCauseServices.GetReasonJustCauseByID(Id);
                    logger.Print_Response(responseJSON);
                    EResponseBase<ReasonJustCauseResponseV1> response = Mapper.Map<EResponseBase<ReasonJustCauseResponseV1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<ReasonJustCauseResponseV1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        //[Authorize]
        [Route("Disabled")]
        [HttpPost]
        public EResponseBase<ReasonJustCauseResponseV1> Disabled([FromBody] ReasonJustCauseRequestV1 request)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    logger.Print_Request(request);
                    EResponseBase<ReasonJustCause> responseJSON = ReasonJustCauseServices.Disabled(request.Id, request.Enabled);
                    logger.Print_Response(responseJSON);
                    EResponseBase<ReasonJustCauseResponseV1> response = Mapper.Map<EResponseBase<ReasonJustCauseResponseV1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<ReasonJustCauseResponseV1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        //[Authorize]
        [Route("InsertOrUpdate")]
        [HttpPost]
        public EResponseBase<ReasonJustCauseResponseV1> InsertOrUpdate([FromBody] ReasonJustCauseRequestV1 request)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    logger.Print_Request(request);
                    ReasonJustCause oReasonJustCause = new ReasonJustCause
                    {
                        Id = request.Id,
                        Reason = request.Reason,
                        Description = request.Description,
                        CreatedBy = request.CreatedBy,
                        CreatedOn = request.CreatedOn,
                        Enabled = request.Enabled
                    };
                    EResponseBase<ReasonJustCause> responseJSON = ReasonJustCauseServices.InsertOrUpdate(oReasonJustCause);
                    logger.Print_Response(responseJSON);
                    EResponseBase<ReasonJustCauseResponseV1> response = Mapper.Map<EResponseBase<ReasonJustCauseResponseV1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<ReasonJustCauseResponseV1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        private void ConfigureService()
        {
            ReasonJustCauseServices.Transaction = RequestUtility.GetHeaders().Transaction;
            ReasonJustCauseServices.Logger = logger;
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