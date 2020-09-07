using Common;
using Common.Logging;
using Domain.Entity_Models;

namespace Service.Interfaces
{
    public interface IMunicipalityServices
    {
        EResponseBase<Municipality> Get();
        ITransaction Transaction { get; set; }
        ICustomLog Logger { get; set; }
    }
}
