using Common;
using Common.Logging;
using Domain.Custom_Models;
using Domain.Entity_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IFamilyServices
    {
        EResponseBase<Family> Get();
        EResponseBase<Family> Get(int FamilyId);
        ITransaction Transaction { get; set; }
        ICustomLog Logger { get; set; }
    }
}
