using KarateClub.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarateClub.Data.Repositories
{
    public class PaymentRepository : IRepository<Payment>
    {
        private ApplicationDbContext2 db;
        private Payment _table;
        public PaymentRepository()
        {
            db = new ApplicationDbContext2();

        }


        public int Add(Payment table)
        {
            if (db.Database.CanConnect())
            {
                db.Payments.Add(table);
                db.SaveChanges();
                return table.PaymentId;
            }
            else
                return -1;
        }

        public int Delete(int Id)
        {
            if (db.Database.CanConnect())
            {
                _table = Find(Id);
                if (_table != null)
                {
                    db.Payments.Remove(_table);
                    db.SaveChanges();
                }
                return _table.PaymentId;

            }

            return -1;
        }

        public int Edit(int Id, Payment table)
        {
            if (db.Database.CanConnect())
            {
                db.Payments.Update(table);
                db.SaveChanges();
                return table.PaymentId;
            }
            else
                return -1;
        }

        public Payment Find(int Id)
        {
            if (db.Database.CanConnect())
                return db.Payments.Where(x => x.PaymentId == Id).First();
            else
                return null;
        }

        public List<Payment> GetAllData()
        {
            if (db.Database.CanConnect())
            {
                // return list;
                return db.Payments.Include(p => p.Member).ToList();
            }
            return null;
        }

        public List<Payment> GetDataById(string Id)
        {
            throw new NotImplementedException();
        }

        public List<Payment> Search(string SerachItem)
        {
            throw new NotImplementedException();
        }


    
    }
}
