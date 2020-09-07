using Audit.WebApi;
using AutoMapper;
using Common;
using Common.Generic.HttpHelpers;
using Common.Logging;
using Core.API.Helpers;
using Core.API.Model.Response;
using Domain.Custom_Models;
using Domain.Entity_Models;
using Service.DependecyInjection;
using Service.Helpers;
using Service.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace Core.API.Controllers
{
    //[AuditApi(EventTypeName = "{controller}/{action} ({verb})", IncludeResponseBody = true, IncludeRequestBody = true, IncludeModelState = true)]
    [RoutePrefix("api/EnrollmentHistory/v1")]
    public class EnrollmentHistoryController : ApiController
    {
        private readonly IEnrollmentHistoryServices EnrollmentHistoryServices = DependencyFactory.GetInstance<IEnrollmentHistoryServices>();
        private readonly IConfigurationLib config = DependencyFactory.GetInstance<IConfigurationLib>();
        private readonly ICustomLog logger;

        public EnrollmentHistoryController()
        {
            EnrollmentHistoryServices = DependencyFactory.GetInstance<IEnrollmentHistoryServices>();
            config = DependencyFactory.GetInstance<IConfigurationLib>();
            logger = DependencyFactory.GetInstance<ICustomLog>();
        }

        //[Authorize]
        [Route("GetOnlyRejects")]
        [HttpGet]
        public EResponseBase<EnrollmentHistoryResponseV2> GetOnlyRejects()
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    logger.Print_Request(null);
                    EResponseBase<Domain.Entity_Models.EnrollmentHistory> responseJSON = EnrollmentHistoryServices.GetOnlyRejects();
                    logger.Print_Response(responseJSON);
                    EResponseBase<EnrollmentHistoryResponseV2> response = Mapper.Map<EResponseBase<EnrollmentHistoryResponseV2>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<EnrollmentHistoryResponseV2>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        //[Authorize]
        [Route("ChangeEnrollmentForCorrection")]
        [HttpPost]
        public EResponseBase<EnrollmentResponseV1> ChangeEnrollmentForCorrection([FromBody] EnrollmentHistoryRequestV1 request)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                bool blnOk = true;
                EResponseBase<EnrollmentResponseV1> result = new EResponseBase<EnrollmentResponseV1>();
                try
                {
                    logger.Print_Request(request);
                    result.Code = 0;
                    result.Message = "Ok";
                    result.MessageEN = "Ok";
                    //if (!request.IsJustCause)
                    //{
                    //    EResponseBase<EnrollmentPeriod> responseJSON = EnrollmentHistoryServices.GetEnrPeriod();
                    //    logger.Print_Response(responseJSON);
                    //    blnOk = (bool)responseJSON.objeto.Enabled;
                    //    if (!blnOk)
                    //    {
                    //        result.Code = CustomConfigurationLib.CodigoTimeOffEnrrollment;
                    //        result.Message = CustomConfigurationLib.MensajeTimeOffEnrrollmentES;
                    //        result.MessageEN = CustomConfigurationLib.MensajeTimeOffEnrrollmentEN;
                    //    }
                    //}

                    //if (blnOk)
                    //{
                        EResponseBase<Member> responseJSON2 = EnrollmentHistoryServices.ChangeEnrollmentForCorrection(request.MemberId, request.McoId, request.PmgId, request.PcpId, request.PpcpId, request.Permission, request.JustCause, request.Origin, request.UserName, request.EnrollmentHistoryId, request.IgnoreValidationRules);                      
                        logger.Print_Response(responseJSON2);
                    result.objeto = new EnrollmentResponseV1
                    {
                        MemberId = responseJSON2.objeto.Id,
                        EnrollmentHistoryID=responseJSON2.objeto.EnrollmentHistoryID
                    };
                        result.Code = responseJSON2.Code;
                        result.Message = responseJSON2.Message;
                        result.MessageEN = responseJSON2.MessageEN;
                    //}
                    //EResponseBase<MemberResponseV1> response = Mapper.Map<EResponseBase<MemberResponseV1>>(responseJSON);
                    return result;
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    return new UtilitariesResponse<EnrollmentResponseV1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        private void ConfigureService()
        {
            EnrollmentHistoryServices.Transaction = RequestUtility.GetHeaders().Transaction;
            EnrollmentHistoryServices.Logger = logger;
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
