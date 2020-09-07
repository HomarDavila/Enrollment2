using Common;
using Common.Others;
using Domain.EntityModel;
using Infraestructure.Repositories;
using NLog;
using Persistence.DbContextScope;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public class DemoService  : IDemoService
    {
        private static ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IRepository<Demo> _demoRepository;

        public DemoService(
           IDbContextScopeFactory dbContextScopeFactory,
           IRepository<Demo> demoRepository
       )
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _demoRepository = demoRepository;
        }

        public EResponseBase<Demo> GetAll(string transactionId)
        {            
            var result = new EResponseBase<Demo>();
            try
            {
                using (var ctx = _dbContextScopeFactory.CreateReadOnly())
                {                    
                    result = _demoRepository.Find();
                }
            }
            catch (Exception e)
            {
                result = UtilitariesResponse<Demo>.setResponseBaseForException(e);
                logger.Error(e.CustomErrorMessage(transactionId));
            }

            return result;
        }

        public EResponseBase<Demo> Get(string transactionId, int id)
        {            
            var result = new EResponseBase<Demo>();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    result = _demoRepository.SingleOrDefault(x => x.DemoId == id);
                }
            }
            catch (Exception e)
            {
                result = UtilitariesResponse<Demo>.setResponseBaseForException(e);
                logger.Error(e.CustomErrorMessage(transactionId));
            }

            return result;
        }

        public EResponseBase<Demo> InsertOrUpdate(string transactionId, Demo model)
        {
            var rh = new EResponseBase<Demo>();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    rh = _demoRepository.InsertOrUpdate(model, model.DemoId);
                    ctx.SaveChanges();                    
                }
            }
            catch (Exception e)
            {
                rh = UtilitariesResponse<Demo>.setResponseBaseForException(e);
                logger.Error(e.CustomErrorMessage(transactionId));
            }

            return rh;
        }

        public EResponseBase<Demo> Delete(string transactionId, int id)
        {
            var rh = new EResponseBase<Demo>();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    var model = _demoRepository.SingleOrDefault(x => x.DemoId == id);
                    rh = _demoRepository.Delete(model);
                    ctx.SaveChanges();
                }
            }
            catch (Exception e)
            {
                rh = UtilitariesResponse<Demo>.setResponseBaseForException(e);
                logger.Error(e.CustomErrorMessage(transactionId));
            }

            return rh;
        }

        public EResponseBase<Demo> Delete(string transactionId, Demo model)
        {
            var rh = new EResponseBase<Demo>();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {                    
                    rh = _demoRepository.Delete(model);
                    ctx.SaveChanges();
                }
            }
            catch (Exception e)
            {
                rh = UtilitariesResponse<Demo>.setResponseBaseForException(e);
                logger.Error(e.CustomErrorMessage(transactionId));
            }

            return rh;
        }
    }
}
