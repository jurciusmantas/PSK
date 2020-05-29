using PSK.Model.DTO;
using PSK.Model.IServices;
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
        public ServerResult<User> Login(LoginArgs args)
        {
            try
            {
                ServerResult<User> result = _decoratee.Login(args);

                if (!result.Success)
                {
                    _logger.Information("{Login}: {DecorateeClassName}.Login unsuccessful", args.Login, _decorateeClassName);
                    return result;
                }
                _logger.Information("{Login}: {DecorateeClassName}.Login() successful", result.Data.Employee.Name, _decorateeClassName);
                return result;
            }
            catch (Exception e)
            {
                _logger.Error(e, "{Login}: {DecorateeClassName}.Login failed {NewLine} {Exception}", args.Login, _decorateeClassName);
                throw;
            }
        }

        public ServerResult<User> LoginToken(string token)
        {
            try
            {
                ServerResult<User> result = _decoratee.LoginToken(token);
                _logger.Information("{Login}: {DecorateeClassName}.LoginToken success", result.Data.Employee.Name, _decorateeClassName);
                if (!result.Success)
                {
                    _logger.Information("{DecorateeClassName}.LoginToken unsuccessful", _decorateeClassName);
                    return result;
                }
                return result;
            }
            catch (Exception e)
            {
                _logger.Error(e, "{DecorateeClassName}.LoginToken failed {NewLine} {Exception}", _decorateeClassName);
                throw;
            }
        }

        public void Logout()
        {
            try
            {
                _decoratee.Logout();
                _logger.Information("{DecorateeClassName}.Logout success", _decorateeClassName);
            }
            catch (Exception e)
            {
                _logger.Error(e, "{DecorateeClassName}.Logout failed {Newline} {Exception}", _decorateeClassName, e);
                throw;
            }
        }
    }
}
