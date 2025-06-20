using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IGroceryRepository Grocery { get; }

        IUserRepository User { get; }
        IMessageRepository Message { get; }
        IRoomRepository Room { get; }
        void Save();
    }
}
