using Common;
using Common.Logging;
using Domain.Entity_Models;
using System.Collections.Generic;

namespace Service.Interfaces
{
    public interface IConfigurationService
    {
        EResponseBase<Configuration> Get();
        EResponseBase<Configuration> Get(int id);
        EResponseBase<Configuration> GetByFilters(string searchText);
        EResponseBase<Configuration> GetByFilters(string name, string code, string description);
        EResponseBase<Configuration> InsertOrUpdate(Configuration model);
        EResponseBase<Configuration> InsertOrUpdate(List<Configuration> model);
        EResponseBase<Configuration> Delete(int id);
        EResponseBase<Configuration> Delete(Configuration model);
        EResponseBase<Configuration> Disabled(int id, bool enabled);
        ITransaction Transaction { get; set; }
        ICustomLog Logger { get; set; }
    }
}
