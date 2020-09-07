using Common;
using Common.Logging;
using Domain.Entity_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IManagedCareOrganizationServices
    {
        EResponseBase<ManagedCareOrganization> Get();
        ITransaction Transaction { get; set; }
        ICustomLog Logger { get; set; }
    }
}
