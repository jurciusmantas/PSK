﻿using PSK.Model.BusinessEntities;
using PSK.Model.Repository;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}