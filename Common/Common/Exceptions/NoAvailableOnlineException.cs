using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Exceptions
{
    public class NoAvailableOnlineException : Exception
    {
        public NoAvailableOnlineException()
        {
        }

        public NoAvailableOnlineException(string message)
            : base(message)
        {
        }

        public NoAvailableOnlineException(string message, Exception inner)
            : base(message, inner)
        {
        }

    }
}
