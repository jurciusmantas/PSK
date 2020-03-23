using Castle.DynamicProxy;
using PSK.Model.Entities;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.Model.Logging
{
    class LoggingInterceptor : IInterceptor
    {
        private readonly ILogger _logger;
        public LoggingInterceptor(ILogger logger)
        {
            _logger = logger;
        }
        public void Intercept(IInvocation invocation)
        {
            string username = ((LoginArgs)invocation.Arguments[0]).Login; // ugly af and probably not safe. should be a better way of doing this
            _logger.Information("{0} {1}: {3}.{4}", 
                DateTime.Now,
                username,
                invocation.Method.DeclaringType.Name,
                invocation.Method);
        }
    }
}
