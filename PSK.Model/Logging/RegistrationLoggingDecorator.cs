using PSK.Model.DTO;
using PSK.Model.Services;
using Serilog;
using System;

namespace PSK.Model.Logging
{
    class RegistrationLoggingDecorator : IRegistrationService
    {
        private readonly IRegistrationService _decoratee;
        private readonly ILogger _logger;
        private readonly string _decorateeClassName;

        public RegistrationLoggingDecorator(IRegistrationService decoratee, ILogger logger)
        {
            _decoratee = decoratee;
            _logger = logger;
            _decorateeClassName = decoratee.ToString();
        }

        public ServerResult AddNewUser(Registration args)
        {
            try
            {
                ServerResult result = _decoratee.AddNewUser(args);

                if (!result.Success)
                {
                    _logger.Information("{Email}: {DecorateeClassName}.AddNewUser unsuccessful", args.Email, _decorateeClassName);
                    return result;
                }
                _logger.Information("{Email}: {DecorateeClassName}.AddNewUser successful", args.Email, _decorateeClassName);
                return result;
            }
            catch (Exception e)
            {
                _logger.Error(e, "{Email}: {DecorateeClassName}.AddNewUser failed {NewLine} {Exception}", args.Email, _decorateeClassName); ;
                throw;
            }
        }

        public ServerResult<string> GetEmailFromToken(string token)
        {
            try
            {
                ServerResult<string> result = _decoratee.GetEmailFromToken(token);

                if (!result.Success)
                {
                    _logger.Information("{DecorateeClassName}.GetEmailFromToken unsuccessful", _decorateeClassName);
                    return result;
                }
                _logger.Information("{DecorateeClassName}.GetEmailFromToken successful", _decorateeClassName);
                return result;
            }
            catch (Exception e)
            {
                _logger.Error(e, "{DecorateeClassName}.GetEmailFromToken failed {NewLine} {Exception}", _decorateeClassName); ;
                throw;
            }
        }
    }
}
