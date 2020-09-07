using LightInject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Ioc
{
    public class DependencyLocator
    {
        public static T GetInstance<T>()
        {
            ServiceContainer container = new ServiceContainer();
            container.RegisterAssembly(typeof(InfraestructureRegister).GetTypeInfo().Assembly);
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
