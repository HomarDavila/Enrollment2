using Common;
using Domain.Custom_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ICityServices
    {
        EResponseBase<CitiesResponse> GetCities(string transactionId);
        string Transaction { get; set; }
        
    }
}
