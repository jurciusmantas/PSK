using PSK.Model.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.Model.Repository
{
    public interface IIncomingEmployeeRepository
    {
        public IncomingEmployee Add(IncomingEmployee incomingEmployee);
        public IncomingEmployee GetIncomingEmployee(int id);
        public IncomingEmployee Update(IncomingEmployee updatedIncomingEmployee);
        public IncomingEmployee Delete(int id);
    }
}
