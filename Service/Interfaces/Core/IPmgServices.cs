using Common;
using Common.Logging;
using Core.API.Model.Response;
using Domain.Custom_Models;
using Domain.Entity_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IPmgServices
    {
        EResponseBase<PrimaryMedicalGroup> Get(bool ShowForChangeEnrollmentProcess);
        ITransaction Transaction { get; set; }
        ICustomLog Logger { get; set; }
    }
}
