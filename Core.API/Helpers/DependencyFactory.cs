using LightInject;
using Service.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Service.DependecyInjection
{
    public class DependencyFactory
    {
        public static T GetInstance<T>()
        {
            ServiceContainer container = new ServiceContainer();
            container.RegisterAssembly(typeof(ServiceRegister).GetTypeInfo().Assembly);
            try
            {
                return container.GetInstance<T>();
            }
            catch
            {
                throw;
            }

        }
    }
}
