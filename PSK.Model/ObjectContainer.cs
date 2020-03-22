﻿using PSK.Model.DBConnection;
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
            container.Register<IDBConnection, MockDBConnection>(Lifestyle.Singleton);

            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("log.log")
                .MinimumLevel.Verbose()
                .CreateLogger();
            container.RegisterInstance(Log.Logger);
        }
    }
}
