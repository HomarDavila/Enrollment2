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
    public interface IFileServices
    {
        EResponseBase<Files> Get();
        EResponseBase<Files> GetFile(int fileId);
        EResponseBase<Files> GetEnrollmentFiles(int memberId);
        EResponseBase<Files> Disabled(int id, bool enabled);
        EResponseBase<Files> InsertOrUpdate(Files model);
        ITransaction Transaction { get; set; }
        ICustomLog Logger { get; set; }
    }
}
