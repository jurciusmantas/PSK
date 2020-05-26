using PSK.Model.Entities;
using System.Collections.Generic;

namespace PSK.Model.Repository
{
    public interface IRestrictionRepository : IRepository<Restriction>
    {
        public Restriction GetLastGlobal();

        public List<Restriction> GetCreatedRestrictions(int creatorId);
    }
}
