using Common.HttpHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{

    public class EResponseBase<TEntity> : ICloneable where TEntity : class, new()
    {
        public int? Code { get; set; }
        public bool IsOK { get; set; }
        public string Message { get; set; }
        public string MessageEN { get; set; }

        public bool IsResultList { get; set; } = false;
        public IEnumerable<TEntity> listado { get; set; }
        public TEntity objeto { get; set; }

        public Exception TechnicalErrors { get; set; }
        public List<SimpleEntity> FunctionalErrors { get; set; }

        public object Clone()
        {
            return MemberwiseClone();
        }

        public override string ToString()
        {
            return string.Format("Response[Code: {0}, Message: {1},  listado: {2} , objeto {3}]", Code, Message, listado, objeto);
        }

    }
}
