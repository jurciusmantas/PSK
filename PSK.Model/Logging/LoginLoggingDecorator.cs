using PSK.Model.Entities;
using PSK.Model.Services;
using Serilog;
using System;

namespace PSK.Model.Logging
{
    class LoginLoggingDecorator : ILoginService
    {
        private readonly ILoginService _decoratee;
        private readonly ILogger _logger;
        private readonly string _decorateeClassName;

        public LoginLoggingDecorator(ILoginService decoratee, ILogger logger)
        {
            _decoratee = decoratee;
            _logger = logger;
            _decorateeClassName = decoratee.ToString();
        }
        // get called twice for some reason...
        public ServerResult<User> Login(LoginArgs args)
        {
            try
            {
                ServerResult<User> result = _decoratee.Login(args);
                _logger.Information("{Timestamp} {Login}: {DecorateeClassName}.Login success", result.Data.Login, _decorateeClassName);
                return result;
            }
            catch (ArgumentNullException e)
            {
                _logger.Information(e, "{Timestamp} {Login}: {DecorateeClassName}.Login failed {Newline} {Exception}", args.Login, _decorateeClassName);
                throw; // perhaps we'd like to hide original exception from the database and 'throw e;' would be more suitable
            }
        }

        public ServerResult<User> LoginToken(string token)
        {
            try
            {
                ServerResult<User> result = _decoratee.LoginToken(token);
                _logger.Information("{Timestamp} {Login}: {DecorateeClassName}.LoginToken success", result.Data.Login, _decorateeClassName);
                return result;
            }
            catch (ArgumentNullException e)
            {
                _logger.Information(e, "{Timestamp}: {DecorateeClassName}.LoginToken failed {Newline} {Exception}", _decorateeClassName);
                throw; // perhaps we'd like to hide original exception from the database and 'throw e;' would be more suitable
            }
        }
    }
}
