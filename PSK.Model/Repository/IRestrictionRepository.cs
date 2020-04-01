using PSK.Model.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.Model.Repository
{
    public interface IRestrictionRepository
    {
        public Restriction Add(Restriction restriction);
        public Restriction GetRestriction(int id);
        public Restriction UpdateRestriction(Restriction updatedRestriction);
        public Restriction Delete(int id);
    }
}
