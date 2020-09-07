using Common;
using Common.AspNet.Logging;
using Common.Logging;
using LightInject;
using log4net;

namespace Infraestructure.Ioc
{
    public class InfraestructureRegister : ICompositionRoot
    {
        public void Compose(IServiceRegistry container)
        {
            container.Register<ICustomLog, CustomLog4Net>();
            container.Register<IConfigurationLib, ConfigurationLib>();
        }
    }
}
