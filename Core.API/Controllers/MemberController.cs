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
    [RoutePrefix("api/Member/v1")]
    public class MemberController : ApiController
    {
        private readonly IMemberServices memberServices = DependencyFactory.GetInstance<IMemberServices>();
        private readonly IConfigurationLib config = DependencyFactory.GetInstance<IConfigurationLib>();
        private readonly ICustomLog logger;

        public MemberController()
        {
            memberServices = DependencyFactory.GetInstance<IMemberServices>();
            config = DependencyFactory.GetInstance<IConfigurationLib>();
            logger = DependencyFactory.GetInstance<ICustomLog>();
        }

        //[Authorize]
        [Route("Get/{MemberId}")]
        [HttpGet]
        public EResponseBase<MemberResponseV1> Get(int MemberId)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    logger.Print_Request(MemberId);
                    EResponseBase<Domain.Entity_Models.Member> responseJSON = memberServices.Get(MemberId);
                    logger.Print_Response(responseJSON);
                    EResponseBase<MemberResponseV1> response = Mapper.Map<EResponseBase<MemberResponseV1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<MemberResponseV1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        //[Authorize]
        [Route("GetEmail/{MemberId}")]
        [HttpGet]
        public EResponseBase<MemberResponseV4> GetEmail(int MemberId)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    logger.Print_Request(MemberId);
                    EResponseBase<MemberCustomModel> responseJSON = memberServices.GetEmail(MemberId);
                    logger.Print_Response(responseJSON);
                    EResponseBase<MemberResponseV4> response = Mapper.Map<EResponseBase<MemberResponseV4>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<MemberResponseV4>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        //[Authorize]
        [Route("GetByFilters")]
        [HttpPost]
        public EResponseBase<MemberResponseV6> Get([FromBody] MemberRequestV2 request)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    logger.Print_Request(request);
                    EResponseBase<MemberCustomModelV2> responseJSON = memberServices.Get(request.MPI, request.Last4SSN, request.DateOfBirth, request.FirstName, request.FirtLastName, request.SecondLastName);
                    logger.Print_Response(responseJSON);
                    EResponseBase<MemberResponseV6> response = Mapper.Map<EResponseBase<MemberResponseV6>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<MemberResponseV6>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        //[Authorize]
        [Route("GetMembersById/{MemberId}")]
        [HttpGet]
        public EResponseBase<MemberResponseV2> GetMembersById(int MemberId)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    logger.Print_Request(MemberId);
                    EResponseBase<Domain.Entity_Models.Member> responseJSON = memberServices.GetMembersById(MemberId);
                    logger.Print_Response(responseJSON);
                    EResponseBase<MemberResponseV2> response = Mapper.Map<EResponseBase<MemberResponseV2>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<MemberResponseV2>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        [Route("ExistUser")]
        [HttpPost]
        public EResponseBase<MemberResponseV3> ExistUser([FromBody] MemberRequestV3 request)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    logger.Print_Request(request);
                    EResponseBase<Domain.Entity_Models.Member> responseTemp = memberServices.Get(request.Last4SSN, request.DateOfBirth, request.ZipCode, request.FirstLastName, request.SecondLastName, request.FirstName);
                    EResponseBase<MemberResponseV3> memberResponseV3 = new EResponseBase<MemberResponseV3>();
                    if (responseTemp.Code == config.CodigoExito)
                    {
                        if (responseTemp.listado.Any())
                        {
                            MemberResponseV3 memberResponseV3T = new MemberResponseV3()
                            {
                                CountOfMembers = responseTemp.listado.Count(),
                                Id = responseTemp.listado.FirstOrDefault().Id,
                                MPI = responseTemp.listado.FirstOrDefault().MPI,
                                MPIShort = responseTemp.listado.FirstOrDefault().MPIShort,
                                MCOId = responseTemp.listado.FirstOrDefault().MCOId ?? 0,
                                PMGId = responseTemp.listado.FirstOrDefault().PMGId ?? 0,
                                PCPId = responseTemp.listado.FirstOrDefault().PCPId ?? 0
                            };
                            memberResponseV3 = new UtilitariesResponse<MemberResponseV3>(config).setResponseBaseForObj(memberResponseV3T);
                        }
                        else
                        {
                            memberResponseV3 = new UtilitariesResponse<MemberResponseV3>(config).setResponseBaseForNoDataFound();
                        }
                    }
                    else
                    {
                        memberResponseV3.Code = responseTemp.Code;
                        memberResponseV3.Message = responseTemp.Message;
                        memberResponseV3.MessageEN = responseTemp.MessageEN;
                    }
                    logger.Print_Response(memberResponseV3);
                    return memberResponseV3;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<MemberResponseV3>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        //[Authorize]
        [Route("ChangeEnrollment")]
        [HttpPost]
        public EResponseBase<EnrollmentResponseV1> ChangeEnrollment([FromBody] MemberRequestV1 request)
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
                    if (!request.IsJustCause)
                    {
                        EResponseBase<EnrollmentPeriod> responseJSON = memberServices.GetEnrPeriod();
                        logger.Print_Response(responseJSON);
                        blnOk = (bool)responseJSON.objeto.Enabled;
                        if (!blnOk)
                        {
                            result.Code = CustomConfigurationLib.CodigoTimeOffEnrrollment;
                            result.Message = CustomConfigurationLib.MensajeTimeOffEnrrollmentES;
                            result.MessageEN = CustomConfigurationLib.MensajeTimeOffEnrrollmentEN;
                        }
                    }

                    //if (blnOk)
                    //{
                        EResponseBase<Member> responseJSON2 = memberServices.ChangeEnrollment(request.MemberId, request.McoId, request.PmgId, request.PcpId, request.PpcpId, request.Permission, request.JustCause, request.Origin, request.UserName, request.JustCauseComment,request.Phone,request.Email, request.IgnoreValidationRules);                      
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
        [Route("ChangeEnrollmentReject")]
        [HttpPost]
        public EResponseBase<EnrollmentResponseV1> ChangeEnrollmenteject([FromBody] MemberRequestV1 request)
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
                    //    EResponseBase<EnrollmentPeriod> responseJSON = memberServices.GetEnrPeriod();
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
                    EResponseBase<Member> responseJSON2 = memberServices.ChangeEnrollmentReject(request.MemberId, request.Idhistories, request.IdStatus, request.McoId, request.PmgId, request.PcpId, request.PpcpId, request.Permission, request.JustCause, request.Origin, request.UserName, request.JustCauseComment, request.IgnoreValidationRules);
                    logger.Print_Response(responseJSON2);
                    result.objeto = new EnrollmentResponseV1
                    {
                        MemberId = responseJSON2.objeto.Id,
                        EnrollmentHistoryID = responseJSON2.objeto.EnrollmentHistoryID
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

        //[Authorize]
        [Route("ChangePersonMcoEnabled/{MemberId}")]
        [HttpGet]
        public EResponseBase<EnrollmentResponseV2> ChangePersonMcoEnabled(int MemberId)
        {
            CustomHeader header = ConfigureLogHeader();
            bool ignoreValidationRules = false;
            bool isAvailableForChange = true;
            string reason = string.Empty;
            string reasonEN = string.Empty;
            int code = 0;
            bool isconsuler = false;
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    logger.Print_Request(MemberId);
                    EResponseBase<EnrollmentPeriod> responseJSON = memberServices.GetEnrPeriod();
                    logger.Print_Response(responseJSON);
                    //if (Convert.ToBoolean(responseJSON.objeto.Enabled))
                    //{
                    var result= memberServices.ChangeEnrollmentEnabled(MemberId, Convert.ToBoolean(responseJSON.objeto.Enabled), ignoreValidationRules, Convert.ToDateTime(responseJSON.objeto.PeriodIni), Convert.ToDateTime(responseJSON.objeto.PeriodFin), ref isAvailableForChange, ref reason, ref reasonEN, ref code, isconsuler);
                    responseJSON.objeto.Enabled = result;
                    responseJSON.IsOK = result;
                    responseJSON.Code = code;
                    responseJSON.Message = reason;
                    responseJSON.MessageEN = reasonEN;
                    
                    //}
                    logger.Print_Response(responseJSON);
                    EResponseBase<EnrollmentResponseV2> response = Mapper.Map<EResponseBase<EnrollmentResponseV2>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<EnrollmentResponseV2>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        [Route("ChangeEnrollmentEnabledJustCause/{MemberId}")]
        [HttpGet]
        public EResponseBase<EnrollmentResponseV2> ChangeEnrollmentEnabledJustCause(int MemberId)
        {
            CustomHeader header = ConfigureLogHeader();
            bool isAvailableForChange = true;
            string reason = string.Empty;
            string reasonEN = string.Empty;
            int code = 0;
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    logger.Print_Request(MemberId);
                    EResponseBase<EnrollmentPeriod> responseJSON = memberServices.GetEnrPeriod();
                    logger.Print_Response(responseJSON);
                    var result = memberServices.ChangeEnrollmentEnabledJustCause(MemberId, ref isAvailableForChange, ref reason, ref reasonEN, ref code);
                    responseJSON.objeto.Enabled = result;
                    responseJSON.IsOK = result;
                    responseJSON.Code = code;
                    responseJSON.Message = reason;
                    responseJSON.MessageEN = reasonEN;

                    logger.Print_Response(responseJSON);
                    EResponseBase<EnrollmentResponseV2> response = Mapper.Map<EResponseBase<EnrollmentResponseV2>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex);
                    return new UtilitariesResponse<EnrollmentResponseV2>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        //[Authorize]
        [Route("GetEnrollmentHistory/{MemberId}")]
        [HttpGet]
        public EResponseBase<EnrollmentHistoryResponseV1> GetEnrollmentChangeHistory(int MemberId)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    logger.Print_Request(MemberId);
                    EResponseBase<Domain.Entity_Models.EnrollmentHistory> responseJSON = memberServices.GetEnrollmentChangeHistory(MemberId);
                    logger.Print_Response(responseJSON);
                    EResponseBase<EnrollmentHistoryResponseV1> response = Mapper.Map<EResponseBase<EnrollmentHistoryResponseV1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    return new UtilitariesResponse<EnrollmentHistoryResponseV1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        [Route("SendSms/{MemberId}/{phone}")]
        [HttpGet]
        public async Task<EResponseBase<EnrollmentHistoryResponseV1>> SendSms(int MemberId,string phone)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    logger.Print_Request(MemberId);
                    EResponseBase<Domain.Entity_Models.EnrollmentHistory> responseJSON = await memberServices.SendSms(MemberId,phone);
                    logger.Print_Response(responseJSON);
                    EResponseBase<EnrollmentHistoryResponseV1> response = Mapper.Map<EResponseBase<EnrollmentHistoryResponseV1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex.StackTrace);
                    return new UtilitariesResponse<EnrollmentHistoryResponseV1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }
        }

        [Route("GetEnrHistory")]
        [HttpPost]
        public EResponseBase<EnrollmentHistoryResponseV1> GetEnrollmentHistory([FromBody] MemberRequestV1 request)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    logger.Print_Request(request);
                    EResponseBase<Domain.Entity_Models.EnrollmentHistory> responseJSON = memberServices.GetEnrollmentHistory(request.fecini, request.fecfin);
                    logger.Print_Response(responseJSON);
                    EResponseBase<EnrollmentHistoryResponseV1> response = Mapper.Map<EResponseBase<EnrollmentHistoryResponseV1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    return new UtilitariesResponse<EnrollmentHistoryResponseV1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }

        }


        [Route("GetEnrPeriod")]
        [HttpPost]
        public EResponseBase<EnrollmentPeriodResponseV1> GetEnrPeriod([FromBody] MemberRequestV1 request)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    logger.Print_Request(request);
                    EResponseBase<Domain.Entity_Models.EnrollmentPeriod> responseJSON = memberServices.GetEnrPeriod();
                    logger.Print_Response(responseJSON);
                    EResponseBase<EnrollmentPeriodResponseV1> response = Mapper.Map<EResponseBase<EnrollmentPeriodResponseV1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    return new UtilitariesResponse<EnrollmentPeriodResponseV1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }

        }

        [Route("SendEnrPeriod")]
        [HttpPost]
        public EResponseBase<EnrollmentPeriodResponseV1> SendEnrPeriod([FromBody] MemberRequestV1 request)
        {
            CustomHeader header = ConfigureLogHeader();
            using (log4net.NDC.Push(RequestHelpers.AuditUserData(header)))
            {
                logger.Print_InitMethod();
                ConfigureService();
                try
                {
                    logger.Print_Request(request);
                    EResponseBase<Domain.Entity_Models.EnrollmentPeriod> responseJSON = memberServices.SendEnrPeriod(request.fecini, request.fecfin);
                    logger.Print_Response(responseJSON);
                    EResponseBase<EnrollmentPeriodResponseV1> response = Mapper.Map<EResponseBase<EnrollmentPeriodResponseV1>>(responseJSON);
                    return response;
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    return new UtilitariesResponse<EnrollmentPeriodResponseV1>(config).setResponseBaseForException(ex);
                }
                finally
                {
                    logger.Print_EndMethod();
                }
            }

        }

        private void ConfigureService()
        {
            memberServices.Transaction = RequestUtility.GetHeaders().Transaction;
            memberServices.Logger = logger;
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
