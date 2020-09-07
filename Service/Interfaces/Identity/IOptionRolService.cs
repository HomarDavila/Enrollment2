using Common;
using Common.Logging;
using Domain.Entity_Models.Identity;
using System.Collections.Generic;

namespace Service.Interfaces
{
    public interface IOptionRolService
    {
        EResponseBase<OptionRol> Insert(List<OptionRol> model);
        EResponseBase<OptionRol> Delete(OptionRol model);
        ITransaction Transaction { get; set; }
        ICustomLog Logger { get; set; }
    }
}
