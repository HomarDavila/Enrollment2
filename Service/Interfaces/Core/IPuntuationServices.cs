using Common;
using Common.Logging;
using Domain.Entity_Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces.Core
{
    public interface IPuntuationServices
    {
        EResponseBase<Puntuation> Get();
        EResponseBase<Puntuation> GetById(int Id);
        ITransaction Transaction { get; set; }
        ICustomLog Logger { get; set; }
    }
}
