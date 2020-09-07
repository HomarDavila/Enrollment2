using Common;
using Common.Logging;
using Common.Proxy;
using Core.API.Model;
using Core.API.Model.Request;
using Core.API.Model.Response;
using Domain.Custom_Models;
using EnrollmentSystemWebApp.Helpers;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Security.API.Model.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EnrollmentSystemWebApp.Proxy
{
    public class ProxyCoreAPI
    {
        public async Task<EResponseBase<McoResponseV1>> GetMcos(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token, bool showEnrollmentProcess)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                using (CustomProxyREST<McoResponseV1> service = new CustomProxyREST<McoResponseV1>(config))
                {
                    logger.Print_InitMethod();
                    logger.Info(string.Format("URL: {0} {1}",
                                           ProxyBase.GetURL(config.CoreAPI_UrlBase,
                                                            config.CoreAPI_ServicePreffix,
                                                            config.CoreAPI_McoController,
                                                            config.CoreAPI_Mco_Get,
                                                            showEnrollmentProcess
                                                            ),
                                           "(GET)"));
                    logger.Print_Request(null);
                    EResponseBase<McoResponseV1> response = await service.Get(config.CoreAPI_UrlBase,
                                                                           config.CoreAPI_ServicePreffix,
                                                                           config.CoreAPI_McoController,
                                                                           config.CoreAPI_Mco_Get,
                                                                           null,
                                                                           null,
                                                                           transaction.Id,
                                                                           AppConstants.Web,
                                                                           config.CoreAPI_IgnoreSSL,
                                                                           config.CoreAPI_Timeout,
                                                                           showEnrollmentProcess);
                    logger.Print_Response(response);
                    logger.Print_EndMethod();
                    return response;
                };
            }
        }

        public async Task<EResponseBase<MemberResponseV1>> GetPeopleById(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token, int applicationMemberID)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                using (CustomProxyREST<MemberResponseV1> service = new CustomProxyREST<MemberResponseV1>(config))
                {
                    logger.Print_InitMethod();
                    logger.Info(string.Format("URL: {0} {1}",
                                           ProxyBase.GetURL(config.CoreAPI_UrlBase,
                                                            config.CoreAPI_ServicePreffix,
                                                            config.CoreAPI_MemberController,
                                                            config.CoreAPI_Member_Get,
                                                            applicationMemberID
                                                            ),
                                           "(GET)"));
                    logger.Print_Request(null);
                    EResponseBase<MemberResponseV1> response = await service.Get(config.CoreAPI_UrlBase,
                                                                           config.CoreAPI_ServicePreffix,
                                                                           config.CoreAPI_MemberController,
                                                                           config.CoreAPI_Member_Get,
                                                                           null,
                                                                           null,
                                                                           transaction.Id,
                                                                           AppConstants.Web,
                                                                           config.CoreAPI_IgnoreSSL,
                                                                           config.CoreAPI_Timeout,
                                                                           applicationMemberID);
                    logger.Print_Response(response);
                    logger.Print_EndMethod();
                    return response;
                };
            }
        }

        public async Task<EResponseBase<EnrollmentHistoryResponseV2>> GetOnlyRejects(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                using (CustomProxyREST<EnrollmentHistoryResponseV2> service = new CustomProxyREST<EnrollmentHistoryResponseV2>(config))
                {
                    logger.Print_InitMethod();
                    logger.Info(string.Format("URL: {0} {1}",
                                           ProxyBase.GetURL(config.CoreAPI_UrlBase,
                                                            config.CoreAPI_ServicePreffix,
                                                            config.CoreAPI_MemberController,
                                                            config.CoreAPI_Member_Get
                                                            ),
                                           "(GET)"));
                    logger.Print_Request(null);
                    EResponseBase<EnrollmentHistoryResponseV2> response = await service.Get(config.CoreAPI_UrlBase,
                                                                           config.CoreAPI_ServicePreffix,
                                                                           config.CoreAPI_EnrollmentHistoryController,
                                                                           config.CoreAPI_EnrollmentHistory_GetOnlyRejects,
                                                                           null,
                                                                           null,
                                                                           transaction.Id,
                                                                           AppConstants.Web,
                                                                           config.CoreAPI_IgnoreSSL,
                                                                           config.CoreAPI_Timeout);
                    logger.Print_Response(response);
                    logger.Print_EndMethod();
                    return response;
                };
            }
        }
        
        public async Task<EResponseBase<MemberResponseV4>> GetEmailPeopleById(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token, int applicationMemberID)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                using (CustomProxyREST<MemberResponseV4> service = new CustomProxyREST<MemberResponseV4>(config))
                {
                    logger.Print_InitMethod();
                    logger.Info(string.Format("URL: {0} {1}",
                                           ProxyBase.GetURL(config.CoreAPI_UrlBase,
                                                            config.CoreAPI_ServicePreffix,
                                                            config.CoreAPI_MemberController,
                                                            config.CoreAPI_Member_GetEmail,
                                                            applicationMemberID
                                                            ),
                                           "(GET)"));
                    logger.Print_Request(null);
                    EResponseBase<MemberResponseV4> response = await service.Get(config.CoreAPI_UrlBase,
                                                                           config.CoreAPI_ServicePreffix,
                                                                           config.CoreAPI_MemberController,
                                                                           config.CoreAPI_Member_GetEmail,
                                                                           null,
                                                                           null,
                                                                           transaction.Id,
                                                                           AppConstants.Web,
                                                                           config.CoreAPI_IgnoreSSL,
                                                                           config.CoreAPI_Timeout,
                                                                           applicationMemberID);
                    logger.Print_Response(response);
                    logger.Print_EndMethod();
                    return response;
                };
            }
        }

        public async Task<EResponseBase<OverCapacityResponseV1>> GetAllOverCapacity(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                using (CustomProxyREST<OverCapacityResponseV1> service = new CustomProxyREST<OverCapacityResponseV1>(config))
                {
                    logger.Print_InitMethod();
                    logger.Info(string.Format("URL: {0} {1}",
                                           ProxyBase.GetURL(config.CoreAPI_UrlBase,
                                                            config.CoreAPI_ServicePreffix,
                                                            config.CoreAPI_OverCapacityController,
                                                            config.CoreAPI_OverCapacity_GetAllOvercapacity
                                                            ),
                                           "(GET)"));
                    logger.Print_Request(null);
                    EResponseBase<OverCapacityResponseV1> response = await service.Get(config.CoreAPI_UrlBase,
                                                                           config.CoreAPI_ServicePreffix,
                                                                           config.CoreAPI_OverCapacityController,
                                                                           config.CoreAPI_OverCapacity_GetAllOvercapacity,
                                                                           null,
                                                                           null,
                                                                           transaction.Id,
                                                                           AppConstants.Web,
                                                                           config.CoreAPI_IgnoreSSL,
                                                                           config.CoreAPI_Timeout
                                                                           );
                    logger.Print_Response(response);
                    logger.Print_EndMethod();
                    return response;
                };
            }
        }
        public async Task<EResponseBase<MemberResponseV6>> GetPeopleByFilters(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token, MemberRequestV2 request)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                using (CustomProxyREST<MemberRequestV2, MemberResponseV6> service = new CustomProxyREST<MemberRequestV2, MemberResponseV6>(config))
                {
                    logger.Print_InitMethod();
                    logger.Info(string.Format("URL: {0} {1}",
                                           ProxyBase.GetURL(config.CoreAPI_UrlBase,
                                                            config.CoreAPI_ServicePreffix,
                                                            config.CoreAPI_MemberController,
                                                            config.CoreAPI_Member_GetByFilters
                                                            ),
                                           "(GET)"));
                    logger.Print_Request(null);
                    EResponseBase<MemberResponseV6> response = await service.Post(config.CoreAPI_UrlBase,
                                                                           config.CoreAPI_ServicePreffix,
                                                                           config.CoreAPI_MemberController,
                                                                           config.CoreAPI_Member_GetByFilters,
                                                                           null,
                                                                           null,
                                                                           transaction.Id,
                                                                           AppConstants.Web,
                                                                           config.CoreAPI_IgnoreSSL,
                                                                           config.CoreAPI_Timeout,
                                                                           request);
                    logger.Print_Response(response);
                    logger.Print_EndMethod();
                    return response;
                };
            }
        }

        public async Task<EResponseBase<EnrollmentHistoryResponseV1>> GetHistoryByFilters(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token, MemberRequestV1 request)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                using (CustomProxyREST<MemberRequestV1, EnrollmentHistoryResponseV1> service = new CustomProxyREST<MemberRequestV1, EnrollmentHistoryResponseV1>(config))
                {
                    logger.Print_InitMethod();
                    logger.Info(string.Format("URL: {0} {1}",
                                           ProxyBase.GetURL(config.CoreAPI_UrlBase,
                                                            config.CoreAPI_ServicePreffix,
                                                            config.CoreAPI_MemberController,
                                                            config.CoreAPI_Member_GetEnrHistory
                                                            ),
                                           "(GET)"));
                    logger.Print_Request(null);
                    EResponseBase<EnrollmentHistoryResponseV1> response = await service.Post(config.CoreAPI_UrlBase,
                                                                           config.CoreAPI_ServicePreffix,
                                                                           config.CoreAPI_MemberController,
                                                                           config.CoreAPI_Member_GetEnrHistory,
                                                                           null,
                                                                           null,
                                                                           transaction.Id,
                                                                           AppConstants.Web,
                                                                           config.CoreAPI_IgnoreSSL,
                                                                           config.CoreAPI_Timeout,
                                                                           request);
                    logger.Print_Response(response);
                    logger.Print_EndMethod();
                    return response;
                };
            }
        }

        public async Task<EResponseBase<MemberResponseV1>> ChangePersonMco(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token, MemberRequestV1 request)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                EResponseBase<User_Response_v1> user = (EResponseBase<User_Response_v1>)HttpContext.Current.Session[config.SessionUser];
                switch (user.objeto.listRoles.ElementAt(0).Name)
                {
                    case AppConstants.RolCOUNSELOR:
                        request.Origin = DataSource.Counselor;
                        break;
                    case AppConstants.RolSELFTSERVICE:
                        request.Origin = DataSource.SelfService;
                        break;
                    case AppConstants.RolASSIST:
                        request.Origin = DataSource.Assist;
                        break;
                    case AppConstants.RolASES:
                        request.Origin = DataSource.Ases;
                        break;
                }
                using (CustomProxyREST<MemberRequestV1, MemberResponseV1> service = new CustomProxyREST<MemberRequestV1, MemberResponseV1>(config))
                {
                    logger.Print_InitMethod();
                    logger.Info(string.Format("URL: {0} {1}",
                                           ProxyBase.GetURL(config.CoreAPI_UrlBase,
                                                            config.CoreAPI_ServicePreffix,
                                                            config.CoreAPI_MemberController,
                                                            config.CoreAPI_Member_ChangeEnrollment
                                                            ),
                                           "(GET)"));
                    logger.Print_Request(null);
                    EResponseBase<MemberResponseV1> response = await service.Post(config.CoreAPI_UrlBase,
                                                                           config.CoreAPI_ServicePreffix,
                                                                           config.CoreAPI_MemberController,
                                                                           config.CoreAPI_Member_ChangeEnrollment,
                                                                           null,
                                                                           null,
                                                                           transaction.Id,
                                                                           AppConstants.Web,
                                                                           config.CoreAPI_IgnoreSSL,
                                                                           config.CoreAPI_Timeout,
                                                                           request);
                    logger.Print_Response(response);
                    logger.Print_EndMethod();
                    return response;
                };
            }
        }
        public async Task<EResponseBase<MemberResponseV1>> ChangePersonMcoReject(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token, MemberRequestV1 request)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                EResponseBase<User_Response_v1> user = (EResponseBase<User_Response_v1>)HttpContext.Current.Session[config.SessionUser];
                switch (user.objeto.listRoles.ElementAt(0).Name)
                {
                    case AppConstants.RolCOUNSELOR:
                        request.Origin = DataSource.Counselor;
                        break;
                    case AppConstants.RolSELFTSERVICE:
                        request.Origin = DataSource.SelfService;
                        break;
                    case AppConstants.RolASSIST:
                        request.Origin = DataSource.Assist;
                        break;
                    case AppConstants.RolASES:
                        request.Origin = DataSource.Ases;
                        break;
                }
                using (CustomProxyREST<MemberRequestV1, MemberResponseV1> service = new CustomProxyREST<MemberRequestV1, MemberResponseV1>(config))
                {
                    logger.Print_InitMethod();
                    logger.Info(string.Format("URL: {0} {1}",
                                           ProxyBase.GetURL(config.CoreAPI_UrlBase,
                                                            config.CoreAPI_ServicePreffix,
                                                            config.CoreAPI_MemberController,
                                                            config.CoreAPI_Member_ChangeEnrollment
                                                            ),
                                           "(GET)"));
                    logger.Print_Request(null);
                    EResponseBase<MemberResponseV1> response = await service.Post(config.CoreAPI_UrlBase,
                                                                           config.CoreAPI_ServicePreffix,
                                                                           config.CoreAPI_MemberController,
                                                                           config.CoreAPI_Member_ChangeEnrollmentReject,
                                                                           null,
                                                                           null,
                                                                           transaction.Id,
                                                                           AppConstants.Web,
                                                                           config.CoreAPI_IgnoreSSL,
                                                                           config.CoreAPI_Timeout,
                                                                           request);
                    logger.Print_Response(response);
                    logger.Print_EndMethod();
                    return response;
                };
            }
        }
        public async Task<EResponseBase<PDFResponseV1>> CreatePDF(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token, PcpPmgMcoRequestV1 request)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                using (CustomProxyREST<PcpPmgMcoRequestV1, PDFResponseV1> service = new CustomProxyREST<PcpPmgMcoRequestV1, PDFResponseV1>(config))
                {
                    logger.Print_InitMethod();
                    logger.Info(string.Format("URL: {0} {1}",
                                           ProxyBase.GetURL(config.CoreAPI_UrlBase,
                                                            config.CoreAPI_ServicePreffix,
                                                            config.CoreAPI_CommonController,
                                                            config.CoreAPI_Common_CreatePDF
                                                            ),
                                           "(GET)"));
                    logger.Print_Request(null);

                    EResponseBase<PDFResponseV1> response = await service.Post(config.CoreAPI_UrlBase,
                                                                           config.CoreAPI_ServicePreffix,
                                                                           config.CoreAPI_CommonController,
                                                                           config.CoreAPI_Common_CreatePDF,
                                                                           null,
                                                                           null,
                                                                           transaction.Id,
                                                                           AppConstants.Web,
                                                                           config.CoreAPI_IgnoreSSL,
                                                                           config.CoreAPI_Timeout,
                                                                           request);

                    logger.Print_Response(response);
                    logger.Print_EndMethod();
                    return response;
                };
            }
        }

        public async Task<EResponseBase<EnrollmentHistoryResponseV1>> SendSms(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token, int MemberId, string phone)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                using (CustomProxyREST<EnrollmentHistoryResponseV1> service = new CustomProxyREST<EnrollmentHistoryResponseV1>(config))
                {
                    logger.Print_InitMethod();
                    logger.Info(string.Format("URL: {0} {1}",
                                           ProxyBase.GetURL(config.CoreAPI_UrlBase,
                                                            config.CoreAPI_ServicePreffix,
                                                            config.CoreAPI_MemberController,
                                                            config.CoreAPI_Member_SendSms,
                                                            MemberId,
                                                            phone
                                                            ),
                                           "(GET)"));
                    logger.Print_Request(null);
                    EResponseBase<EnrollmentHistoryResponseV1> response = await service.Get(config.CoreAPI_UrlBase,
                                                                           config.CoreAPI_ServicePreffix,
                                                                           config.CoreAPI_MemberController,
                                                                           config.CoreAPI_Member_SendSms,
                                                                           null,
                                                                           null,
                                                                           transaction.Id,
                                                                           AppConstants.Web,
                                                                           config.CoreAPI_IgnoreSSL,
                                                                           config.CoreAPI_Timeout,
                                                                           MemberId,
                                                                           phone);
                    logger.Print_Response(response);
                    logger.Print_EndMethod();
                    return response;
                };
            }
        }
        public async Task<EResponseBase<PrimaryCarePhysicianCustomModel>> GetPcpWithFilters(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token, PcpRequestV1 request)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                using (CustomProxyREST<PcpRequestV1, PrimaryCarePhysicianCustomModel> service = new CustomProxyREST<PcpRequestV1, PrimaryCarePhysicianCustomModel>(config))
                {
                    logger.Print_InitMethod();
                    logger.Info(string.Format("URL: {0} {1}",
                                           ProxyBase.GetURL(config.CoreAPI_UrlBase,
                                                            config.CoreAPI_ServicePreffix,
                                                            config.CoreAPI_PcpController,
                                                            config.CoreAPI_Pcp_GetWithFilters
                                                            ),
                                           "(GET)"));
                    logger.Print_Request(null);
                    EResponseBase<PrimaryCarePhysicianCustomModel> response = await service.Post(config.CoreAPI_UrlBase,
                                                                           config.CoreAPI_ServicePreffix,
                                                                           config.CoreAPI_PcpController,
                                                                           config.CoreAPI_Pcp_GetWithFilters,
                                                                           null,
                                                                           null,
                                                                           transaction.Id,
                                                                           AppConstants.Web,
                                                                           config.CoreAPI_IgnoreSSL,
                                                                           config.CoreAPI_Timeout,
                                                                           request);
                    logger.Print_Response(response);
                    logger.Print_EndMethod();
                    return response;
                };
            }
        }

        public async Task<EResponseBase<PcpResponseV2>> GetPcpWithFiltersToList(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token, PcpRequestV1 request)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                using (CustomProxyREST<PcpRequestV1, PcpResponseV2> service = new CustomProxyREST<PcpRequestV1, PcpResponseV2>(config))
                {
                    logger.Print_InitMethod();
                    logger.Info(string.Format("URL: {0} {1}",
                                           ProxyBase.GetURL(config.CoreAPI_UrlBase,
                                                            config.CoreAPI_ServicePreffix,
                                                            config.CoreAPI_PcpController,
                                                            config.CoreAPI_Pcp_GetWithFiltersToList
                                                            ),
                                           "(GET)"));
                    logger.Print_Request(null);
                    EResponseBase<PcpResponseV2> response = await service.Post(config.CoreAPI_UrlBase,
                                                                           config.CoreAPI_ServicePreffix,
                                                                           config.CoreAPI_PcpController,
                                                                           config.CoreAPI_Pcp_GetWithFiltersToList,
                                                                           null,
                                                                           null,
                                                                           transaction.Id,
                                                                           AppConstants.Web,
                                                                           config.CoreAPI_IgnoreSSL,
                                                                           config.CoreAPI_Timeout,
                                                                           request);
                    logger.Print_Response(response);
                    logger.Print_EndMethod();
                    return response;
                };
            }
        }

        public async Task<EResponseBase<PrimaryCarePhysicianDetailCustomModel>> GetPrimaryCarePhysicianDetailWithFiltersToList(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token, PrimaryCarePhysicianDetailRequestV1 request)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                using (CustomProxyREST<PrimaryCarePhysicianDetailRequestV1, PrimaryCarePhysicianDetailCustomModel> service = new CustomProxyREST<PrimaryCarePhysicianDetailRequestV1, PrimaryCarePhysicianDetailCustomModel>(config))
                {
                    logger.Print_InitMethod();
                    logger.Info(string.Format("URL: {0} {1}",
                                           ProxyBase.GetURL(config.CoreAPI_UrlBase,
                                                            config.CoreAPI_ServicePreffix,
                                                            config.CoreAPI_PrimaryCarePhysicianDetailController,
                                                            config.CoreAPI_PrimaryCarePhysicianDetail_GetWithFiltersToList
                                                            ),
                                           "(GET)"));
                    logger.Print_Request(null);
                    EResponseBase<PrimaryCarePhysicianDetailCustomModel> response = await service.Post(config.CoreAPI_UrlBase,
                                                                           config.CoreAPI_ServicePreffix,
                                                                           config.CoreAPI_PrimaryCarePhysicianDetailController,
                                                                           config.CoreAPI_PrimaryCarePhysicianDetail_GetWithFiltersToList,
                                                                           null,
                                                                           null,
                                                                           transaction.Id,
                                                                           AppConstants.Web,
                                                                           config.CoreAPI_IgnoreSSL,
                                                                           config.CoreAPI_Timeout,
                                                                           request);
                    logger.Print_Response(response);
                    logger.Print_EndMethod();
                    return response;
                };
            }
        }

        public async Task<EResponseBase<EnrollmentResponseV2>> ChangePersonMcoEnabled(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token, int MemberId)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                using (CustomProxyREST<EnrollmentResponseV2> service = new CustomProxyREST<EnrollmentResponseV2>(config))
                {
                    logger.Print_InitMethod();
                    logger.Info(string.Format("URL: {0} {1}",
                                           ProxyBase.GetURL(config.CoreAPI_UrlBase,
                                                            config.CoreAPI_ServicePreffix,
                                                            config.CoreAPI_MemberController,
                                                            config.CoreAPI_Member_ChangePersonMcoEnabled
                                                            ),
                                           "(GET)"));
                    logger.Print_Request(null);
                    EResponseBase<EnrollmentResponseV2> response = await service.Get(config.CoreAPI_UrlBase,
                                                                           config.CoreAPI_ServicePreffix,
                                                                           config.CoreAPI_MemberController,
                                                                           config.CoreAPI_Member_ChangePersonMcoEnabled,
                                                                           null,
                                                                           null,
                                                                           transaction.Id,
                                                                           AppConstants.Web,
                                                                           config.CoreAPI_IgnoreSSL,
                                                                           config.CoreAPI_Timeout,
                                                                           MemberId);
                    logger.Print_Response(response);
                    logger.Print_EndMethod();
                    return response;
                };
            }
        }

        public async Task<EResponseBase<EnrollmentResponseV2>> ChangeEnrollmentEnabledJustCause(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token, int MemberId)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                using (CustomProxyREST<EnrollmentResponseV2> service = new CustomProxyREST<EnrollmentResponseV2>(config))
                {
                    logger.Print_InitMethod();
                    logger.Info(string.Format("URL: {0} {1}",
                                           ProxyBase.GetURL(config.CoreAPI_UrlBase,
                                                            config.CoreAPI_ServicePreffix,
                                                            config.CoreAPI_MemberController,
                                                            config.CoreAPI_Member_ChangeEnrollmentEnabledJustCause
                                                            ),
                                           "(GET)"));
                    logger.Print_Request(null);
                    EResponseBase<EnrollmentResponseV2> response = await service.Get(config.CoreAPI_UrlBase,
                                                                           config.CoreAPI_ServicePreffix,
                                                                           config.CoreAPI_MemberController,
                                                                           config.CoreAPI_Member_ChangeEnrollmentEnabledJustCause,
                                                                           null,
                                                                           null,
                                                                           transaction.Id,
                                                                           AppConstants.Web,
                                                                           config.CoreAPI_IgnoreSSL,
                                                                           config.CoreAPI_Timeout,
                                                                           MemberId);
                    logger.Print_Response(response);
                    logger.Print_EndMethod();
                    return response;
                };
            }
        }

        public async Task<EResponseBase<SpecialityResponseV1>> GetSpecialities(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                using (CustomProxyREST<SpecialityResponseV1> service = new CustomProxyREST<SpecialityResponseV1>(config))
                {
                    logger.Print_InitMethod();
                    logger.Info(string.Format("URL: {0} {1}",
                                           ProxyBase.GetURL(config.CoreAPI_UrlBase,
                                                            config.CoreAPI_ServicePreffix,
                                                            config.CoreAPI_SpecialityController,
                                                            config.CoreAPI_Speciality_Get
                                                            ),
                                           "(GET)"));
                    logger.Print_Request(null);
                    EResponseBase<SpecialityResponseV1> response = await service.Get(config.CoreAPI_UrlBase,
                                                                           config.CoreAPI_ServicePreffix,
                                                                           config.CoreAPI_SpecialityController,
                                                                           config.CoreAPI_Speciality_Get,
                                                                           null,
                                                                           null,
                                                                           transaction.Id,
                                                                           AppConstants.Web,
                                                                           config.CoreAPI_IgnoreSSL,
                                                                           config.CoreAPI_Timeout,
                                                                           true);
                    logger.Print_Response(response);
                    logger.Print_EndMethod();
                    return response;
                };
            }
        }

        public async Task<EResponseBase<SpecialityResponseV1>> PostSpecialitiesPCPId(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token, PmgRequestV1 request)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                using (CustomProxyREST<PmgRequestV1, SpecialityResponseV1> service = new CustomProxyREST<PmgRequestV1, SpecialityResponseV1>(config))
                {
                    logger.Print_InitMethod();
                    logger.Info(string.Format("URL: {0} {1}",
                                           ProxyBase.GetURL(config.CoreAPI_UrlBase,
                                                            config.CoreAPI_ServicePreffix,
                                                            config.CoreAPI_SpecialityController,
                                                            config.CoreAPI_Speciality_GetByPCPId,
                                                            true
                                                            ),
                                           "(GET)"));
                    logger.Print_Request(null);
                    EResponseBase<SpecialityResponseV1> response = await service.Post(config.CoreAPI_UrlBase,
                                                                           config.CoreAPI_ServicePreffix,
                                                                           config.CoreAPI_SpecialityController,
                                                                           config.CoreAPI_Speciality_GetByPCPId,
                                                                           null,
                                                                           null,
                                                                           transaction.Id,
                                                                           AppConstants.Web,
                                                                           config.CoreAPI_IgnoreSSL,
                                                                           config.CoreAPI_Timeout,
                                                                           request);
                    logger.Print_Response(response);
                    logger.Print_EndMethod();
                    return response;
                };
            }
        }

        public async Task<EResponseBase<PmgResponseV1>> PostAllPmgPCP(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token, PmgRequestV1 request)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                using (CustomProxyREST<PmgRequestV1, PmgResponseV1> service = new CustomProxyREST<PmgRequestV1, PmgResponseV1>(config))
                {
                    logger.Print_InitMethod();
                    logger.Info(string.Format("URL: {0} {1}",
                                           ProxyBase.GetURL(config.CoreAPI_UrlBase,
                                                            config.CoreAPI_ServicePreffix,
                                                            config.CoreAPI_PrimaryMedicalGroupController,
                                                            config.CoreAPI_PrimaryMedicalGroup_GetByPCPId,
                                                            true
                                                            ),
                                           "(GET)"));
                    logger.Print_Request(null);
                    EResponseBase<PmgResponseV1> response = await service.Post(config.CoreAPI_UrlBase,
                                                                           config.CoreAPI_ServicePreffix,
                                                                           config.CoreAPI_PrimaryMedicalGroupController,
                                                                           config.CoreAPI_PrimaryMedicalGroup_GetByPCPId,
                                                                           null,
                                                                           null,
                                                                           transaction.Id,
                                                                           AppConstants.Web,
                                                                           config.CoreAPI_IgnoreSSL,
                                                                           config.CoreAPI_Timeout,
                                                                           request);
                    logger.Print_Response(response);
                    logger.Print_EndMethod();
                    return response;
                };
            }
        }

        public async Task<EResponseBase<ReasonJustCauseResponseV1>> GetReasonJustCause(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                using (CustomProxyREST<ReasonJustCauseResponseV1> service = new CustomProxyREST<ReasonJustCauseResponseV1>(config))
                {
                    logger.Print_InitMethod();
                    logger.Info(string.Format("URL: {0} {1}",
                                           ProxyBase.GetURL(config.CoreAPI_UrlBase,
                                                            config.CoreAPI_ServicePreffix,
                                                            config.CoreAPI_ReasonJustCauseController,
                                                            config.CoreAPI_ReasonJustCause_Get
                                                            ),
                                           "(GET)"));
                    logger.Print_Request(null);
                    EResponseBase<ReasonJustCauseResponseV1> response = await service.Get(config.CoreAPI_UrlBase,
                                                                           config.CoreAPI_ServicePreffix,
                                                                           config.CoreAPI_ReasonJustCauseController,
                                                                           config.CoreAPI_ReasonJustCause_Get,
                                                                           null,
                                                                           null,
                                                                           transaction.Id,
                                                                           AppConstants.Web,
                                                                           config.CoreAPI_IgnoreSSL,
                                                                           config.CoreAPI_Timeout);
                    logger.Print_Response(response);
                    logger.Print_EndMethod();
                    return response;
                };
            }
        }

        public async Task<EResponseBase<MunicipalityResponseV1>> GetMunicipalitys(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                using (CustomProxyREST<MunicipalityResponseV1> service = new CustomProxyREST<MunicipalityResponseV1>(config))
                {
                    logger.Print_InitMethod();
                    logger.Info(string.Format("URL: {0} {1}",
                                           ProxyBase.GetURL(config.CoreAPI_UrlBase,
                                                            config.CoreAPI_ServicePreffix,
                                                            config.CoreAPI_MunicipalityController,
                                                            config.CoreAPI_Municipality_Get,
                                                            true
                                                            ),
                                           "(GET)"));
                    logger.Print_Request(null);
                    EResponseBase<MunicipalityResponseV1> response = await service.Get(config.CoreAPI_UrlBase,
                                                                           config.CoreAPI_ServicePreffix,
                                                                           config.CoreAPI_MunicipalityController,
                                                                           config.CoreAPI_Municipality_Get,
                                                                           null,
                                                                           null,
                                                                           transaction.Id,
                                                                           AppConstants.Web,
                                                                           config.CoreAPI_IgnoreSSL,
                                                                           config.CoreAPI_Timeout);
                    logger.Print_Response(response);
                    logger.Print_EndMethod();
                    return response;
                };
            }
        }

        public async Task<EResponseBase<ReasonJustCauseResponseV1>> GetReasonJustCauseById(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token, int ReasonJustCauseID)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                using (CustomProxyREST<ReasonJustCauseResponseV1> service = new CustomProxyREST<ReasonJustCauseResponseV1>(config))
                {
                    logger.Print_InitMethod();
                    logger.Info(string.Format("URL: {0} {1}",
                                           ProxyBase.GetURL(config.CoreAPI_UrlBase,
                                                            config.CoreAPI_ServicePreffix,
                                                            config.CoreAPI_ReasonJustCauseController,
                                                            config.CoreAPI_ReasonJustCause_GetReasonJustCauseById,
                                                            ReasonJustCauseID
                                                            ),
                                           "(GET)"));
                    logger.Print_Request(null);
                    EResponseBase<ReasonJustCauseResponseV1> response = await service.Get(config.CoreAPI_UrlBase,
                                                                           config.CoreAPI_ServicePreffix,
                                                                           config.CoreAPI_ReasonJustCauseController,
                                                                           config.CoreAPI_ReasonJustCause_GetReasonJustCauseById,
                                                                           null,
                                                                           null,
                                                                           transaction.Id,
                                                                           AppConstants.Web,
                                                                           config.CoreAPI_IgnoreSSL,
                                                                           config.CoreAPI_Timeout,
                                                                           ReasonJustCauseID);
                    logger.Print_Response(response);
                    logger.Print_EndMethod();
                    return response;
                };
            }
        }

        public async Task<EResponseBase<PersonPcpResponseV1>> GetPcps(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                using (CustomProxyREST<PersonPcpResponseV1> service = new CustomProxyREST<PersonPcpResponseV1>(config))
                {
                    logger.Print_InitMethod();
                    logger.Info(string.Format("URL: {0} {1}",
                                           ProxyBase.GetURL(config.CoreAPI_UrlBase,
                                                            config.CoreAPI_ServicePreffix,
                                                            config.CoreAPI_PcpController,
                                                            config.CoreAPI_Pcp_Get
                                                            ),
                                           "(GET)"));
                    logger.Print_Request(null);
                    EResponseBase<PersonPcpResponseV1> response = await service.Get(config.CoreAPI_UrlBase,
                                                                           config.CoreAPI_ServicePreffix,
                                                                           config.CoreAPI_PcpController,
                                                                           config.CoreAPI_Pcp_Get,
                                                                           null,
                                                                           null,
                                                                           transaction.Id,
                                                                           AppConstants.Web,
                                                                           config.CoreAPI_IgnoreSSL,
                                                                           config.CoreAPI_Timeout,
                                                                           true);
                    logger.Print_Response(response);
                    logger.Print_EndMethod();
                    return response;
                };
            }
        }

        public async Task<EResponseBase<PmgResponseV1>> GetPmgs(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token, bool ShowForChangeEnrollmentProcess)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                using (CustomProxyREST<PmgResponseV1> service = new CustomProxyREST<PmgResponseV1>(config))
                {
                    logger.Print_InitMethod();
                    logger.Info(string.Format("URL: {0} {1}",
                                           ProxyBase.GetURL(config.CoreAPI_UrlBase,
                                                            config.CoreAPI_ServicePreffix,
                                                            config.CoreAPI_PmgController,
                                                            config.CoreAPI_Pmg_Get,
                                                            ShowForChangeEnrollmentProcess
                                                            ),
                                           "(GET)"));
                    logger.Print_Request(null);
                    EResponseBase<PmgResponseV1> response = await service.Get(config.CoreAPI_UrlBase,
                                                                           config.CoreAPI_ServicePreffix,
                                                                           config.CoreAPI_PmgController,
                                                                           config.CoreAPI_Pmg_Get,
                                                                           null,
                                                                           null,
                                                                           transaction.Id,
                                                                           AppConstants.Web,
                                                                           config.CoreAPI_IgnoreSSL,
                                                                           config.CoreAPI_Timeout,
                                                                           ShowForChangeEnrollmentProcess);
                    logger.Print_Response(response);
                    logger.Print_EndMethod();
                    return response;
                };
            }
        }

        public async Task<EResponseBase<EnrollmentHistoryResponseV1>> GetEnrollmentHistoryByPersonId(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token, int MemberId)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                using (CustomProxyREST<EnrollmentHistoryResponseV1> service = new CustomProxyREST<EnrollmentHistoryResponseV1>(config))
                {
                    logger.Print_InitMethod();
                    logger.Info(string.Format("URL: {0} {1}",
                                           ProxyBase.GetURL(config.CoreAPI_UrlBase,
                                                            config.CoreAPI_ServicePreffix,
                                                            config.CoreAPI_MemberController,
                                                            config.CoreAPI_Member_GetEnrollmentHistory,
                                                            MemberId
                                                            ),
                                           "(GET)"));
                    logger.Print_Request(null);
                    EResponseBase<EnrollmentHistoryResponseV1> response = await service.Get(config.CoreAPI_UrlBase,
                                                                           config.CoreAPI_ServicePreffix,
                                                                           config.CoreAPI_MemberController,
                                                                           config.CoreAPI_Member_GetEnrollmentHistory,
                                                                           null,
                                                                           null,
                                                                           transaction.Id,
                                                                           AppConstants.Web,
                                                                           config.CoreAPI_IgnoreSSL,
                                                                           config.CoreAPI_Timeout,
                                                                           MemberId);
                    logger.Print_Response(response);
                    logger.Print_EndMethod();
                    return response;
                };
            }
        }

        public async Task<EResponseBase<EnrollmentHistoryResponseV1>> GetEnrollmentHistory(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token, MemberRequestV1 request)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                using (CustomProxyREST<MemberRequestV1, EnrollmentHistoryResponseV1> service = new CustomProxyREST<MemberRequestV1, EnrollmentHistoryResponseV1>(config))
                {
                    logger.Print_InitMethod();
                    logger.Info(string.Format("URL: {0} {1}",
                                           ProxyBase.GetURL(config.CoreAPI_UrlBase,
                                                            config.CoreAPI_ServicePreffix,
                                                            config.CoreAPI_MemberController,
                                                            config.CoreAPI_Member_GetEnrHistory
                                                            ),
                                           "(GET)"));
                    logger.Print_Request(null);
                    EResponseBase<EnrollmentHistoryResponseV1> response = await service.Post(config.CoreAPI_UrlBase,
                                                                           config.CoreAPI_ServicePreffix,
                                                                           config.CoreAPI_MemberController,
                                                                           config.CoreAPI_Member_GetEnrHistory,
                                                                           null,
                                                                           null,
                                                                           transaction.Id,
                                                                           AppConstants.Web,
                                                                           config.CoreAPI_IgnoreSSL,
                                                                           config.CoreAPI_Timeout,
                                                                           request);
                    logger.Print_Response(response);
                    logger.Print_EndMethod();
                    return response;
                };

            }
        }

        public async Task<EResponseBase<EnrollmentHistoryResponseV1>> SendStatistics(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token, MemberRequestV1 request)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                using (CustomProxyREST<MemberRequestV1, EnrollmentHistoryResponseV1> service = new CustomProxyREST<MemberRequestV1, EnrollmentHistoryResponseV1>(config))
                {
                    logger.Print_InitMethod();
                    logger.Info(string.Format("URL: {0} {1}",
                                           ProxyBase.GetURL(config.CoreAPI_UrlBase,
                                                            config.CoreAPI_ServicePreffix,
                                                            config.CoreAPI_MemberController,
                                                            config.CoreAPI_Member_SendEnrPeriod
                                                            ),
                                           "(GET)"));
                    logger.Print_Request(null);
                    EResponseBase<EnrollmentHistoryResponseV1> response = await service.Post(config.CoreAPI_UrlBase,
                                                                           config.CoreAPI_ServicePreffix,
                                                                           config.CoreAPI_MemberController,
                                                                           config.CoreAPI_Member_SendEnrPeriod,
                                                                           null,
                                                                           null,
                                                                           transaction.Id,
                                                                           AppConstants.Web,
                                                                           config.CoreAPI_IgnoreSSL,
                                                                           config.CoreAPI_Timeout,
                                                                           request);
                    logger.Print_Response(response);
                    logger.Print_EndMethod();
                    return response;
                };

            }
        }
        public async Task<EResponseBase<EnrollmentPeriodResponseV1>> GetEnrollmentPeriod(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token, MemberRequestV1 request)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                using (CustomProxyREST<MemberRequestV1, EnrollmentPeriodResponseV1> service = new CustomProxyREST<MemberRequestV1, EnrollmentPeriodResponseV1>(config))
                {
                    logger.Print_InitMethod();
                    logger.Info(string.Format("URL: {0} {1}",
                                           ProxyBase.GetURL(config.CoreAPI_UrlBase,
                                                            config.CoreAPI_ServicePreffix,
                                                            config.CoreAPI_MemberController,
                                                            config.CoreAPI_Member_GetEnrPeriod
                                                            ),
                                           "(GET)"));
                    logger.Print_Request(null);
                    EResponseBase<EnrollmentPeriodResponseV1> response = await service.Post(config.CoreAPI_UrlBase,
                                                                           config.CoreAPI_ServicePreffix,
                                                                           config.CoreAPI_MemberController,
                                                                           config.CoreAPI_Member_GetEnrPeriod,
                                                                           null,
                                                                           null,
                                                                           transaction.Id,
                                                                           AppConstants.Web,
                                                                           config.CoreAPI_IgnoreSSL,
                                                                           config.CoreAPI_Timeout,
                                                                           request);
                    logger.Print_Response(response);
                    logger.Print_EndMethod();
                    return response;
                };

            }
        }

        public async Task<EResponseBase<EnrollmentPeriodResponseV1>> GetEnrollmentStatistics(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token, MemberRequestV1 request)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                using (CustomProxyREST<MemberRequestV1, EnrollmentPeriodResponseV1> service = new CustomProxyREST<MemberRequestV1, EnrollmentPeriodResponseV1>(config))
                {
                    logger.Print_InitMethod();
                    logger.Info(string.Format("URL: {0} {1}",
                                           ProxyBase.GetURL(config.CoreAPI_UrlBase,
                                                            config.CoreAPI_ServicePreffix,
                                                            config.CoreAPI_MemberController,
                                                            config.CoreAPI_Member_GetEnrPeriod
                                                            ),
                                           "(GET)"));
                    logger.Print_Request(null);
                    EResponseBase<EnrollmentPeriodResponseV1> response = await service.Post(config.CoreAPI_UrlBase,
                                                                           config.CoreAPI_ServicePreffix,
                                                                           config.CoreAPI_MemberController,
                                                                           config.CoreAPI_Member_GetEnrPeriod,
                                                                           null,
                                                                           null,
                                                                           transaction.Id,
                                                                           AppConstants.Web,
                                                                           config.CoreAPI_IgnoreSSL,
                                                                           config.CoreAPI_Timeout,
                                                                           request);
                    logger.Print_Response(response);
                    logger.Print_EndMethod();
                    return response;
                };

            }
        }

        public async Task<EResponseBase<EnrollmentHistoryResponseV1>> SendEnrollmentPeriod(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token, MemberRequestV1 request)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                using (CustomProxyREST<MemberRequestV1, EnrollmentHistoryResponseV1> service = new CustomProxyREST<MemberRequestV1, EnrollmentHistoryResponseV1>(config))
                {
                    logger.Print_InitMethod();
                    logger.Info(string.Format("URL: {0} {1}",
                                           ProxyBase.GetURL(config.CoreAPI_UrlBase,
                                                            config.CoreAPI_ServicePreffix,
                                                            config.CoreAPI_MemberController,
                                                            config.CoreAPI_Member_SendEnrPeriod
                                                            ),
                                           "(GET)"));
                    logger.Print_Request(null);
                    EResponseBase<EnrollmentHistoryResponseV1> response = await service.Post(config.CoreAPI_UrlBase,
                                                                           config.CoreAPI_ServicePreffix,
                                                                           config.CoreAPI_MemberController,
                                                                           config.CoreAPI_Member_SendEnrPeriod,
                                                                           null,
                                                                           null,
                                                                           transaction.Id,
                                                                           AppConstants.Web,
                                                                           config.CoreAPI_IgnoreSSL,
                                                                           config.CoreAPI_Timeout,
                                                                           request);
                    logger.Print_Response(response);
                    logger.Print_EndMethod();
                    return response;
                };

            }
        }



        public async Task<EResponseBase<FileResponseV1>> GetFile(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token, int FileId)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                using (CustomProxyREST<FileResponseV1> service = new CustomProxyREST<FileResponseV1>(config))
                {
                    logger.Print_InitMethod();
                    logger.Info(string.Format("URL: {0} {1}",
                                           ProxyBase.GetURL(config.CoreAPI_UrlBase,
                                                            config.CoreAPI_ServicePreffix,
                                                            config.CoreAPI_FileController,
                                                            config.CoreAPI_File_GetFile,
                                                            FileId
                                                            ),
                                           "(GET)"));
                    logger.Print_Request(null);
                    EResponseBase<FileResponseV1> response = await service.Get(config.CoreAPI_UrlBase,
                                                                           config.CoreAPI_ServicePreffix,
                                                                           config.CoreAPI_FileController,
                                                                           config.CoreAPI_File_GetFile,
                                                                           null,
                                                                           null,
                                                                           transaction.Id,
                                                                           AppConstants.Web,
                                                                           config.CoreAPI_IgnoreSSL,
                                                                           config.CoreAPI_Timeout,
                                                                           FileId);
                    //Byte[] bytes = File.ReadAllBytes(string.Concat(((List<FileResponseV1>)response.listado)[0].Path, ((List<FileResponseV1>)response.listado)[0].Name));
                    //((List<FileResponseV1>)response.listado)[0].Content = Convert.ToBase64String(bytes);
                    logger.Print_Response(response);
                    logger.Print_EndMethod();
                    return response;
                };
            }
        }

        public async Task<EResponseBase<FileResponseV1>> GetPDF(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token, FileRequestV1 request)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                using (CustomProxyREST<FileRequestV1, FileResponseV1> service = new CustomProxyREST<FileRequestV1, FileResponseV1>(config))
                {
                    logger.Print_InitMethod();
                    logger.Info(string.Format("URL: {0} {1}",
                                           ProxyBase.GetURL(config.CoreAPI_UrlBase,
                                                            config.CoreAPI_ServicePreffix,
                                                            config.CoreAPI_FileController,
                                                            config.CoreAPI_File_GetPDF
                                                            ),
                                           "(GET)"));
                    logger.Print_Request(null);
                    EResponseBase<FileResponseV1> response = await service.Post(config.CoreAPI_UrlBase,
                                                            config.CoreAPI_ServicePreffix,
                                                            config.CoreAPI_FileController,
                                                            config.CoreAPI_File_GetPDF,
                                                            null,
                                                            null,
                                                            transaction.Id,
                                                            AppConstants.Web,
                                                            config.CoreAPI_IgnoreSSL,
                                                            config.CoreAPI_Timeout,
                                                            request);
                    logger.Print_Response(response);
                    logger.Print_EndMethod();
                    return response;
                };
            }
        }

        public async Task<EResponseBase<FileResponseV1>> SetPDF(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token, FileRequestV1 request)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                using (CustomProxyREST<FileRequestV1, FileResponseV1> service = new CustomProxyREST<FileRequestV1, FileResponseV1>(config))
                {
                    logger.Print_InitMethod();
                    logger.Info(string.Format("URL: {0} {1}",
                                           ProxyBase.GetURL(config.CoreAPI_UrlBase,
                                                            config.CoreAPI_ServicePreffix,
                                                            config.CoreAPI_FileController,
                                                            config.CoreAPI_File_SetPDF
                                                            ),
                                           "(GET)"));
                    logger.Print_Request(null);
                    EResponseBase<FileResponseV1> response = await service.Post(config.CoreAPI_UrlBase,
                                                            config.CoreAPI_ServicePreffix,
                                                            config.CoreAPI_FileController,
                                                            config.CoreAPI_File_SetPDF,
                                                            null,
                                                            null,
                                                            transaction.Id,
                                                            AppConstants.Web,
                                                            config.CoreAPI_IgnoreSSL,
                                                            config.CoreAPI_Timeout,
                                                            request);
                    logger.Print_Response(response);
                    logger.Print_EndMethod();
                    return response;
                };
            }
        }

        public async Task<EResponseBase<FileResponseV1>> GetEnrollmentFiles(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token, int MemberId)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                using (CustomProxyREST<FileResponseV1> service = new CustomProxyREST<FileResponseV1>(config))
                {
                    logger.Print_InitMethod();
                    logger.Info(string.Format("URL: {0} {1}",
                                           ProxyBase.GetURL(config.CoreAPI_UrlBase,
                                                            config.CoreAPI_ServicePreffix,
                                                            config.CoreAPI_FileController,
                                                            config.CoreAPI_File_GetEnrollmentFiles,
                                                            MemberId
                                                            ),
                                           "(GET)"));
                    logger.Print_Request(null);
                    EResponseBase<FileResponseV1> response = await service.Get(config.CoreAPI_UrlBase,
                                                                           config.CoreAPI_ServicePreffix,
                                                                           config.CoreAPI_FileController,
                                                                           config.CoreAPI_File_GetEnrollmentFiles,
                                                                           null,
                                                                           null,
                                                                           transaction.Id,
                                                                           AppConstants.Web,
                                                                           config.CoreAPI_IgnoreSSL,
                                                                           config.CoreAPI_Timeout,
                                                                           MemberId);
                    logger.Print_Response(response);
                    logger.Print_EndMethod();
                    return response;
                };
            }
        }

        public async Task<EResponseBase<FileResponseV1>> DisabledEnrollmentFiles(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token, FileRequestV1 request)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                using (CustomProxyREST<FileRequestV1, FileResponseV1> service = new CustomProxyREST<FileRequestV1, FileResponseV1>(config))
                {
                    logger.Print_InitMethod();
                    logger.Info(string.Format("URL: {0} {1}",
                                           ProxyBase.GetURL(config.CoreAPI_UrlBase,
                                                            config.CoreAPI_ServicePreffix,
                                                            config.CoreAPI_FileController,
                                                            config.CoreAPI_File_DisabledEnrollmentFiles
                                                            ),
                                           "(GET)"));
                    logger.Print_Request(null);
                    EResponseBase<FileResponseV1> response = await service.Post(config.CoreAPI_UrlBase,
                                                                           config.CoreAPI_ServicePreffix,
                                                                           config.CoreAPI_FileController,
                                                                           config.CoreAPI_File_DisabledEnrollmentFiles,
                                                                           null,
                                                                           null,
                                                                           transaction.Id,
                                                                           AppConstants.Web,
                                                                           config.CoreAPI_IgnoreSSL,
                                                                           config.CoreAPI_Timeout,
                                                                           request);
                    logger.Print_Response(response);
                    logger.Print_EndMethod();
                    return response;
                };
            }
        }

        public async Task<EResponseBase<FileResponseV1>> SetEnrollmentFiles(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token, FileRequestV1 request)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                using (CustomProxyREST<FileRequestV1, FileResponseV1> service = new CustomProxyREST<FileRequestV1, FileResponseV1>(config))
                {
                    logger.Print_InitMethod();
                    logger.Info(string.Format("URL: {0} {1}",
                                           ProxyBase.GetURL(config.CoreAPI_UrlBase,
                                                            config.CoreAPI_ServicePreffix,
                                                            config.CoreAPI_FileController,
                                                            config.CoreAPI_File_SetEnrollmentFiles
                                                            ),
                                           "(GET)"));
                    logger.Print_Request(null);
                    EResponseBase<FileResponseV1> response = await service.Post(config.CoreAPI_UrlBase,
                                                                           config.CoreAPI_ServicePreffix,
                                                                           config.CoreAPI_FileController,
                                                                           config.CoreAPI_File_SetEnrollmentFiles,
                                                                           null,
                                                                           null,
                                                                           transaction.Id,
                                                                           AppConstants.Web,
                                                                           config.CoreAPI_IgnoreSSL,
                                                                           config.CoreAPI_Timeout,
                                                                           request);
                    //File.WriteAllBytes(string.Concat(request.Path, request.Name), Convert.FromBase64String(request.Content.Substring(request.Content.LastIndexOf(",") + 1)));
                    //try
                    //{
                    //}
                    //catch (Exception ex)
                    //{
                    //    logger.Print_Response(ex);
                    //}
                    logger.Print_Response(response);
                    logger.Print_EndMethod();
                    return response;
                };
            }
        }

        public async Task<EResponseBase<StatisticsResponseV1>> GetReportsWeb(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token, StatisticsRequestV1 request)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                using (CustomProxyREST<StatisticsRequestV1, StatisticsResponseV1> service = new CustomProxyREST<StatisticsRequestV1, StatisticsResponseV1>(config))
                {
                    logger.Print_InitMethod();
                    logger.Info(string.Format("URL: {0} {1}",
                                           ProxyBase.GetURL(config.CoreAPI_UrlBase,
                                                            config.CoreAPI_ServicePreffix,
                                                            config.CoreAPI_ReportsController,
                                                            config.CoreAPI_Reports_GetReportsWeb
                                                            ),
                                           "(GET)"));
                    logger.Print_Request(null);
                    EResponseBase<StatisticsResponseV1> response = await service.Post(config.CoreAPI_UrlBase,
                                                                           config.CoreAPI_ServicePreffix,
                                                                           config.CoreAPI_ReportsController,
                                                                           config.CoreAPI_Reports_GetReportsWeb,
                                                                           null,
                                                                           null,
                                                                           transaction.Id,
                                                                           AppConstants.Web,
                                                                           config.CoreAPI_IgnoreSSL,
                                                                           config.CoreAPI_Timeout,
                                                                           request);
                    logger.Print_Response(response);
                    logger.Print_EndMethod();
                    return response;
                };

            }
        }

        public async Task<EResponseBase<StatisticsResponseV1>> InsertStatistic(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token, StatisticsRequestV1 request)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                using (CustomProxyREST<StatisticsRequestV1, StatisticsResponseV1> service = new CustomProxyREST<StatisticsRequestV1, StatisticsResponseV1>(config))
                {
                    logger.Print_InitMethod();
                    logger.Info(string.Format("URL: {0} {1}",
                                           ProxyBase.GetURL(config.CoreAPI_UrlBase,
                                                            config.CoreAPI_ServicePreffix,
                                                            config.CoreAPI_ReportsController,
                                                            config.CoreAPI_Reports_InsertStatistic
                                                            ),
                                           "(GET)"));
                    logger.Print_Request(null);
                    EResponseBase<StatisticsResponseV1> response = await service.Post(config.CoreAPI_UrlBase,
                                                                           config.CoreAPI_ServicePreffix,
                                                                           config.CoreAPI_ReportsController,
                                                                           config.CoreAPI_Reports_InsertStatistic,
                                                                           null,
                                                                           null,
                                                                           transaction.Id,
                                                                           AppConstants.Web,
                                                                           config.CoreAPI_IgnoreSSL,
                                                                           config.CoreAPI_Timeout,
                                                                           request);
                    logger.Print_Response(response);
                    logger.Print_EndMethod();
                    return response;
                };

            }
        }

        public async Task<EResponseBase<ReportResponseV1>> GetReportInscripciones(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token, ReportRequestV1 request)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                using (CustomProxyREST<ReportRequestV1, ReportResponseV1> service = new CustomProxyREST<ReportRequestV1, ReportResponseV1>(config))
                {
                    logger.Print_InitMethod();
                    logger.Info(string.Format("URL: {0} {1}",
                                           ProxyBase.GetURL(config.CoreAPI_UrlBase,
                                                            config.CoreAPI_ServicePreffix,
                                                            config.CoreAPI_ReportController,
                                                            config.CoreAPI_Report_GetReportInscripciones
                                                            ),
                                           "(GET)"));
                    logger.Print_Request(null);
                    EResponseBase<ReportResponseV1> response = await service.Post(config.CoreAPI_UrlBase,
                                                                           config.CoreAPI_ServicePreffix,
                                                                           config.CoreAPI_ReportController,
                                                                           config.CoreAPI_Report_GetReportInscripciones,
                                                                           null,
                                                                           null,
                                                                           transaction.Id,
                                                                           AppConstants.Web,
                                                                           config.CoreAPI_IgnoreSSL,
                                                                           config.CoreAPI_Timeout,
                                                                           request);
                    logger.Print_Response(response);
                    logger.Print_EndMethod();
                    return response;
                };
            }
        }

        public async Task<EResponseBase<ReportResponseV1>> GetReportJustaCausa(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token, ReportRequestV1 request)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                using (CustomProxyREST<ReportRequestV1, ReportResponseV1> service = new CustomProxyREST<ReportRequestV1, ReportResponseV1>(config))
                {
                    logger.Print_InitMethod();
                    logger.Info(string.Format("URL: {0} {1}",
                                           ProxyBase.GetURL(config.CoreAPI_UrlBase,
                                                            config.CoreAPI_ServicePreffix,
                                                            config.CoreAPI_ReportController,
                                                            config.CoreAPI_Report_GetReportJustaCausa
                                                            ),
                                           "(GET)"));
                    logger.Print_Request(null);
                    EResponseBase<ReportResponseV1> response = await service.Post(config.CoreAPI_UrlBase,
                                                                           config.CoreAPI_ServicePreffix,
                                                                           config.CoreAPI_ReportController,
                                                                           config.CoreAPI_Report_GetReportJustaCausa,
                                                                           null,
                                                                           null,
                                                                           transaction.Id,
                                                                           AppConstants.Web,
                                                                           config.CoreAPI_IgnoreSSL,
                                                                           config.CoreAPI_Timeout,
                                                                           request);
                    logger.Print_Response(response);
                    logger.Print_EndMethod();
                    return response;
                };
            }
        }
    }
}