using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.Dao
{
    public class OrderDao
    {
        OnlineShopDbContext db = null;
        public OrderDao()
        {
            db = new OnlineShopDbContext();
        }
        public int Add(Order entity)
        {
            db.Orders.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public List<Order> ListOrder(int id)
        {
            return db.Orders.Where(x=>x.UserID == id).ToList();
        }
        public List<Order> ListAll()
        {
            return db.Orders.ToList();
        }
        public Order ViewDetail(long? id)
        {
            return db.Orders.Find(id);
        }
        public bool Update(int id, int Status)
        {
            try
            {
                var order = db.Orders.Find(id);
                order.Status = Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return true;
            }
        }
        public int Delete(int id)
        {
            Order order = db.Orders.Find(id);
            if (order != null)
            {
                db.Orders.Remove(order);
                return db.SaveChanges();
            }
            else
                return -1;
        }


    }
}
