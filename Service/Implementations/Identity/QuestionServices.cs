using Common;
using Common.Logging;
using Domain.Custom_Models;
using Domain.Entity_Models;
using Domain.Entity_Models.Identity;
using Infraestructure.Context;
using Infraestructure.Repositories;
using Mehdime.Entity;
using Service.Interfaces.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations.Identity
{
    public class QuestionServices : IQuestionService
    {
        private readonly IDbContextScopeFactory dbContextScopeFactory;
        private readonly IRepository<Configuration, ApplicationDbContext> repository;
        private readonly IConfigurationLib config;
        public ICustomLog Logger { get; set; }
        public ITransaction Transaction { get; set; }

        public QuestionServices(
            IDbContextScopeFactory _dbContextScopeFactory,
           IRepository<Configuration, ApplicationDbContext> _repository,
           IConfigurationLib _config
        )
        {
            dbContextScopeFactory = _dbContextScopeFactory;
            repository = _repository;
            config = _config;
        }

        public EResponseBase<SecurityAnswer> GetSecurityQuestions()
        {
            Logger.InitializeLog(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, Transaction);
            EResponseBase<SecurityAnswer> result = new EResponseBase<SecurityAnswer>();
            List<SecurityAnswer> resultList = new List<SecurityAnswer>();
            try
            {
                using (IDbContextReadOnlyScope ctx = dbContextScopeFactory.CreateReadOnly())
                {
                    Logger.Print_InitMethod();
                    Logger.Print_Request(null, printDebug: true);
                    Configuration resultTemp = repository.FirstOrDefaultWithoutEResponse(includeProperties: x => x.Configurations, filter: x => x.Code == AppConstants.SecurityAnswersCode);
                    foreach (ConfigurationDetail obj in resultTemp.Configurations)
                    {
                        SecurityAnswer securityAnswer = new SecurityAnswer()
                        {
                            QuestionID = Convert.ToInt16(obj.AdditionalNumericValue),
                            QuestionES = obj.AdditionalStringValue,
                            QuestionEN = obj.StringValue
                        };
                        resultList.Add(securityAnswer);
                    }
                    result = new UtilitariesResponse<SecurityAnswer>(config).setResponseBaseForList(resultList);
                    Logger.Print_Response(result, printDebug: true);
                    Logger.Print_EndMethod();
                }
            }
            catch (Exception e)
            {
                result = new UtilitariesResponse<SecurityAnswer>(config).setResponseBaseForException(e);
                Logger.Error(e.Message);
            }

            return result;
        }
    }
}
