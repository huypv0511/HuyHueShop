using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.Dao
{
    public class ProductCategoryDao
    {
        OnlineShopDbContext db = null;
        public ProductCategoryDao()
        {
            db = new OnlineShopDbContext();
        }
        public void Add(ProductCategory entity)
        {
            db.ProductCategory.Add(entity);
            db.SaveChanges();
        }
        public ProductCategory CheckName(string name)
        {
            return db.ProductCategory.SingleOrDefault(x => x.Name == name);
        }
        public List<ProductCategory> ListProductCategory()
        {
            return db.ProductCategory.ToList();
        }
        public ProductCategory ViewDetail(long? id)
        {
            return db.ProductCategory.Find(id);
        }
        public int Delete(int id)
        {
            ProductCategory procate = db.ProductCategory.Find(id);
            if (procate != null)
            {
                db.ProductCategory.Remove(procate);
                return db.SaveChanges();
            }
            else
                return -1;
        }
        public bool Update(ProductCategory entity,long? id)
        {
            try
            {
                var procate = db.ProductCategory.Find(id);
                procate.Name = entity.Name;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
