using Model.Dao;
using Model.EF;
using MVCDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCDemo.Areas.Admin.Controllers
{
    public class ReportController : BaseController
    {
        // GET: Admin/Report
        public ActionResult Index()
        {
            var lst = new OrderDao().ListAll();
            return View(lst);
        }
        public ActionResult DetailReport(int id)
        {
            var lst = new List<CartModel>();
            var dao = new OrderDetailDao().ViewDetail(id);
            foreach (var item in dao)
            {
                var cart = new CartModel();
                var cartInOrder = new ProductDao().ViewDetail(item.ProductID);
                var quantity = item.Quantity;
                cart.Product = cartInOrder;
                cart.Quantity = quantity;
                lst.Add(cart);
            }

            return View(lst);
        }
        [HttpDelete]
        public ActionResult DeleteReportAdmin(int id)
        {
            var orderDao = new OrderDao();
            var orderDetailDao = new OrderDetailDao();
            var userID = (UserLogin)Session[Common.CommonConstants.USER_SESSION];
            var userM = new UserDao();
            // 0: Dang xu ly
            // 1: Dang van chuyen
            // 2: Da hoan thanh
            // 3: Da hoan tra
            if (orderDao.ViewDetail(id).Status == 2)
            {
                orderDetailDao.Delete(id);
                orderDao.Delete(id);
                return Json(new { isok = true, message = "Xóa thành công." });
            }
            else if (orderDao.ViewDetail(id).Status == 0)
            {
                return Json(new { isok = false, message = "Không thể xóa đơn hàng đang xử lý." });
            }
            return Json(new { isok = false, message = "Xóa thất bại." });
        }
    }
}