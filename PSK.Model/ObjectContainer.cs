using PSK.Model.Logging;
using PSK.Model.Services;
using Serilog;
using SimpleInjector;
using System;

namespace PSK.Model
{
    public class ObjectContainer
    {
        public static void InitializeContainer(Container container, string logFile, LogLevel logLevel)
        {
            container.Register<ILoginService, LoginService>(Lifestyle.Scoped);
            container.Register<IInviteService, InviteService>(Lifestyle.Scoped);
            container.Register<ITopicService, TopicService>(Lifestyle.Scoped);
            container.Register<IRecommendationService, RecommendationService>(Lifestyle.Scoped);
            container.Register<IRegistrationService, RegistrationService>(Lifestyle.Scoped);
            container.Register<ILearningDayService, LearningDayService>(Lifestyle.Scoped);

            InitializeLogging(logFile, logLevel);
            
            container.RegisterInstance(Log.Logger);

            container.RegisterDecorator<ILoginService, LoginLoggingDecorator>(Lifestyle.Scoped);
            container.RegisterDecorator<IInviteService, InviteLoggingDecorator>(Lifestyle.Scoped);
        }

        private static void InitializeLogging(string logFile, LogLevel logLevel)
        {
            LoggerConfiguration logConfig = new LoggerConfiguration()
                .WriteTo.File(logFile);
            switch (logLevel)
            {
                case LogLevel.Debug:
                    logConfig.MinimumLevel.Debug();
                    break;
                case LogLevel.Verb:
                    logConfig.MinimumLevel.Verbose();
                    break;
                case LogLevel.Info:
                    logConfig.MinimumLevel.Information();
                    break;
                case LogLevel.Warn:
                    logConfig.MinimumLevel.Warning();
                    break;
                case LogLevel.Err:
                    logConfig.MinimumLevel.Error();
                    break;
                default:
                    throw new NotImplementedException();
            }
            Log.Logger = logConfig.CreateLogger();
        }
    }
}
