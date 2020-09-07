using Common;
using Common.Logging;
using Domain.Entity_Models;
using System.Collections.Generic;

namespace Service.Interfaces
{
    public interface IConfigurationDetailService
    {
        EResponseBase<ConfigurationDetail> Get();
        EResponseBase<ConfigurationDetail> Get(int id);
        EResponseBase<ConfigurationDetail> GetByCode(string code);
        EResponseBase<ConfigurationDetail> GetByConfigurationId(int configurationId);
        EResponseBase<ConfigurationDetail> GetByConfigurationCode(string configurationCode);
        EResponseBase<ConfigurationDetail> InsertOrUpdate(ConfigurationDetail model);
        EResponseBase<ConfigurationDetail> InsertOrUpdate(List<ConfigurationDetail> model);
        EResponseBase<ConfigurationDetail> Delete(int id);
        EResponseBase<ConfigurationDetail> Delete(ConfigurationDetail model);
        EResponseBase<ConfigurationDetail> Disabled(int id, bool enabled);
        ITransaction Transaction { get; set; }
        ICustomLog Logger { get; set; }
    }
}
