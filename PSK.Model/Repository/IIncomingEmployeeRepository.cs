using PSK.Model.BusinessEntities;

namespace PSK.Model.Repository
{
    public interface IIncomingEmployeeRepository : IRepository<IncomingEmployee>
    {
        public IncomingEmployee FindByToken(string token);
    }
}
