using DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Data;
using Models;

namespace DataAccess.Repository
{
    public class GroceryRepository : Repository<Grocery>, IGroceryRepository
    {
        private  ApplicationDbContext _db;
        public GroceryRepository(ApplicationDbContext db) : base(db) 
        {
            _db = db;
        }
        public void Update(Grocery obj)
        {
            dbSet.Update(obj);
        }
    }
}
