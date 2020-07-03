using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.Dao
{
    public class OrderDetailDao
    {
        OnlineShopDbContext db = null;
        public OrderDetailDao()
        {
            db = new OnlineShopDbContext();
        }
        public bool Add(OrderDetail entity)
        {
            try
            {
                db.OrderDetails.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
          
        }

        public int Delete(int id)
        {
            List<OrderDetail> pro = db.OrderDetails.Where(x=>x.OrderID == id).ToList();
            if (pro != null)
            {
                foreach (var item in pro)
                {
                    db.OrderDetails.Remove(item);
                }
                return db.SaveChanges();
            }
            else
                return -1;
        }
        public List<OrderDetail> ListCategory()
        {
            return db.OrderDetails.ToList();
        }
        public List<OrderDetail> ViewDetail(int id)
        {
            return db.OrderDetails.Where(x=>x.OrderID == id).ToList();
        }

    }
}
