using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;
using Common;

namespace Model.Dao
{
    public class UserDao
    {
        OnlineShopDbContext db = null;
        public UserDao()
        {
            db = new OnlineShopDbContext();
        }
        public void Add(User entity)
        {
            db.User.Add(entity);
            db.SaveChanges();       
        }
        public List<User> ListUser()
        {
            return db.User.ToList();
        }
        public User ViewDetail(int id)
        {
            return db.User.Find(id);
        }
        public int Delete(int id)
        {
            User pro = db.User.Find(id);
            if (pro != null)
            {
                db.User.Remove(pro);
                return db.SaveChanges();
            }
            else
                return -1;
        }
        public bool Update(User entity, int? id)
        {
            try
            {
                var user = db.User.Find(id);
                user.SDT = entity.SDT;
                user.Address = entity.Address;
                user.Email = entity.Email;
                user.Fullname = entity.Fullname;
                user.Status = true;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
           
        }
        public bool UpdateMoney(decimal? money, int? id)
        {
            try
            {
                var user = db.User.Find(id);
                user.Money = money;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
        public User GetByUserName(string userName)
        {
            return db.User.SingleOrDefault(x => x.Username == userName);
        }public User GetByEmail(string Email)
        {
            return db.User.SingleOrDefault(x => x.Email == Email);
        }
        public int Login(string userName, string passWord, bool isLoginAdmin = false)
        {
            var result = db.User.SingleOrDefault(x => x.Username == userName);
            if (result ==null)
            {
                return 0;
            }
            else
            {
                if (isLoginAdmin == true)
                {
                    if (result.GroupID == RoleConstants.ADMIN_GROUP)
                    {
                        if (result.Status == false)
                        {
                            return -1;
                        }
                        else
                        {
                            if (result.Password == passWord)
                                return 1;
                            else
                                return -2;
                        }
                    }
                    else
                    {
                        return -3;
                    }
                }
                else 
                {
                    if (result.Status == false)
                    {
                        return -1;
                    }
                    else
                    {
                        if (result.Password == passWord)
                            return 1;
                        else
                            return -2;
                    }
                }
            }
        }
    }
}
