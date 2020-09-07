using Common;
using Common.Logging;
using Domain.Custom_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces.Identity
{
    public interface IQuestionService
    {
        EResponseBase<SecurityAnswer> GetSecurityQuestions();
        ITransaction Transaction { get; set; }
        ICustomLog Logger { get; set; }
    }
}
