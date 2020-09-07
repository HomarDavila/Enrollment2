using Common;
using Common.Logging;
using Domain.Entity_Models;
using System;

namespace Service.Interfaces
{
    public interface IReportsServices
    {
        ITransaction Transaction { get; set; }
        ICustomLog Logger { get; set; }
        EResponseBase<EnrollmentStatistics> GetReportsWeb(EnrollmentStatistics request);
        EResponseBase<EnrollmentStatistics> InsertStatistic(EnrollmentStatistics request);
    }
}
