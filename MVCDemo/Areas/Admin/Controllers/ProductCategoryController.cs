using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Dao;
using MVCDemo.Areas.Admin.Models;
using MVCDemo.Common;
using MVCDemo.Models;

namespace MVCDemo.Areas.Admin.Controllers
{
    public class ProductCategoryController : BaseController
    {
        // GET: Admin/ProductCategory
        public ActionResult ListProductCategory()
        {
            var daocate = new ProductCategoryDao();
            var lst = daocate.ListProductCategory();
            return View(lst);
        }
        public ActionResult Edit(int id)
        {
            var procate = new ProductCategoryDao().ViewDetail(id);
            return View(procate);
        }
        [HttpDelete]
        public ActionResult DeleteProductCategory(int id)
        {
            var dao = new ProductDao();
            var check = dao.CountProduct(id);
            if (check == 0)
            {
                var daoo = new ProductCategoryDao();
                daoo.Delete(id);
                //return RedirectToAction("ListProduct", "Product");
                return Json(new { isok = true, message = "Your Message" });
            }
            else
            {
                //return RedirectToAction("ListProduct", "Product");
                return Json(new { isok = false, message = "Your Message" });
            }
        }
        [HttpPost]
        public ActionResult Edit(ProductCategory model, int id)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductCategoryDao();
                var result = dao.Update(model,id);
                if (result)
                {
                    return Json(new { isok = true, message = "Your Message" });
                    //return RedirectToAction("ListUser", "Admin");
                }
                else
                {
                    return Json(new { isok = false, message = "Your Message" });
                }
            }
            else 
            {
                return Json(new { isok = false, message = "Your Message" });
            }
        }
        [HttpPost]
        public ActionResult Add(ProductCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var check = new ProductDao().CheckName(model.Name);
                if (check == null)
                {
                    var dao = new ProductCategoryDao();
                    var procate = new ProductCategory();
                    procate.Name = model.Name;
                    procate.CreatedDate = DateTime.Now;
                    dao.Add(procate);
                    return Json(new { isok = true, message = "Thêm thành công !" });
                }
                else
                {
                    return Json(new { isok = false, message = "Thể loại đã tồn tại." });
                }

            }
            else
            {
                return Json(new { isok = false, message = "Your Message" });
            }
        }
    }
}