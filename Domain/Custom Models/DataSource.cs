using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Custom_Models
{
    public enum DataSource : byte
    {
        //None = 0,     
        //Medicaid = 10,
        //Counselor = 20,
        //Web = 30,
        //Ases = 100
        None = 0,
        Medicaid = 10,
        Counselor = 20,
        SelfService = 30,
        Assist = 40,
        Ases = 100
    }
}
