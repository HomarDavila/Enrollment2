using Common;
using Common.Logging;
using Domain.Entity_Models;
using Domain.Entity_Models.Identity;
using Infraestructure.Context;
using Infraestructure.Repositories;
using Mehdime.Entity;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.Implementations
{
    public class OptionRolService : IOptionRolService
    {
        private readonly IDbContextScopeFactory dbContextScopeFactory;
        private readonly IRepository<OptionRol, ApplicationDbContext> repository;
        private readonly IRepository<Role, Infraestructure.Context.ApplicationDbContext> repository2;
        private readonly IRepository<Application, Infraestructure.Context.ApplicationDbContext> repository3;
        private readonly IRepository<Option, Infraestructure.Context.ApplicationDbContext> repository4;

        private readonly IConfigurationLib config;
        public ITransaction Transaction { get; set; }
        public ICustomLog Logger { get; set; }

        public OptionRolService(
           IDbContextScopeFactory _dbContextScopeFactory,
           IRepository<OptionRol, ApplicationDbContext> _repository,
           IRepository<Role, ApplicationDbContext> _repository2,
           IRepository<Application, ApplicationDbContext> _repository3,
           IRepository<Option, ApplicationDbContext> _repository4,
           IConfigurationLib _config
       )
        {
            dbContextScopeFactory = _dbContextScopeFactory;
            repository = _repository;
            repository2 = _repository2;
            repository3 = _repository3;
            repository4 = _repository4;
            config = _config;
        }

        public EResponseBase<OptionRol> Insert(List<OptionRol> model)
        {
            List<string> errorList = new List<string>();
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<OptionRol> rh = new EResponseBase<OptionRol>();
            try
            {
                using (IDbContextScope ctx = dbContextScopeFactory.Create())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(model, printDebug: true);
                    errorList = validateInsert(model, errorList);
                    if (errorList.Any()) rh = new UtilitariesResponse<OptionRol>(config).setResponseBaseForValidationExceptionString(errorList);
                    else rh = repository.Insert(model);
                    ctx.SaveChanges();
                    Logger.Print_Response(rh, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                rh = new UtilitariesResponse<OptionRol>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return rh;
        }

        public EResponseBase<OptionRol> Delete(OptionRol model)
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<OptionRol> rh = new EResponseBase<OptionRol>();
            try
            {
                using (IDbContextScope ctx = dbContextScopeFactory.Create())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(model, printDebug: true);
                    OptionRol entityToDelete = repository.SingleOrDefaultWithoutEResponse(filter: x => (x.ApplicationId == model.ApplicationId && x.OptionId == model.OptionId && x.RolId == model.RolId));
                    rh = repository.Delete(entityToDelete);
                    ctx.SaveChanges();
                    Logger.Print_Response(rh, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                rh = new UtilitariesResponse<OptionRol>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return rh;
        }

        private List<string> validateInsert(List<OptionRol> model, List<string> errorList)
        {
            foreach (OptionRol optionRol in model)
            {
                errorList = isEnabledRol(optionRol.RolId, errorList);
                errorList = isEnabledApplication(optionRol.ApplicationId, errorList);
                errorList = isEnabledoption(optionRol.OptionId, errorList);
            }
            return errorList;
        }

        private List<string> isEnabledoption(int optionId, List<string> errorList)
        {
            EResponseBase<Option> response = repository4.FirstOrDefault(x => x.Id == optionId);
            if (response.Code != config.CodigoExito)
                errorList.Add(String.Format("Option {0} not found", optionId));
            else
            {
                if (response.objeto == null)
                    errorList.Add(String.Format("Option {0} not found", optionId));
                else
                {
                    if (!response.objeto.Enabled.Value)
                        errorList.Add(String.Format("Option {0} is not active", optionId));
                }
            }

            return errorList;
        }

        private List<string> isEnabledApplication(int applicationId, List<string> errorList)
        {
            EResponseBase<Application> response = repository3.FirstOrDefault(x => x.Id == applicationId);
            if (response.Code != config.CodigoExito)
                errorList.Add(String.Format("Application {0} not found", applicationId));
            else
            {
                if (response.objeto == null)
                    errorList.Add(String.Format("Application {0} not found", applicationId));
                else
                {
                    if (!response.objeto.Enabled.Value)
                        errorList.Add(String.Format("Application {0} is not active", applicationId));
                }
            }

            return errorList;
        }

        private List<string> isEnabledRol(int rolId, List<string> errorList)
        {
            EResponseBase<Role> response = repository2.FirstOrDefault(x => x.Id == rolId);
            if (response.Code != config.CodigoExito)
                errorList.Add(String.Format("Rol {0} not found", rolId));
            else
            {
                if (response.objeto == null)
                    errorList.Add(String.Format("Rol {0} not found", rolId));
                else
                {
                    if (!response.objeto.Enabled.Value)
                        errorList.Add(String.Format("Rol {0} is not active", rolId));
                }
            }

            return errorList;
        }

    }
}
