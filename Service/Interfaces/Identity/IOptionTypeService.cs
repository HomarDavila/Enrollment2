using Common;
using Common.Logging;
using Domain.Entity_Models.Identity;

namespace Service.Interfaces
{
    public interface IOptionTypeService
    {
        EResponseBase<OptionType> Get();
        EResponseBase<OptionType> Get(int id);
        EResponseBase<OptionType> GetByCode(string code);
        EResponseBase<OptionType> InsertOrUpdate(OptionType model);
        EResponseBase<OptionType> Delete(int id);
        EResponseBase<OptionType> Delete(OptionType model);
        EResponseBase<OptionType> Disabled(int id, bool enabled);
        ITransaction Transaction { get; set; }
        ICustomLog Logger { get; set; }
    }
}
