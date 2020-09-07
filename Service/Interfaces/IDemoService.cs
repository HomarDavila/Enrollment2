using Common;
using Domain.EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IDemoService
    {
        EResponseBase<Demo> GetAll(string transactionId);
        EResponseBase<Demo> Get(string transactionId, int id);
        EResponseBase<Demo> InsertOrUpdate(string transactionId, Demo model);
        EResponseBase<Demo> Delete(string transactionId, int id);
        EResponseBase<Demo> Delete(string transactionId, Demo model);
    }
}
