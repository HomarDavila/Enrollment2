using Common;
using Common.Logging;
using Domain.Custom_Models;
using Domain.Entity_Models;
using Infraestructure.Context;
using Infraestructure.Repositories;
using Mehdime.Entity;
using Service.Helpers;
using Service.Interfaces;
using Sms;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Service.Implementations
{
    public class EnrollmentHistoryServices : IEnrollmentHistoryServices
    {
        private readonly IDbContextScopeFactory dbContextScopeFactory;
        private readonly IRepository<Member, ApplicationDbContext> repository;
        private readonly IRepository<EnrollmentHistory, ApplicationDbContext> repository2;
        private readonly IRepository<EnrollmentPeriod, ApplicationDbContext> repository3;
        private readonly IRepository<PrimaryMedicalGroup, ApplicationDbContext> repository4;
        private readonly IConfigurationLib config;
        public ITransaction Transaction { get; set; }
        public ICustomLog Logger { get; set; }

        public EnrollmentHistoryServices(IDbContextScopeFactory _dbContextScopeFactory,
                              IRepository<Member, ApplicationDbContext> _repository,
                              IRepository<EnrollmentHistory, ApplicationDbContext> _repository2,
                              IRepository<EnrollmentPeriod, ApplicationDbContext> _repository3,
                              IRepository<PrimaryMedicalGroup, ApplicationDbContext> _repository4,
                              IConfigurationLib _config)
        {
            dbContextScopeFactory = _dbContextScopeFactory;
            repository2 = _repository2;
            repository3 = _repository3;
            repository4 = _repository4;
            repository = _repository;
            config = _config;

        }

        public EResponseBase<EnrollmentHistory> Get()
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<EnrollmentHistory> result = new EResponseBase<EnrollmentHistory>();
            try
            {
                using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(null, printDebug: true);
                    result = repository2.Find(x => x.Enabled == true);
                    Logger.Print_Response(result, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<EnrollmentHistory>(config).setResponseBaseForException(e);
                Logger.Error(e);
            }
            return result;
        }

        public EResponseBase<EnrollmentHistory> Get(int EnrollmentHistoryId)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<EnrollmentHistory> result = new EResponseBase<EnrollmentHistory>();
            try
            {
                using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(null, printDebug: true);
                    EnrollmentHistory objeto = repository2.SingleOrDefaultWithoutEResponse(x => x.Id == EnrollmentHistoryId && x.Enabled == true, x => x.MCO, x => x.PMG, x => x.PCP, x => x.Member, x => x.Member.MCO, x => x.Member.PCP, x => x.Member.PMG);
                    //if (objeto != null)
                    //{
                    //    DateTime initOpenEnrollmentForActives = DateTime.Parse(CustomConfigurationLib.DateOfInitEnrollmentForActives, CultureInfo.CreateSpecificCulture("en-US"));
                    //    DateTime endOpenEnrollmentForActives = DateTime.Parse(CustomConfigurationLib.DateOfEndEnrollmentForActives, CultureInfo.CreateSpecificCulture("en-US"));
                    //    bool isAvailableForChange = true;
                    //    string reason = string.Empty;
                    //    string reasonEN = string.Empty;
                    //    int code = 0;
                    //    ValidateElegibility(objeto, ref isAvailableForChange, ref reason, ref reasonEN, ref code);
                    //    ValidateIfExistChangePrevious(objeto, ref isAvailableForChange, ref reason, ref reasonEN, initOpenEnrollmentForActives, endOpenEnrollmentForActives, ref code);
                    //    ValidateCertificationDateForNews(objeto, ref isAvailableForChange, ref reason, ref reasonEN, ref code);
                    //    objeto.IsAvailableForChange = isAvailableForChange;
                    //    objeto.Reason = reason;
                    //    objeto.ReasonEN = reasonEN;
                    //}
                    result = new UtilitariesResponse<EnrollmentHistory>(config).setResponseBaseForObj(objeto);
                    Logger.Print_Response(result, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<EnrollmentHistory>(config).setResponseBaseForException(e);
                Logger.Error(e);
            }
            return result;
        }

        public EResponseBase<EnrollmentHistory> GetOnlyRejects()
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<EnrollmentHistory> result = new EResponseBase<EnrollmentHistory>();
            try
            {
                using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(null, printDebug: true);
                    result = repository2.Find(x => x.StatusId == AppConstants.StatusIdForReject && x.Enabled == true, null, x => x.MCO, x => x.PMG, x => x.PCP, x => x.Member, x => x.Member.MCO, x => x.Member.PCP, x => x.Member.PMG);
                    Logger.Print_Response(result, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<EnrollmentHistory>(config).setResponseBaseForException(e);
                Logger.Error(e);
            }
            return result;
        }

        public EResponseBase<Member> ChangeEnrollmentForCorrection(int memberId, int? mcoId, int? pmgId, int? pcpId, int? ppcpId, bool permission, int justCause, DataSource origin, string userName, int EnrollmentHistoryId, bool ignoreValidationRules = false)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<Member> result = new EResponseBase<Member>();

            try
            {
                using (IDbContextScope ctx = dbContextScopeFactory.Create())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(null, printDebug: true);
                    //Pendiente             
                    bool isAvailableForChange = true;
                    string reason = string.Empty;
                    string reasonEN = string.Empty;
                    int code = 0;
                    bool MCOChange = true;
                    bool PCPChange = true;
                    bool PMGChange = true;
                    //var memberlist = repository.Find(x => x.Id == memberId, null, x => x.PMG, x => x.PCP, x => x.MCO); //, null, x => x.PMG, x => x.PCP, x => x.MCO
                    EResponseBase<Member> memberlist = repository.Find(x => x.Id == memberId);
                    Member member = memberlist.listado.FirstOrDefault();
                    if (member != null)
                    {
                        int? currentMcoId = member.MCOId;
                        DateTime? currentMcoEffectiveDate = member.MCOEffectiveDate;
                        int? currentPmgId = member.PMGId;
                        DateTime? currentPmgEffectiveDate = member.PMGEffectiveDate;
                        int? currentPpcpId = member.PCPId;
                        DateTime? currentPcpEffectiveDate = member.PCPEffectiveDate;

                        if (mcoId == currentMcoId)
                        {
                            result.Code = CustomConfigurationLib.CodigoNewEnrollmentIsTheSame;
                            result.Message = CustomConfigurationLib.MensajeNewEnrollmentIsTheSameES;
                            result.MessageEN = CustomConfigurationLib.MensajeNewEnrollmentIsTheSameEN;
                        }
                        else
                        {
                            // asignar 
                            bool isconsuler = false;
                            if (permission)
                            {
                                ignoreValidationRules = true;
                            }

                            bool isValidForChange = IsValidForChange(member, ignoreValidationRules, ref isAvailableForChange, ref reason, ref reasonEN, ref code, isconsuler);
                            DateTime? McoEffectiveDate = null;
                            DateTime? PmgEffectiveDate = null;
                            DateTime? PcpEffectiveDate = null;

                            //Evitando validación temporal
                            isValidForChange = true;
                            if (isValidForChange)
                            {
                                if (mcoId == currentMcoId && pmgId == currentPmgId && ppcpId == currentPpcpId)
                                {
                                    result.Code = CustomConfigurationLib.CodigoNewEnrollmentIsTheSame;
                                    result.Message = CustomConfigurationLib.MensajeNewEnrollmentIsTheSameES;
                                    result.MessageEN = CustomConfigurationLib.MensajeNewEnrollmentIsTheSameEN;
                                }
                                else
                                {
                                    if (mcoId == currentMcoId)
                                    {
                                        McoEffectiveDate = currentMcoEffectiveDate;
                                        MCOChange = false;
                                    }
                                    else
                                    {
                                        McoEffectiveDate = setEffectiveDate(DateTime.Now);
                                    }

                                    if (pmgId == currentPmgId)
                                    {
                                        PmgEffectiveDate = currentPmgEffectiveDate;
                                        PMGChange = false;
                                    }
                                    else
                                    {
                                        PmgEffectiveDate = setEffectiveDate(DateTime.Now);
                                    }

                                    if (ppcpId == currentPpcpId)
                                    {
                                        PcpEffectiveDate = currentPcpEffectiveDate;
                                        PCPChange = false;
                                    }
                                    else
                                    {
                                        PcpEffectiveDate = setEffectiveDate(DateTime.Now);
                                    }

                                    ////member.PCP = member.PCPId == ppcpId ? member.PCP : null;
                                    ////member.PCPId = member.PCPId == ppcpId ? member.PCPId : ppcpId;
                                    ///
                                    //member.PCP = new PrimaryCarePhysician();
                                    //member.PCP.Id = pcpId.Value;
                                    //member.PCP = repositoryPcp.FirstOrDefault(x => x.Id == pcpId.Value).objeto;

                                    ////member.PCPEffectiveDate = PcpEffectiveDate;
                                    ////member.MCO = member.MCOId == mcoId ? member.MCO : null;
                                    ////member.MCOId = member.MCOId == mcoId ? member.MCOId : mcoId;

                                    //member.MCO = new ManagedCareOrganization();
                                    //member.MCO.Id = mcoId.Value;
                                    //member.MCO = repositoryMco.FirstOrDefault(x => x.Id == mcoId.Value).objeto;

                                    ////member.MCOEffectiveDate = McoEffectiveDate;
                                    ////member.PMG = member.PMGId == pmgId ? member.PMG : null;
                                    ////member.PMGId = member.PMGId == pmgId ? member.PMGId : pmgId;

                                    //member.PMG = new PrimaryMedicalGroup();
                                    //member.PMG.Id = pmgId.Value;
                                    //member.PMG = repositoryPmg.FirstOrDefault(x => x.Id == pmgId.Value).objeto;

                                    ////member.PMGEffectiveDate = PmgEffectiveDate;

                                    member.UpdatedBy = userName;
                                    DateTime fecha_actual = DateTime.Now;
                                    if (fecha_actual.Day > 21)
                                    {
                                        member.NewCertificationDate = new DateTime(fecha_actual.Year, fecha_actual.Month + 1, 1);
                                    }
                                    member.UpdatedOn = DateTime.Now;
                                    member.MemberPrimaryCenter = repository4.FirstOrDefault(x => x.Id == pmgId).objeto.PmgCode;
                                    result = repository.Update(member);
                                    //repository.SaveChanges();
                                    var restulHistory = InsertHistory(origin, userName, member, pmgId, ppcpId, mcoId, justCause, MCOChange, PCPChange, PMGChange, EnrollmentHistoryId);
                                    ctx.SaveChanges();
                                    result.objeto.EnrollmentHistoryID = restulHistory.objeto.Id;
                                    //
                                }
                            }
                            else
                            {
                                result.Code = code;
                                result.Message = reason;
                                result.MessageEN = reasonEN;
                            }
                        }
                    }
                    else
                    {
                        result = new UtilitariesResponse<Member>(config).setResponseBaseForNoDataFound();
                    }

                    Logger.Print_Response(result, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<Member>(config).setResponseBaseForException(e);
                Logger.Error(e);
            }
            return result;
        }

        private string ToParamValue(DataSource originSource)
        {
            string result;

            switch (originSource)
            {
                case DataSource.None:
                    result = string.Empty;
                    break;
                case DataSource.Medicaid:
                    result = "MEDICAID";
                    break;
                case DataSource.Counselor:
                    result = "COUNSELOR";
                    break;
                case DataSource.SelfService:
                    result = "SELFSERVICE";
                    break;
                case DataSource.Assist:
                    result = "ASSIST";
                    break;
                case DataSource.Ases:
                    result = "ASES";
                    break;
                default:
                    result = null;
                    break;
            }

            return result;
        }

        private EResponseBase<EnrollmentHistory> InsertHistory(DataSource originSource, string userName, Member member, int? newPmg, int? newPcpp, int? newMco, int justcauseReason, bool MCOChange, bool PCPChange, bool PMGChange, int EnrollmentHistoryId)
        {
            string origin = ToParamValue(originSource);
            EnrollmentHistory history = new EnrollmentHistory()
            {
                CertificationDate = member.CertificationDate,
                CreatedBy = member.UpdatedBy,
                CreatedOn = member.UpdatedOn,
                DateOfBirth = member.DateOfBirth,
                Enabled = member.Enabled,
                FamilyId = member.FamilyId,
                FirstLastName = member.FirstLastName,
                FirstName = member.FirstName,
                HICNumber = member.HICNumber,
                Id = EnrollmentHistoryId,
                MCOEffectiveDate = member.MCOEffectiveDate,
                MCOId = newMco,
                MCOModifiedBy = userName,
                MCOModifiedDate = DateTime.Now,
                MCOModifiedSource = origin,
                MedicaidIndicator = member.MedicaidIndicator,
                MedicareIndicator = member.MedicareIndicator,
                MemberId = member.Id,
                MemberPrimaryCenter = member.MemberPrimaryCenter,
                MiddleName = member.MiddleName,
                MPI = member.MPI,
                MPIContactMember = member.MPIContactMember,
                MPIShort = member.MPIShort,
                PCPEffectiveDate = member.PCPEffectiveDate,
                PCPId = newPcpp,
                PCPModifiedBy = userName,
                PCPModifiedSource = origin,
                PCPModifiedDate = DateTime.Now,
                PlanType = member.PlanType,
                PlanVersion = member.PlanVersion,
                PMGEffectiveDate = member.PMGEffectiveDate,
                PMGId = newPmg,
                PMGModifiedBy = userName,
                PMGModifiedDate = DateTime.Now,
                PMGModifiedSource = origin,
                SecondLastName = member.SecondLastName,
                SSN = member.SSN,
                Suffix = member.Suffix,
                TranId = member.TranId,
                UpdatedBy = member.UpdatedBy,
                UpdatedOn = member.UpdatedOn,
                PreviusPmg = member.PMGId,
                PreviusMco = member.MCOId,
                PreviusPcp = member.PCPId,
                JustCauseReasonId = justcauseReason,
                MCOChange = MCOChange,
                PCPChange = PCPChange,
                PMGChange = PMGChange,
                StatusId = 1
            };
            return repository2.Update(history);
        }

        private DateTime? setEffectiveDate(DateTime now)
        {
            DateTime? result = null;

            if (now.Day < CustomConfigurationLib.DayOfBreakEffectiveDate)
            {
                result = (new DateTime(now.Year, now.Month, 1)).AddMonths(1);
            }
            else
            {
                result = (new DateTime(now.Year, now.Month, 1)).AddMonths(2);
            }
            return result;
        }

        private bool IsValidForChange(Member member, bool ignoreValidationRules, ref bool isAvailableForChange, ref string reason, ref string reasonEN, ref int Code, bool isconsuler = false)
        {
            if (ignoreValidationRules) return true;
            bool result = true;
            DateTime initOpenEnrollmentForActives = DateTime.Parse(CustomConfigurationLib.DateOfInitEnrollmentForActives, CultureInfo.CreateSpecificCulture("en-US"));
            DateTime endOpenEnrollmentForActives = DateTime.Parse(CustomConfigurationLib.DateOfEndEnrollmentForActives, CultureInfo.CreateSpecificCulture("en-US"));
            if (!ValidateElegibility(member, ref isAvailableForChange, ref reason, ref reasonEN, ref Code)) return false;
            if (!ValidateCertificationDateForNews(member, ref isAvailableForChange, ref reason, ref reasonEN, ref Code)) return false;
            if (!ValidateIfExistChangePrevious(member, ref isAvailableForChange, ref reason, ref reasonEN, initOpenEnrollmentForActives, endOpenEnrollmentForActives, ref Code)) return false;
            // verificar capacidad
            //if (!ValidateMCOCapacity(member, ref isAvailableForChange, ref reason, ref reasonEN, ref Code)) return false;
            //if (!ValidatePCPCapacity(member, ref isAvailableForChange, ref reason, ref reasonEN, ref Code)) return false;
            //agregar periodo

            return result;
        }

        private bool ValidateElegibility(Member objeto, ref bool isAvailableForChange, ref string reason, ref string reasonEN, ref int Code)
        {
            bool result = true;
            if (objeto.TranId.Trim().ToUpper() != AppConstants.ElegibilityChar)
            {
                result = false;
                isAvailableForChange = false;
                Code = CustomConfigurationLib.CodigoValidateElegibility;
                reason = CustomConfigurationLib.MensajeValidateEligibilityES;
                reasonEN = CustomConfigurationLib.MensajeValidateEligibilityEN;
            }
            return result;
        }

        private bool ValidateCertificationDateForNews(Member objeto, ref bool isAvailableForChange, ref string reason, ref string reasonEN, ref int Code)
        {
            bool resultFinal = true;
            DateTime InitEnrollmentProcess = DateTime.Parse(CustomConfigurationLib.DateOfInitEnrollmentProcess, CultureInfo.CreateSpecificCulture("en-US"));
            if (DateTime.Today >= InitEnrollmentProcess)
            {

                IQueryable<EnrollmentHistory> result = repository2.FindWithoutEResponse(x => x.MemberId == objeto.Id && x.Enabled == true);
                if (result == null)
                {
                    resultFinal = ValidateCertificationDate(objeto, ref isAvailableForChange, ref reason, ref reasonEN, ref Code);
                }
                else
                {
                    if (!result.Any())
                    {
                        resultFinal = ValidateCertificationDate(objeto, ref isAvailableForChange, ref reason, ref reasonEN, ref Code);
                    }
                }
            }
            return resultFinal;
        }

        private bool ValidateIfExistChangePrevious(Member objeto, ref bool isAvailableForChange, ref string reason, ref string reasonEN, DateTime openEnrollmentPeriodStart, DateTime openEnrollmentPeriodEnd, ref int code)
        {
            bool resultFinal = true;
            IQueryable<EnrollmentHistory> result = repository2.FindWithoutEResponse(x => x.MemberId == objeto.Id && x.Enabled == true && (x.CreatedOn >= openEnrollmentPeriodStart && x.CreatedOn <= openEnrollmentPeriodEnd));
            if (result != null)
            {
                if (result.Any())
                {
                    resultFinal = false;
                    isAvailableForChange = false;
                    code = CustomConfigurationLib.CodigoValidateIfExistChangePrevious;
                    reason = CustomConfigurationLib.MensajeValidateIfExistChangePreviousES;
                    reasonEN = CustomConfigurationLib.MensajeValidateIfExistChangePreviousEN;
                }
            }
            return resultFinal;
        }

        private static bool ValidateCertificationDate(Member objeto, ref bool isAvailableForChange, ref string reason, ref string reasonEN, ref int Code)
        {
            bool result = true;
            if (objeto.CertificationDate.HasValue)
            {
                if (DateTime.Today >= objeto.CertificationDate.Value.AddDays(CustomConfigurationLib.DaysAfterCertificationDate))
                {
                    result = false;
                    isAvailableForChange = false;
                    Code = CustomConfigurationLib.CodigoValidateCertificationDate;
                    reason = CustomConfigurationLib.MensajeValidateCertificationDateES;
                    reasonEN = CustomConfigurationLib.MensajeValidateCertificationDateEN;
                }
            }
            return result;
        }

    }
}
