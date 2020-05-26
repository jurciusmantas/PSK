using PSK.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.Model.Services
{
    public interface IRestrictionService
    {
        ServerResult CreateRestriction(RestrictionArgs restrictionArgs);
        ServerResult CreateGlobalRestriction();
        ServerResult<Restriction> GetRestriction(int employeeId);
        ServerResult<List<Restriction>> GetCreatedRestrictions(int employeeId);
        ServerResult DeleteRestriction(int id);
        ServerResult<List<User>> GetLowerUsers(int currentUserId);

    }
}
