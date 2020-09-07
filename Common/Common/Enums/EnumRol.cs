using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Enums
{
    public static class EnumRol
    {
        public enum Role
        {
            AdministradorSelfService = 1,
            AdministradorSystem = 2,
            UserEnrollmentSelfService = 3,
            Consuler = 4,
            CallCenter = 5
        }
        public enum Application
        {
            EnrollmentSelfService=1,
            EnrollmentSystem=2
        }
    }
}
