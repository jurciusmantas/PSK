using PSK.Model.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.Model.Services
{
    public interface IRestrictionService
    {
        ServerResult CreateRestriction(RestrictionArgs restrictionArgs);
        ServerResult CreateGlobalRestriction();
        ServerResult<Restrictions> GetRestrictions(int employeeId);
        ServerResult DeleteRestriction(int id);

    }
}
