using PSK.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.Model.IServices
{
    public interface IRestrictionService
    {
        ServerResult CreateRestriction(RestrictionArgs restrictionArgs);
        ServerResult<List<Restriction>> GetRestrictionsTo(int employeeId);
        ServerResult<Restriction> GetRestrictionFrom(int employeeId);
        ServerResult DeleteRestriction(int id, int employeeId);

    }
}
