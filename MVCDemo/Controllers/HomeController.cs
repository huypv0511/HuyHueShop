using Model.Dao;
using MVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using MVCDemo.Common;

namespace MVCDemo.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [ChildActionOnly]
        public ActionResult TopMenu()
        {
            var cart = Session[Common.CommonConstants.CART_SESSION];
            var lst = new List<CartModel>();
            if (cart != null)
            {
                lst = (List<CartModel>)cart;
            }
            return PartialView(lst);
        }
        public ActionResult Guess(string search, int? i)
        {
            var lst = new ProductDao().ListProduct();
            var lstcate = new ProductCategoryDao().ListProductCategory();
            var mixmodel = new ModelMix {ProductObj = lst.ToPagedList(i ?? 1, 12), ProductCateObj = lstcate};
            
            return View(mixmodel);
        }

    }
}