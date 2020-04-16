using PSK.Model.Services;
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
        }
    }
}
