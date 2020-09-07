using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    /// <summary>
    /// Catch NotFound Exception into WebServices
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class WSNotFoundException : Exception
    {
        public WSNotFoundException()
        {
        }

        public WSNotFoundException(string message)
            : base(message)
        {
        }

        public WSNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
