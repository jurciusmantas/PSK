using PSK.Model.Entities;
using System;
using System.Collections.Generic;

namespace PSK.Model.Repository
{
    public interface IRestrictionRepository : IRepository<Restriction>
    {
        public (List<Restriction>, List<List<string>>) GetRestrictionsTo(int creatorId);
        public Restriction GetLastGlobal();
    }
}
