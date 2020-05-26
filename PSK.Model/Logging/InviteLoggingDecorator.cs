using PSK.Model.DTO;
using PSK.Model.Services;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.Model.Logging
{
    class InviteLoggingDecorator : IInviteService
    {
        private readonly IInviteService _decoratee;
        private readonly ILogger _logger;
        private readonly string _decorateeClassName;

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
                _logger.Information("User {User}: invite {Email}", "TODO", args.Email);
                return _decoratee.Invite(args);
            }
            catch (Exception e)
            {
                _logger.Error(e, "User {User}: invite {Email} failed: {Newline}, {Exception}", "TODO", args.Email);
                throw;
            }
        }
    }
}
