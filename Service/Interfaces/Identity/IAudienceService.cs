using Common;
using Common.Logging;
using Domain.Entity_Models.Identity;

namespace Service.Interfaces
{
    public interface IAudienceService
    {
        EResponseBase<Audience> Register(string name, string base64);
        EResponseBase<Audience> Get(string clientId);
        ITransaction Transaction { get; set; }
        ICustomLog Logger { get; set; }
    }
}
