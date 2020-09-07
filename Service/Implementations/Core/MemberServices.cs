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
    public class MemberServices : IMemberServices
    {
        private readonly IDbContextScopeFactory dbContextScopeFactory;
        private readonly IRepository<Member, ApplicationDbContext> repository;
        private readonly IRepository<EnrollmentHistory, ApplicationDbContext> repository2;
        private readonly IRepository<EnrollmentPeriod, ApplicationDbContext> repository3;
        private readonly IRepository<PrimaryMedicalGroup, ApplicationDbContext> repository4;
        private readonly IConfigurationLib config;
        public ITransaction Transaction { get; set; }
        public ICustomLog Logger { get; set; }

        public MemberServices(IDbContextScopeFactory _dbContextScopeFactory,
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

        public EResponseBase<EnrollmentHistory> GetEnrollmentChangeHistory(int memberId)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<EnrollmentHistory> result = new EResponseBase<EnrollmentHistory>();
            try
            {
                using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(null, printDebug: true);
                    result = repository2.Find(x => x.MemberId == memberId && x.Enabled == true, null, x => x.MCO, x => x.PMG, x => x.PCP, x => x.Status);
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

        public async Task<EResponseBase<EnrollmentHistory>> SendSms(int memberId, string phone)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<EnrollmentHistory> result = new EResponseBase<EnrollmentHistory>();
            try
            {
                if (!string.IsNullOrEmpty(phone))
                {
                    phone = phone.Replace("(", "")
                        .Replace(")", "").Replace("-", "").Replace(" ", "");
                }
                using (var context = new ApplicationDbContext())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(null, printDebug: true);
                    var objeto = context.EnrollmentHistories.Where(x => x.MemberId == memberId && x.Enabled == true).ToList().LastOrDefault();

                    Logger.Print_Request("{" + string.Format("memberId:{0}, phone:{1}", objeto.Id, phone) + "}");
                    Sinch sms = new Sinch();
                    var status = await sms.SendAndCheckSms(phone, objeto.Id);
                    Logger.Print_Response(status);
                    if (status.Type != "-1")
                    {
                        if (status.Statuses[0].Code == 0 || status.Statuses[0].Code == 401) //|| status.Statuses[0].Code == 400 
                        {
                            result = new UtilitariesResponse<EnrollmentHistory>(config).setResponseBaseForOK(objeto);
                        }
                        else
                        {
                            result = new UtilitariesResponse<EnrollmentHistory>(config).setResponseBaseForSmsError();
                            throw new Exception("status type = -1 // error" + Newtonsoft.Json.JsonConvert.SerializeObject(status));
                        }
                    }
                    else
                    {
                        result = new UtilitariesResponse<EnrollmentHistory>(config).setResponseBaseForSmsError();
                        throw new Exception("status type = -1 // error" + Newtonsoft.Json.JsonConvert.SerializeObject(status));
                    }

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


        public EResponseBase<EnrollmentHistory> GetEnrollmentHistory(DateTime? fecini, DateTime? fecfin)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<EnrollmentHistory> result = new EResponseBase<EnrollmentHistory>();
            try
            {

                using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(null, printDebug: true);
                    result = repository2.Find(x => x.UpdatedOn >= fecini && x.UpdatedOn <= fecfin && x.Enabled == true);
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

        public EResponseBase<EnrollmentPeriod> GetEnrPeriod()
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<EnrollmentPeriod> result = new EResponseBase<EnrollmentPeriod>();
            try
            {

                using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(null, printDebug: true);
                    //result = repository3.FirstOrDefault(x => x.MemberId == memberId );
                    result = repository3.FirstOrDefault(x => x.Enabled == true);
                    //asignando periodo default

                    if (result.objeto == null)
                    {
                        EnrollmentPeriod def = new EnrollmentPeriod();
                        int year = DateTime.Today.Year;
                        def.PeriodIni = Convert.ToDateTime("01/11/" + year.ToString());
                        def.PeriodFin = Convert.ToDateTime("31/01/" + (year + 1).ToString());
                        result.objeto = def;
                        result.Code = 0;
                    }

                    //validar si se puede hacer enrollment por fecha actual

                    if (DateTime.Today.Date >= result.objeto.PeriodIni && DateTime.Today.Date <= result.objeto.PeriodFin)
                    {
                        result.objeto.Enabled = true;
                    }
                    else
                    {
                        result.objeto.Enabled = false;
                    }


                    Logger.Print_Response(result, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<EnrollmentPeriod>(config).setResponseBaseForException(e);
                Logger.Error(e);
            }
            return result;
        }

        public EResponseBase<EnrollmentPeriod> SendEnrPeriod(DateTime? fecini, DateTime? fecfin)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<EnrollmentPeriod> result = new EResponseBase<EnrollmentPeriod>();

            try
            {
                using (IDbContextScope ctx = dbContextScopeFactory.Create())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(null, printDebug: true);
                    result = repository3.FirstOrDefault(x => x.Enabled == true);
                    EnrollmentPeriod previusPeriod = result.objeto;
                    previusPeriod.Enabled = false;
                    //Pendiente             

                    string reason = string.Empty;
                    string reasonEN = string.Empty;

                    EnrollmentPeriod period = new EnrollmentPeriod()
                    {
                        PeriodIni = fecini,
                        PeriodFin = fecfin,
                        CreatedOn = DateTime.Today,
                        Enabled = true

                    };

                    EResponseBase<EnrollmentPeriod> responsePeriod = repository3.Insert(period);
                    EResponseBase<EnrollmentPeriod> responsePreviudPeriod = repository3.Update(previusPeriod);
                    result.Code = 0;
                    ctx.SaveChanges();

                    Logger.Print_Response(result, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<EnrollmentPeriod>(config).setResponseBaseForException(e);
                Logger.Error(e);
            }
            return result;
        }

        public EResponseBase<Member> ChangeEnrollment(int memberId, int? mcoId, int? pmgId, int? pcpId, int? ppcpId, bool permission, int justCause, DataSource origin, string userName, string justCauseComment, string phone, string email, bool ignoreValidationRules = false)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<Member> result = new EResponseBase<Member>();

            try
            {
                if (!string.IsNullOrEmpty(phone))
                {
                    phone = phone.Replace("(", "")
                        .Replace(")", "").Replace("-", "").Replace(" ", "");
                }
                using (IDbContextScope ctx = dbContextScopeFactory.Create())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(null, printDebug: true);
                    //Pendiente             
                    bool isAvailableForChange = true;
                    string reason = string.Empty;
                    string reasonEN = string.Empty;
                    string strMemberPrimaryCenter = string.Empty;
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
                        //DateTime? currentMcoEffectiveDate = member.MCOEffectiveDate;
                        int? currentPmgId = member.PMGId;
                        //DateTime? currentPmgEffectiveDate = member.PMGEffectiveDate;
                        int? currentPpcpId = member.PCPId;
                        //DateTime? currentPcpEffectiveDate = member.PCPEffectiveDate;

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

                            //bool isValidForChange = IsValidForChange(member, ignoreValidationRules, ref isAvailableForChange, ref reason, ref reasonEN, ref code, isconsuler);
                            bool isValidForChange = ValidateIfExistChangeInProcess(member, ref isAvailableForChange, ref reason, ref reasonEN, ref code);
                            //DateTime? McoEffectiveDate = null;
                            //DateTime? PmgEffectiveDate = null;
                            //DateTime? PcpEffectiveDate = null;

                            //Evitando validación temporal
                            //isValidForChange = true;
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
                                    //McoEffectiveDate = setEffectiveDate(DateTime.Now);
                                    //if (mcoId == currentMcoId)
                                    //{
                                    //    McoEffectiveDate = currentMcoEffectiveDate;
                                    //    MCOChange = false;
                                    //}
                                    //else
                                    //{
                                    //    McoEffectiveDate = setEffectiveDate(DateTime.Now);
                                    //}

                                    //if (pmgId == currentPmgId)
                                    //{
                                    //    PmgEffectiveDate = currentPmgEffectiveDate;
                                    //    PMGChange = false;
                                    //}
                                    //else
                                    //{
                                    //    PmgEffectiveDate = setEffectiveDate(DateTime.Now);
                                    //}

                                    //if (ppcpId == currentPpcpId)
                                    //{
                                    //    PcpEffectiveDate = currentPcpEffectiveDate;
                                    //    PCPChange = false;
                                    //}
                                    //else
                                    //{
                                    //    PcpEffectiveDate = setEffectiveDate(DateTime.Now);
                                    //}

                                    ////member.PCP = member.PCPId == ppcpId ? member.PCP : null;
                                    ////member.PCPId = member.PCPId == ppcpId ? member.PCPId : ppcpId;
                                    ///
                                    //member.PCP = new PrimaryCarePhysician();
                                    //member.PCP.Id = pcpId.Value;
                                    //member.PCP = repositoryPcp.FirstOrDefault(x => x.Id == pcpId.Value).objeto;

                                    //member.PCPEffectiveDate = McoEffectiveDate;
                                    //member.PCPEffectiveDate = PcpEffectiveDate;
                                    ////member.MCO = member.MCOId == mcoId ? member.MCO : null;
                                    ////member.MCOId = member.MCOId == mcoId ? member.MCOId : mcoId;

                                    //member.MCO = new ManagedCareOrganization();
                                    //member.MCO.Id = mcoId.Value;
                                    //member.MCO = repositoryMco.FirstOrDefault(x => x.Id == mcoId.Value).objeto;

                                    //member.MCOEffectiveDate = McoEffectiveDate;
                                    //member.MCOEffectiveDate = McoEffectiveDate;
                                    ////member.PMG = member.PMGId == pmgId ? member.PMG : null;
                                    ////member.PMGId = member.PMGId == pmgId ? member.PMGId : pmgId;

                                    //member.PMG = new PrimaryMedicalGroup();
                                    //member.PMG.Id = pmgId.Value;
                                    //member.PMG = repositoryPmg.FirstOrDefault(x => x.Id == pmgId.Value).objeto;

                                    //member.PMGEffectiveDate = McoEffectiveDate;
                                    //member.PMGEffectiveDate = PmgEffectiveDate;

                                    //member.UpdatedBy = userName;
                                    //DateTime fecha_actual = DateTime.Now;
                                    //if (fecha_actual.Day > 21)
                                    //{
                                    //    member.NewCertificationDate = new DateTime(fecha_actual.Year, fecha_actual.Month + 1, 1);
                                    //}
                                    //member.UpdatedOn = DateTime.Now;
                                    //"0000"
                                    //member.MemberPrimaryCenter = repository4.FirstOrDefault(x => x.Id == pmgId).objeto.PmgCode;
                                    strMemberPrimaryCenter = repository4.FirstOrDefault(x => x.Id == pmgId).objeto.PmgCode;
                                    result = repository.Update(member);
                                    //repository.SaveChanges();
                                    var restulHistory = InsertHistory(origin, userName, member, pmgId, ppcpId, mcoId, justCause, MCOChange, PCPChange, PMGChange, justCauseComment, phone, email, strMemberPrimaryCenter);
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

        public bool ChangeEnrollmentEnabled(int memberId, bool inEnrollmentPeriod, bool ignoreValidationRules, DateTime initOpenEnrollmentForActives, DateTime endOpenEnrollmentForActives, ref bool isAvailableForChange, ref string reason, ref string reasonEN, ref int code, bool isconsuler = false)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            bool result = true;
            try
            {
                using (IDbContextScope ctx = dbContextScopeFactory.Create())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(null, printDebug: true);
                    Member member = repository.Find(x => x.Id == memberId).listado.FirstOrDefault();
                    if (member != null)
                    {
                        //result = IsValidForChangeEnabled(member, ignoreValidationRules, initOpenEnrollmentForActives, endOpenEnrollmentForActives,ref isAvailableForChange, ref reason, ref reasonEN, ref code, isconsuler);
                        if (ignoreValidationRules) return true;
                        if (!ValidateElegibility(member, ref isAvailableForChange, ref reason, ref reasonEN, ref code)) return false;
                        bool inCertificationDate = ValidateCertificationDateEnabled(member, ref isAvailableForChange, ref reason, ref reasonEN, ref code);
                        if (!inCertificationDate && !inEnrollmentPeriod) return false;
                        if (!ValidateIfExistChangeInProcess(member, ref isAvailableForChange, ref reason, ref reasonEN, ref code)) return false;
                        if ((!inCertificationDate && inEnrollmentPeriod))
                        {
                            if (!ValidateIfExistOneChangeInEnrollmentPeriod(member, ref isAvailableForChange, ref reason, ref reasonEN, initOpenEnrollmentForActives, endOpenEnrollmentForActives, ref code)) return false;
                        }
                        if (inCertificationDate && !inEnrollmentPeriod && Convert.ToDateTime(member.CertificationDate).Year == DateTime.Now.Year && DateTime.Now < initOpenEnrollmentForActives)
                        {
                            if (!ValidateIfExistOneChangeInCertificationDate(member, ref isAvailableForChange, ref reason, ref reasonEN, Convert.ToDateTime(member.CertificationDate), Convert.ToDateTime(member.CertificationDate.Value.AddDays(CustomConfigurationLib.DaysAfterCertificationDate)), ref code)) return false;
                        }
                        if (inCertificationDate && inEnrollmentPeriod)
                        {
                            //if (!ValidateIfExistChangeOverLaping(member, ref isAvailableForChange, ref reason, ref reasonEN, initOpenEnrollmentForActives, endOpenEnrollmentForActives, ref code)) return false;
                            if (!ValidateIfExistOneChangeInEnrollmentPeriod(member, ref isAvailableForChange, ref reason, ref reasonEN, initOpenEnrollmentForActives, endOpenEnrollmentForActives, ref code)) return false;
                        }
                        if (inCertificationDate && !inEnrollmentPeriod && Convert.ToDateTime(member.CertificationDate).Year == DateTime.Now.Year && DateTime.Now > endOpenEnrollmentForActives)
                        {
                            if (!ValidateIfExistOneChangeInEnrollmentPeriod(member, ref isAvailableForChange, ref reason, ref reasonEN, initOpenEnrollmentForActives, endOpenEnrollmentForActives, ref code)) return false;
                            if (!ValidateIfExistOneChangeInCertificationDate(member, ref isAvailableForChange, ref reason, ref reasonEN, Convert.ToDateTime(member.CertificationDate), Convert.ToDateTime(member.CertificationDate.Value.AddDays(CustomConfigurationLib.DaysAfterCertificationDate)), ref code)) return false;
                        }
                    }

                    if (result)
                    {
                        code = 0;
                        reason = "";
                        reasonEN = "";
                    }

                    Logger.Print_Response(result, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
            return result;
        }

        public bool ChangeEnrollmentEnabledJustCause(int memberId, ref bool isAvailableForChange, ref string reason, ref string reasonEN, ref int code)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            bool result = true;
            try
            {
                using (IDbContextScope ctx = dbContextScopeFactory.Create())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(null, printDebug: true);
                    Member member = repository.Find(x => x.Id == memberId).listado.FirstOrDefault();
                    if (member != null)
                    {
                        if (!ValidateIfExistChangeInProcess(member, ref isAvailableForChange, ref reason, ref reasonEN, ref code)) return false;
                    }

                    if (result)
                    {
                        code = 0;
                        reason = "";
                        reasonEN = "";
                    }

                    Logger.Print_Response(result, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
            return result;
        }

        public EResponseBase<Member> Get()
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<Member> result = new EResponseBase<Member>();
            try
            {
                using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(null, printDebug: true);
                    result = repository.Find();
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

        public EResponseBase<Member> Get(int memberId)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<Member> result = new EResponseBase<Member>();
            try
            {
                using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(null, printDebug: true);
                    Member objeto = repository.SingleOrDefaultWithoutEResponse(x => x.Id == memberId, x => x.MCO, x => x.PMG, x => x.PCP, x => x.Family, x => x.PCP);
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
                    result = new UtilitariesResponse<Member>(config).setResponseBaseForObj(objeto);
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

        public EResponseBase<MemberCustomModel> GetEmail(int memberId)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<MemberCustomModel> result = new EResponseBase<MemberCustomModel>();
            try
            {
                using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
                {
                    //Logger.Print_InitMethod();
                    //Logger.Print_Request(null, printDebug: true);
                    //Member objeto = repository.SingleOrDefaultWithoutEResponse(x => x.Id == memberId, x => x.i);
                    //result = new UtilitariesResponse<Member>(config).setResponseBaseForObj(objeto);
                    //Logger.Print_Response(result, printDebug: true);
                    //Logger.Print_EndMethod();


                    Logger.Print_InitMethod();
                    Logger.Print_Request(null, printDebug: true);
                    ApplicationDbContext context = ctx.DbContexts.Get<ApplicationDbContext>();

                    IQueryable<MemberCustomModel> query = from member in context.Members
                                                          join user in context.Users on member.Id equals user.MemberId
                                                          where member.Id == memberId
                                                          select new MemberCustomModel()
                                                          {
                                                              Email = user.Email,
                                                              FirstLastName = member.FirstLastName,
                                                              SecondLastName = member.SecondLastName,
                                                              FirstName = member.FirstName
                                                          };


                    MemberCustomModel objeto = query.FirstOrDefault();

                    result = new UtilitariesResponse<MemberCustomModel>(config).setResponseBaseForObj(objeto);
                    Logger.Print_Response(result, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<MemberCustomModel>(config).setResponseBaseForException(e);
                Logger.Error(e);
            }
            return result;
        }

        public EResponseBase<MemberCustomModelV2> Get(string MPI, string Last4SSN, DateTime? DateOfBirth, string FirstName, string FirstLastName, string SecondLastName)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<MemberCustomModelV2> result = new EResponseBase<MemberCustomModelV2>();
            try
            {
                using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
                {

                    Logger.Print_InitMethod();
                    Logger.Print_Request(null, printDebug: true);
                    ApplicationDbContext context = ctx.DbContexts.Get<ApplicationDbContext>();
                    //IQueryable<Member> query = from member in context.Members
                    //                           where
                    //                            (
                    //                              (member.MPIShort == MPI || String.IsNullOrEmpty(MPI))
                    //                               && (member.Last4SSN == Last4SSN || String.IsNullOrEmpty(Last4SSN))
                    //                               && (member.FirstName.ToUpper().Trim().Contains(FirstName.ToUpper().Trim()) || String.IsNullOrEmpty(FirstName))
                    //                               && (member.FirstLastName.ToUpper().Trim().Contains(FirstLastName.ToUpper().Trim()) || String.IsNullOrEmpty(FirstLastName))
                    //                               && (member.SecondLastName.ToUpper().Trim().Contains(SecondLastName.ToUpper().Trim()) || String.IsNullOrEmpty(SecondLastName))
                    //                               && (member.DateOfBirth == null || ((DbFunctions.TruncateTime(member.DateOfBirth.Value) == DbFunctions.TruncateTime(DateOfBirth.Value) || DateOfBirth.Value == null)))
                    //                             )
                    //                           select member;

                    IQueryable<Member> query = from member in context.Members
                                               where member.MCOId != null
                                               select member;


                    if (MPI != null)
                    {
                        query = query.Where(x => x.MPIShort == MPI);
                    }
                    if (Last4SSN != null)
                    {
                        query = query.Where(x => x.Last4SSN == Last4SSN);
                    }
                    if (DateOfBirth != null)
                    {
                        query = query.Where(x => x.DateOfBirth == DateOfBirth);
                    }
                    if (FirstName != null)
                    {
                        query = query.Where(x => x.FirstName == FirstName);

                    }
                    if (FirstLastName != null)
                    {
                        query = query.Where(x => x.FirstLastName == FirstLastName);
                    }
                    if (SecondLastName != null)
                    {
                        query = query.Where(x => x.SecondLastName == SecondLastName);
                    }

                    var list = (from men in (query)
                                join fam in context.Families on men.FamilyId equals fam.Id
                                select new MemberCustomModelV2()
                                {
                                    Id = men.Id,
                                    FirstName = men.FirstName,
                                    FirstLastName = men.FirstLastName,
                                    SecondLastName = men.SecondLastName,
                                    MiddleName = men.MiddleName,
                                    MPIShort = men.MPIShort,
                                    Last4SSN = men.Last4SSN,
                                    DateOfBirth = men.DateOfBirth,
                                    Family = new FamilyCustomModel()
                                    {
                                        ContactFirstName = fam.ContactFirstName,
                                        ContactFirstLastName = fam.ContactFirstLastName,
                                        ContactSecondLastName = fam.ContactSecondLastName,
                                        ApplicationNumber = fam.ApplicationNumber,
                                        ResidenceAddressZipCode = fam.ResidenceAddressZipCode
                                    }
                                }).Take(200).ToList();
                    result = new UtilitariesResponse<MemberCustomModelV2>(config).setResponseBaseForList(list);
                    Logger.Print_Response(result, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<MemberCustomModelV2>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }
            return result;
        }

        public EResponseBase<Member> Get(string Last4SSN, DateTime DateOfBirth, string ZipCode, string FirstLastName, string SecondLastName, string FirstName)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<Member> result = new EResponseBase<Member>();
            try
            {
                using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(null, printDebug: true);
                    ApplicationDbContext context = ctx.DbContexts.Get<ApplicationDbContext>();
                    //var memberslist = (from member in context.Members
                    //              where
                    //              ((member.Last4SSN.Trim() == Last4SSN.Trim())
                    //                  && (member.Family.MailAddressZipCode.Trim() == ZipCode.Trim() || member.Family.ResidenceAddressZip4 == ZipCode.Trim())
                    //                )
                    //              select new
                    //              {
                    //                  shortDate = member.DateOfBirth.Value.ToShortDateString(),
                    //                  family=member.Family
                    //              }).ToList();

                    //var query2 = from c in memberslist
                    //            where c.shortDate == DateOfBirth.ToShortDateString()
                    //            select new Member {
                    //                Family=c.family
                    //            };
                    //var list = query2.ToList();
                    IQueryable<Member> query = from member in context.Members
                                               where
                                                ((member.Last4SSN.Trim() == Last4SSN.Trim())
                                                   && (
                                                       (DbFunctions.TruncateTime(member.DateOfBirth.Value) ==
                                                        DbFunctions.TruncateTime(DateOfBirth)))
                                                   && (member.Family.MailAddressZipCode.Trim() == ZipCode.Trim() || member.Family.ResidenceAddressZip4 == ZipCode.Trim())
                                                   && (member.FirstLastName == FirstLastName.Trim())
                                                   && (member.SecondLastName == SecondLastName.Trim())
                                                   && (member.FirstName == FirstName.Trim())
                                                 )
                                               select member;
                    List<Member> list = query.Include(i => i.Family).ToList();
                    //var list = query.Include(i => i.Family)                                   
                    //                .Distinct()
                    //                .ToList();
                    result = new UtilitariesResponse<Member>(config).setResponseBaseForList(list);
                    Logger.Print_Response(result, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<Member>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }
            return result;
        }

        public EResponseBase<Member> GetMembersById(int memberId)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<Member> result = new EResponseBase<Member>();
            try
            {
                using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(null, printDebug: true);
                    ApplicationDbContext context = ctx.DbContexts.Get<ApplicationDbContext>();
                    IQueryable<Member> query = context.Members.Include(i => i.PCP).Include(i => i.PMG).Include(i => i.MCO).
                        Where(x => x.Id == memberId);

                    //IQueryable<Member> query = from member in context.Members
                    //                           where (member.Id == memberId)
                    //                           select member;
                    //query = query.Include(i => i.Family)
                    //                .Distinct();
                    //List<Member> list = query.Include(i => i.PCP).Include(i => i.PMG).Include(i => i.MCO).ToList();

                    //var list = query.Include(i => i.Family)
                    //                .Distinct()
                    //                .ToList();
                    result = new UtilitariesResponse<Member>(config).setResponseBaseForList(query);
                    Logger.Print_Response(result, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<Member>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }
            return result;
        }

        private EResponseBase<EnrollmentHistory> InsertHistory(DataSource originSource, string userName, Member member, int? newPmg, int? newPcpp, int? newMco, int justcauseReason, bool MCOChange, bool PCPChange, bool PMGChange, string justCauseComment, string phone, string email, string MemberPrimaryCenter)
        {
            string origin = ToParamValue(originSource);

            //Para casos que no se deben enviar al archivo sus
            int StatusId = 1;
            DateTime? McoEffectiveDate = null;
            McoEffectiveDate = setEffectiveDate(DateTime.Now);

            //DateTime? NewCertificationDate = null;
            //DateTime fecha_actual = DateTime.Now;
            //if (fecha_actual.Day > 21)
            //{
            //    NewCertificationDate = new DateTime(fecha_actual.Year, fecha_actual.Month + 1, 1);
            //}
            //else
            //{
            //    NewCertificationDate = member.NewCertificationDate;
            //}
            if (justcauseReason == 7)
                StatusId = 7;
            EnrollmentHistory history = new EnrollmentHistory()
            {
                CertificationDate = member.CertificationDate,
                CreatedBy = userName,
                //CreatedOn = member.UpdatedOn,
                CreatedOn = DateTime.Today,
                DateOfBirth = member.DateOfBirth,
                Enabled = member.Enabled,
                FamilyId = member.FamilyId,
                FirstLastName = member.FirstLastName,
                FirstName = member.FirstName,
                HICNumber = member.HICNumber,
                Id = member.Id,
                MCOEffectiveDate = McoEffectiveDate,
                MCOId = newMco,
                MCOModifiedBy = userName,
                MCOModifiedDate = DateTime.Now,
                MCOModifiedSource = origin,
                MedicaidIndicator = member.MedicaidIndicator,
                MedicareIndicator = member.MedicareIndicator,
                MemberId = member.Id,
                MemberPrimaryCenter = MemberPrimaryCenter,
                MiddleName = member.MiddleName,
                MPI = member.MPI,
                MPIContactMember = member.MPIContactMember,
                MPIShort = member.MPIShort,
                PCPEffectiveDate = McoEffectiveDate,
                PCPId = newPcpp,
                PCPModifiedBy = userName,
                PCPModifiedSource = origin,
                PCPModifiedDate = DateTime.Now,
                PlanType = member.PlanType,
                PlanVersion = member.PlanVersion,
                PMGEffectiveDate = McoEffectiveDate,
                PMGId = newPmg,
                PMGModifiedBy = userName,
                PMGModifiedDate = DateTime.Now,
                PMGModifiedSource = origin,
                SecondLastName = member.SecondLastName,
                SSN = member.SSN,
                Suffix = member.Suffix,
                TranId = member.TranId,
                UpdatedBy = userName,
                //UpdatedOn = member.UpdatedOn,
                UpdatedOn = DateTime.Today,
                PreviusPmg = member.PMGId,
                PreviusMco = member.MCOId,
                PreviusPcp = member.PCPId,
                JustCauseReasonId = justcauseReason,
                MCOChange = MCOChange,
                PCPChange = PCPChange,
                PMGChange = PMGChange,
                StatusId = StatusId,
                JustCauseComment = justCauseComment,
                Phone = phone,
                Email = email
            };
            return repository2.Insert(history);
        }
        private EResponseBase<EnrollmentHistory> UpdatetHistory(DataSource originSource, string userName, Member member, int idHistories, int IdStatus, int? newPmg, int? newPcpp, int? newMco, int justcauseReason, bool MCOChange, bool PCPChange, bool PMGChange, string justCauseComment)
        {
            string origin = ToParamValue(originSource);
            EnrollmentHistory history = null;
            if (IdStatus == 5)
            {
                history = new EnrollmentHistory()
                {
                    CertificationDate = member.CertificationDate,
                    CreatedOn = member.UpdatedOn,
                    DateOfBirth = member.DateOfBirth,
                    Enabled = member.Enabled,
                    FamilyId = member.FamilyId,
                    FirstLastName = member.FirstLastName,
                    FirstName = member.FirstName,
                    HICNumber = member.HICNumber,
                    Id = idHistories,
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
                    StatusId = IdStatus,
                    JustCauseComment = justCauseComment
                };
            }
            else
            {
                history = new EnrollmentHistory()
                {
                    CertificationDate = member.CertificationDate,
                    CreatedOn = member.UpdatedOn,
                    DateOfBirth = member.DateOfBirth,
                    Enabled = member.Enabled,
                    FamilyId = member.FamilyId,
                    FirstLastName = member.FirstLastName,
                    FirstName = member.FirstName,
                    HICNumber = member.HICNumber,
                    Id = idHistories,
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
                    StatusId = IdStatus,
                    JustCauseComment = justCauseComment
                };
            }
            return repository2.InsertOrUpdateReject(history, idHistories);
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


        private bool IsValidForChangeEnabled(Member member, bool ignoreValidationRules, DateTime initOpenEnrollmentForActives, DateTime endOpenEnrollmentForActives, ref bool isAvailableForChange, ref string reason, ref string reasonEN, ref int Code, bool isconsuler = false)
        {
            if (ignoreValidationRules) return true;
            bool result = true;
            if (!ValidateElegibility(member, ref isAvailableForChange, ref reason, ref reasonEN, ref Code)) return false;
            if (!ValidateCertificationDateForNews(member, ref isAvailableForChange, ref reason, ref reasonEN, ref Code)) return false;
            if (!ValidateIfExistChangeInProcess(member, ref isAvailableForChange, ref reason, ref reasonEN, ref Code)) return false;
            if (!ValidateIfExistChangePrevious(member, ref isAvailableForChange, ref reason, ref reasonEN, initOpenEnrollmentForActives, endOpenEnrollmentForActives, ref Code)) return false;
            return result;
        }

        private bool ValidatePCPCapacity(Member member, ref bool isAvailableForChange, ref string reason, ref string reasonEN, ref int code)
        {
            bool result = true;
            //if (member.PCP.OverCapacity)
            //{
            //    result = false;
            //    isAvailableForChange = false;
            //    code = CustomConfigurationLib.CodigoPcpCapacity;
            //    reason = CustomConfigurationLib.MensajePCPCapacityES;
            //    reasonEN = CustomConfigurationLib.MensajePCPCapacityEN;
            //}
            return result;
        }

        private bool ValidateMCOCapacity(Member member, ref bool isAvailableForChange, ref string reason, ref string reasonEN, ref int code)
        {
            bool result = true;
            if (member.MCO.OverCapacity)
            {
                result = false;
                isAvailableForChange = false;
                code = CustomConfigurationLib.CodigoMcoCapacity;
                reason = CustomConfigurationLib.MensajeMcoCapacityES;
                reasonEN = CustomConfigurationLib.MensajeMcoCapacityEN;
            }
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

        private bool ValidatePeriod(Member objeto, ref bool isAvailableForChange, ref string reason, ref string reasonEN, ref int Code)
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

        private static bool ValidateCertificationDateEnabled(Member objeto, ref bool isAvailableForChange, ref string reason, ref string reasonEN, ref int Code)
        {
            bool result = false;
            Code = CustomConfigurationLib.CodigoValidateCertificationDate;
            reason = CustomConfigurationLib.MensajeValidateCertificationDateES;
            reasonEN = CustomConfigurationLib.MensajeValidateCertificationDateEN;
            if (objeto.CertificationDate.HasValue)
            {
                if (DateTime.Today >= objeto.CertificationDate.Value && DateTime.Today <= objeto.CertificationDate.Value.AddDays(CustomConfigurationLib.DaysAfterCertificationDate))
                {
                    result = true;
                    isAvailableForChange = true;
                    Code = 0;
                    reason = "Ok";
                    reasonEN = "Ok";
                }
            }
            return result;
        }

        private bool ValidateIfExistChangeInProcess(Member objeto, ref bool isAvailableForChange, ref string reason, ref string reasonEN, ref int code)
        {
            bool resultFinal = true;

            using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
            {
                Logger.Print_InitMethod();
                Logger.Print_Request(null, printDebug: true);
                ApplicationDbContext context = ctx.DbContexts.Get<ApplicationDbContext>();
                IQueryable<EnrollmentHistory> query = from h in context.EnrollmentHistories
                                                      where h.MemberId == objeto.Id && h.Status.AllowChange == false && h.Enabled == true
                                                      select h;

                if (query.ToList().Count() != 0)
                {
                    resultFinal = false;
                    isAvailableForChange = false;
                    code = CustomConfigurationLib.CodigoValidateIfExistChangeInProcess;
                    reason = CustomConfigurationLib.MensajeValidateIfExistChangeInProcessES;
                    reasonEN = CustomConfigurationLib.MensajeValidateIfExistChangeInProcessEN;
                }
            }

            //IQueryable<EnrollmentHistory> result = repository2.FindWithoutEResponse(x => x.MemberId == objeto.Id && x.Status.AllowChange == true);
            //if (result != null)
            //{
            //    if (!result.Any())
            //    {
            //        resultFinal = false;
            //        isAvailableForChange = false;
            //        code = CustomConfigurationLib.CodigoValidateIfExistChangeInProcess;
            //        reason = CustomConfigurationLib.MensajeValidateIfExistChangeInProcessES;
            //        reasonEN = CustomConfigurationLib.MensajeValidateIfExistChangeInProcessEN;
            //    }
            //}
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

        private bool ValidateIfExistChangeOverLaping(Member objeto, ref bool isAvailableForChange, ref string reason, ref string reasonEN, DateTime openEnrollmentPeriodStart, DateTime openEnrollmentPeriodEnd, ref int code)
        {
            bool resultFinal = true;
            IQueryable<EnrollmentHistory> result = repository2.FindWithoutEResponse(x => x.MemberId == objeto.Id && x.Enabled == true && x.Status.AllowChange == false && (x.CreatedOn >= openEnrollmentPeriodStart && x.CreatedOn <= openEnrollmentPeriodEnd));
            if (result != null)
            {
                if (!result.Any())
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

        private bool ValidateIfExistOneChangeInEnrollmentPeriod(Member objeto, ref bool isAvailableForChange, ref string reason, ref string reasonEN, DateTime openEnrollmentPeriodStart, DateTime openEnrollmentPeriodEnd, ref int code)
        {
            bool resultFinal = true;

            using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
            {
                Logger.Print_InitMethod();
                Logger.Print_Request(null, printDebug: true);
                ApplicationDbContext context = ctx.DbContexts.Get<ApplicationDbContext>();
                IQueryable<EnrollmentHistory> query = from h in context.EnrollmentHistories
                                                      where h.MemberId == objeto.Id && h.Enabled == true && h.Status.AllowChange == true && h.CreatedOn >= openEnrollmentPeriodStart && h.CreatedOn <= openEnrollmentPeriodEnd
                                                      select h;

                if (query.ToList().Count() != 0)
                {
                    resultFinal = false;
                    isAvailableForChange = false;
                    code = CustomConfigurationLib.CodigoValidateIfExistChangePrevious;
                    reason = CustomConfigurationLib.MensajeValidateIfExistChangePreviousES;
                    reasonEN = CustomConfigurationLib.MensajeValidateIfExistChangePreviousEN;
                }
            }

            //IQueryable<EnrollmentHistory> result = repository2.FindWithoutEResponse(x => x.MemberId == objeto.Id && x.Status.AllowChange == false && (x.CreatedOn >= openEnrollmentPeriodStart && x.CreatedOn <= openEnrollmentPeriodEnd));
            //if (result != null)
            //{
            //    if (!result.Any())
            //    {
            //        resultFinal = false;
            //        isAvailableForChange = false;
            //        code = CustomConfigurationLib.CodigoValidateIfExistChangePrevious;
            //        reason = CustomConfigurationLib.MensajeValidateIfExistChangePreviousES;
            //        reasonEN = CustomConfigurationLib.MensajeValidateIfExistChangePreviousEN;
            //    }
            //}
            return resultFinal;
        }

        private bool ValidateIfExistOneChangeInCertificationDate(Member objeto, ref bool isAvailableForChange, ref string reason, ref string reasonEN, DateTime openEnrollmentPeriodStart, DateTime openEnrollmentPeriodEnd, ref int code)
        {
            bool resultFinal = true;

            using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
            {
                Logger.Print_InitMethod();
                Logger.Print_Request(null, printDebug: true);
                ApplicationDbContext context = ctx.DbContexts.Get<ApplicationDbContext>();
                IQueryable<EnrollmentHistory> query = from h in context.EnrollmentHistories
                                                      where h.MemberId == objeto.Id && h.Enabled == true && h.Status.AllowChange == true && h.CreatedOn >= openEnrollmentPeriodStart && h.CreatedOn <= openEnrollmentPeriodEnd
                                                      select h;

                if (query.ToList().Count() != 0)
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

        private bool ValidateIfExistOneChangeInCertificationDate2(Member objeto, ref bool isAvailableForChange, ref string reason, ref string reasonEN, DateTime openEnrollmentPeriodStart, DateTime openEnrollmentPeriodEnd, ref int code)
        {
            bool resultFinal = true;
            IQueryable<EnrollmentHistory> result = repository2.FindWithoutEResponse(x => x.MemberId == objeto.Id && x.Enabled == true && x.Status.AllowChange == true && x.CreatedOn >= openEnrollmentPeriodEnd);
            if (result != null)
            {
                if (!result.Any())
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

        private bool ValidateIfExistOneChangeInCertificationDate3(Member objeto, ref bool isAvailableForChange, ref string reason, ref string reasonEN, DateTime openEnrollmentPeriodStart, DateTime openEnrollmentPeriodEnd, ref int code)
        {
            bool resultFinal = true;
            IQueryable<EnrollmentHistory> result = repository2.FindWithoutEResponse(x => x.MemberId == objeto.Id && x.Enabled == true && x.Status.AllowChange == true && x.CreatedOn <= openEnrollmentPeriodStart);
            if (result != null)
            {
                if (!result.Any())
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

        public EResponseBase<Member> ChangeEnrollmentReject(int memberId, int? IdHistories, int? IdStatus, int? mcoId, int? pmgId, int? pcpId, int? ppcpId, bool permission, int justCause, DataSource origin, string userName, string justCauseComment, bool ignoreValidationRules = false)
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
                        int? currentPcpId = member.PCPId;
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
                                if (mcoId == currentMcoId && pmgId == currentPmgId && pcpId == currentPcpId)
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

                                    if (pcpId == currentPcpId)
                                    {
                                        PcpEffectiveDate = currentPcpEffectiveDate;
                                        PCPChange = false;
                                    }
                                    else
                                    {
                                        PcpEffectiveDate = setEffectiveDate(DateTime.Now);
                                    }

                                    member.UpdatedBy = userName;
                                    DateTime fecha_actual = DateTime.Now;
                                    if (fecha_actual.Day > 21)
                                    {
                                        member.NewCertificationDate = new DateTime(fecha_actual.Year, fecha_actual.Month + 1, 1);
                                    }
                                    member.UpdatedOn = DateTime.Now;
                                    //"0000"
                                    member.MemberPrimaryCenter = repository4.FirstOrDefault(x => x.Id == pmgId).objeto.PmgCode;
                                    result = repository.Update(member);

                                    //repository.SaveChanges();
                                    var restulHistory = UpdatetHistory(origin, userName, member, (int)IdHistories, (int)IdStatus, pmgId, pcpId, mcoId, justCause, MCOChange, PCPChange, PMGChange, justCauseComment);
                                    //var restulHistory = InsertHistory(origin, userName, member, pmgId, ppcpId, mcoId, justCause, MCOChange, PCPChange, PMGChange, justCauseComment);
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
    }
}
