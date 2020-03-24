using PSK.Model.DBConnection;
using PSK.Model.Logging;
using PSK.Model.Services;
using Serilog;
using SimpleInjector;

namespace PSK.Model
{
    public class ObjectContainer
    {
        public static void InitializeContainer(Container container)
        {
            container.Register<ILoginService, LoginService>(Lifestyle.Scoped);
            container.Register<IInviteService, InviteService>(Lifestyle.Scoped);
            container.Register<IDBConnection, MockDBConnection>(Lifestyle.Singleton);
            container.Register<ITopicService, TopicService>(Lifestyle.Scoped);

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("log.log")
                .MinimumLevel.Verbose() // dev only
                .CreateLogger();
            container.RegisterInstance(Log.Logger);

            container.RegisterDecorator<ILoginService, LoginLoggingDecorator>(Lifestyle.Scoped);
        }
    }
}
