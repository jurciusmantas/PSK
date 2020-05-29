using PSK.Model.IServices;
using PSK.Model.Logging;
using PSK.Model.Repository;
using PSK.Model.Services;
using Serilog;
using SimpleInjector;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace PSK.Model
{
    public class ObjectContainer
    {
        public static void InitializeContainer(Container container, string logFile, LogLevel logLevel, 
            string[] pluginsDllPaths)
        {

            container.Options.AllowOverridingRegistrations = true;
            container.Register<ILoginService, LoginService>(Lifestyle.Scoped);
            container.Register<IInviteService, InviteService>(Lifestyle.Scoped);
            container.Register<ITopicService, TopicService>(Lifestyle.Scoped);
            container.Register<IRecommendationsService, RecommendationsService>(Lifestyle.Scoped);
            container.Register<IRegistrationService, RegistrationService>(Lifestyle.Scoped);
            container.Register<ILearningDayService, LearningDayService>(Lifestyle.Scoped);
            container.Register<IEmployeesService, EmployeesService>(Lifestyle.Scoped);
            container.Register<IRestrictionService, RestrictionService>(Lifestyle.Scoped);

            InitializeLogging(logFile, logLevel);
            
            container.RegisterInstance(Log.Logger);

            InitializePlugins<IEmployeesService>(container, "Service", pluginsDllPaths[0], Lifestyle.Scoped);

            container.RegisterDecorator<ILoginService, LoginLoggingDecorator>(Lifestyle.Scoped);
            container.RegisterDecorator<IInviteService, InviteLoggingDecorator>(Lifestyle.Scoped);
            container.RegisterDecorator<ITopicService, TopicLoggingDecorator>(Lifestyle.Scoped);
            container.RegisterDecorator<IRecommendationsService, RecommendationsLoggingDecorator>(Lifestyle.Scoped);
            container.RegisterDecorator<IRegistrationService, RegistrationLoggingDecorator>(Lifestyle.Scoped);
            container.RegisterDecorator<ILearningDayService, LearningDayLoggingDecorator>(Lifestyle.Scoped);

            InitializePlugins<IEmployeeRepository>(container, "Repository", pluginsDllPaths[1], Lifestyle.Scoped);
            
        }

        private static void InitializePlugins<T>(Container container, string classNameEndsWith,
            string pluginsDirectoryPath, Lifestyle lifestyle)
        {
            if (pluginsDirectoryPath != "")
            {
                try
                {
                    var abstractions = (
                        from type in typeof(T).Assembly.GetExportedTypes()
                        where type.IsInterface
                        where type.Name.EndsWith(classNameEndsWith)
                        select type).ToArray();

                    var pluginAssemblies =
                        from file in new DirectoryInfo(pluginsDirectoryPath).GetFiles()
                        where file.Extension.ToLower() == ".dll"
                        select Assembly.LoadFrom(file.FullName);

                    var implementationTypes =
                        from assembly in pluginAssemblies
                        from type in assembly.GetExportedTypes()
                        where abstractions.Any(r => r.IsAssignableFrom(type))
                        where !type.IsAbstract
                        where !type.IsGenericTypeDefinition
                        select type;

                    foreach (var type in implementationTypes)
                    {
                        var abstraction = abstractions.Single(r => r.IsAssignableFrom(type));
                        container.Register(abstraction, type, lifestyle);
                    }
                }
                catch(Exception e)
                {
                    Log.Logger.Error("Injection " + classNameEndsWith + " from outside dll failed. Details: " + e.Message);
                }
            }
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
