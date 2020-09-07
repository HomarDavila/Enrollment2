using Common;
using Common.HttpHelpers;
using Common.Logging;
using Common.Proxy;
using Core.API.Model;
using Core.API.Model.Request;
using Core.API.Model.Response;
using Domain.Custom_Models;
using EnrrolmentSelfServicesWebApp.Helpers;
using System;
using System.Threading.Tasks;

namespace EnrrolmentSelfServicesWebApp.Proxy
{
    public class ProxyCoreAPI
    {
        public async Task<EResponseBase<McoResponseV1>> GetMcos(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token)
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
                                                            config.CoreAPI_Mco_Get
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
                                                                           config.CoreAPI_Timeout);
                    logger.Print_Response(response);
                    logger.Print_EndMethod();
                    return response;
                };
            }
        }

        public async Task<EResponseBase<MemberResponseV2>> GetPeopleByFilters(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token, MemberRequestV2 request)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                using (CustomProxyREST<MemberRequestV2, MemberResponseV2> service = new CustomProxyREST<MemberRequestV2, MemberResponseV2>(config))
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
                    EResponseBase<MemberResponseV2> response = await service.Post(config.CoreAPI_UrlBase,
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

        public async Task<EResponseBase<MemberResponseV2>> GetMembersById(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token, int MemberId)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                using (CustomProxyREST<MemberResponseV2> service = new CustomProxyREST<MemberResponseV2>(config))
                {
                    logger.Print_InitMethod();
                    logger.Info(string.Format("URL: {0} {1}",
                                           ProxyBase.GetURL(config.CoreAPI_UrlBase,
                                                            config.CoreAPI_ServicePreffix,
                                                            config.CoreAPI_MemberController,
                                                            config.CoreAPI_Member_GetMembersById
                                                            ),
                                           "(GET)"));
                    logger.Print_Request(null);
                    EResponseBase<MemberResponseV2> response = await service.Get(config.CoreAPI_UrlBase,
                                                                           config.CoreAPI_ServicePreffix,
                                                                           config.CoreAPI_MemberController,
                                                                           config.CoreAPI_Member_GetMembersById,
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

        public async Task<EResponseBase<MemberResponseV1>> ChangePersonMco(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token, MemberRequestV1 request)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
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

        public async Task<EResponseBase<PcpResponseV1>> GetPcpWithFilters(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token, PcpRequestV1 request)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                using (CustomProxyREST<PcpRequestV1, PcpResponseV1> service = new CustomProxyREST<PcpRequestV1, PcpResponseV1>(config))
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
                    EResponseBase<PcpResponseV1> response = await service.Post(config.CoreAPI_UrlBase,
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

        public async Task<EResponseBase<SpecialityResponseV1>> GetSpecialities(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token, bool ShowForChangeEnrollmentProcess)
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
                                                            config.CoreAPI_Speciality_Get,
                                                            true
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
                                                                           ShowForChangeEnrollmentProcess);
                    logger.Print_Response(response);
                    logger.Print_EndMethod();
                    return response;
                };
            }
        }

        public async Task<EResponseBase<SpecialityResponseV1>> GetSpecialitiesPCPId(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token, PmgRequestV1 request)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                //using (CustomProxyREST<SpecialityResponseV1> service = new CustomProxyREST<SpecialityResponseV1>(config))
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

        public async Task<EResponseBase<PmgResponseV1>> GetAllPmgPCP(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token, PmgRequestV1 request)
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
                                                            config.CoreAPI_Pcp_Get,
                                                            true
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

        public async Task<EResponseBase<ManagedCareOrganizationResponseV1>> GetManagedCareOrganizations(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token)
        {
            using (log4net.NDC.Push(RequestHelpers.AuditUserData()))
            {
                using (CustomProxyREST<ManagedCareOrganizationResponseV1> service = new CustomProxyREST<ManagedCareOrganizationResponseV1>(config))
                {
                    logger.Print_InitMethod();
                    logger.Info(string.Format("URL: {0} {1}",
                                           ProxyBase.GetURL(config.CoreAPI_UrlBase,
                                                            config.CoreAPI_ServicePreffix,
                                                            config.CoreAPI_ManagedCareOrganizationController,
                                                            config.CoreAPI_ManagedCareOrganization_Get,
                                                            true
                                                            ),
                                           "(GET)"));
                    logger.Print_Request(null);
                    EResponseBase<ManagedCareOrganizationResponseV1> response = await service.Get(config.CoreAPI_UrlBase,
                                                                           config.CoreAPI_ServicePreffix,
                                                                           config.CoreAPI_ManagedCareOrganizationController,
                                                                           config.CoreAPI_ManagedCareOrganization_Get,
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
                                                            config.CoreAPI_Pmg_Get
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

        public async Task<EResponseBase<EnrollmentPeriodResponseV1>> GetEnrPeriod(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token, MemberRequestV1 request)
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

        public async Task<EResponseBase<EnrollmentHistoryResponseV1>> SendSms(Transaction transaction, ICustomLog logger, CustomConfigurationLib config, string token, int MemberId,string phone)
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
    }
}