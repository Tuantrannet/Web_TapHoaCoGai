using DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Data;

namespace DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;

        public IGroceryRepository Grocery { get; private set; }

        public IUserRepository User { get; private set; }

        public IRoomRepository Room { get; private set; }

        public IMessageRepository Message { get; private set; }
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Grocery = new GroceryRepository(_db);
            User = new UserRepository(_db);
            Message = new MessageRepository(_db);
            Room = new RoomRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
