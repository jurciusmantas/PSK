using PSK.Model.Entities;
using PSK.Model.Repository;
using System;
using System.Collections.Generic;

namespace PSK.DB.MockRepository
{
    public class IncomingEmployeeMockRepository : IIncomingEmployeeRepository
    {
        public IncomingEmployee Add(IncomingEmployee incomingEmployee)
        {
            throw new NotImplementedException();
        }

        public IncomingEmployee Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IncomingEmployee Get(int id)
        {
            throw new NotImplementedException();
        }

        public IncomingEmployee Update(IncomingEmployee updatedIncomingEmployee)
        {
            throw new NotImplementedException();
        }

        public IncomingEmployee FindByToken(string token)
        {
            throw new NotImplementedException();
        }

        public List<IncomingEmployee> Get()
        {
            throw new NotImplementedException();
        }
    }
}
