using PSK.Model.Entities;
using System.Collections.Generic;

namespace PSK.Model.Repository
{
    public interface IIncomingEmployeeRepository : IRepository<IncomingEmployee>
    {
        IncomingEmployee FindByToken(string token);
        List<IncomingEmployee> GetAllByEmail(string email);
    }
}
