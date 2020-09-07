using Common;
using Common.Logging;
using Domain.Entity_Models.Identity;

namespace Service.Interfaces
{
    public interface IOptionService
    {
        EResponseBase<Option> Get();
        EResponseBase<Option> Get(int id);
        EResponseBase<Option> GetByCode(string code);
        EResponseBase<Option> GetByTypeId(int typeId);
        EResponseBase<Option> InsertOrUpdate(Option model);
        EResponseBase<Option> Delete(int id);
        EResponseBase<Option> Delete(Option model);
        EResponseBase<Option> Disabled(int id, bool enabled);
        ITransaction Transaction { get; set; }
        ICustomLog Logger { get; set; }
    }
}
