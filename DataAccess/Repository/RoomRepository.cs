using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        private ApplicationDbContext _db;

        public RoomRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }

        public void Update(Room obj)
        {
            dbSet.Update(obj);
        }

    }
}
