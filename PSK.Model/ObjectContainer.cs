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
            container.Register<ITopicService, TopicService>(Lifestyle.Scoped);
            container.Register<IRecommendationService, RecommendationService>(Lifestyle.Scoped);
            container.Register<IRegistrationService, RegistrationService>(Lifestyle.Scoped);

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("log.log")
                .MinimumLevel.Verbose() // dev only
                .CreateLogger();
            container.RegisterInstance(Log.Logger);

            container.RegisterDecorator<ILoginService, LoginLoggingDecorator>(Lifestyle.Scoped);
        }
    }
}
