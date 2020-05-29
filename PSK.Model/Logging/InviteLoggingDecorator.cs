using PSK.Model.DTO;
using PSK.Model.IServices;
using Serilog;
using System;

namespace PSK.Model.Logging
{
    class InviteLoggingDecorator : IInviteService
    {
        private readonly IInviteService _decoratee;
        private readonly ILogger _logger;

        public InviteLoggingDecorator(IInviteService inviteService, ILogger logger)
        {
            _decoratee = inviteService;
            _logger = logger;
        }

        public ServerResult<Invite> Invite(Invite args)
        {
            try
            {
                // TODO: get user id
                _logger.Information("{User}: invite {Email}", "TODO", args.Email);
                return _decoratee.Invite(args);
            }
            catch (Exception e)
            {
                _logger.Error(e, "{User}: invite {Email} failed: {Newline} {Exception}", "TODO", args.Email);
                throw;
            }
        }
    }
}
