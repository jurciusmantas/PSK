using PSK.Model.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.Model.Repository
{
    public interface IPlanRepository
    {
        public Plan Add(Plan plan);
        public Plan GetPlan(int id);
        public Plan Update(Plan updatedPlan);
        public Plan Delete(int id);
    }
}
