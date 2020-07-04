using Model.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCDemo.Common;
using MVCDemo.Models;
using System.Web.Script.Serialization;
using Model.EF;

namespace MVCDemo.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index(int id)
        {
            var product = new ProductDao().ViewDetail(id);
            var lst = new ProductDao().ListProduct();
            ViewBag.data = lst;
            return View(product);
        }
        public ActionResult Payment()
        {
            var cart = Session[Common.CommonConstants.CART_SESSION];
            var lst = new List<CartModel>();
            if (cart != null)
            {
                lst = (List<CartModel>)cart;
            }
            return View(lst);
        }
        [HttpPost]
        public ActionResult Payment(string amountPrice, string userName, string phoneNum, string adrress)
        {
            if (userName == "" || phoneNum == "" || adrress == "")
            {
                return Json(new { isok = false, message = "Thiếu thông tin." });
            }
            var userID = (UserLogin)Session[Common.CommonConstants.USER_SESSION];
            var checkuser = new UserDao().ViewDetail(userID.UserID);
            if (checkuser.Money > Convert.ToDecimal(amountPrice))
            {
                var user = new User();
                user.Money = checkuser.Money - Convert.ToDecimal(amountPrice);
                var dao = new UserDao().UpdateMoney(user.Money, userID.UserID);
                var cart = (List<CartModel>)Session[Common.CommonConstants.CART_SESSION];
                var order = new Order();
                order.CreatedDate = DateTime.Now;
                order.CustomerName = userName;
                order.Phone = phoneNum;
                order.Address = adrress;
                order.UserID = userID.UserID;
                order.Price = Convert.ToDecimal(amountPrice);
                var id = new OrderDao().Add(order);
                var detailDao = new OrderDetailDao();
                foreach (var item in cart)
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.OrderID = id;
                    orderDetail.ProductID = item.Product.ID;
                    orderDetail.Price = item.Product.PromotionPice;
                    orderDetail.Quantity = item.Quantity;
                    detailDao.Add(orderDetail);
                    var productViewCount = new ProductDao().UpdateViewCount(item.Product.ID,1);


                }
                Session[Common.CommonConstants.CART_SESSION] = null;
                return Json(new { isok = true, message = "Thanh toán thành công." });
            }
            else
            {
                return Json(new { isok = false, message = "Số dư trong tài khoản không đủ." });
            }

        }
        public ActionResult Cart()
        {
            var cart = Session[Common.CommonConstants.CART_SESSION];
            var lst = new List<CartModel>();
            if (cart != null)
            {
                lst = (List<CartModel>)cart;
            }
            return View(lst);
        }
        [HttpPost]
        public JsonResult DeleteAll()
        {
            Session[Common.CommonConstants.CART_SESSION] = null;
            return Json(new { status = true });
        }
        [HttpPost]
        public JsonResult AddToCart(long productId, int quantity)
        {
            var product = new ProductDao().ViewDetail(productId);
            var cart = Session[Common.CommonConstants.CART_SESSION];
            if (cart != null)
            {
                var list = (List<CartModel>)cart;
                if (list.Exists(x => x.Product.ID == productId))
                {
                    foreach (var item in list)
                    {
                        if (item.Product.ID == productId)
                        {
                            item.Quantity += quantity;
                        }
                    }

                }
                else
                {
                    var item = new CartModel();
                    item.Product = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
                Session[Common.CommonConstants.CART_SESSION] = list;
            }
            else
            {
                var item = new CartModel();
                item.Product = product;
                item.Quantity = quantity;
                var list = new List<CartModel>();
                list.Add(item);
                Session[Common.CommonConstants.CART_SESSION] = list;
            }
            return Json(new { status = true });
            //return View("Cart");
        }

        [HttpPost]
        public JsonResult UpdateCart(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartModel>>(cartModel);
            var sessionCart = (List<CartModel>)Session[Common.CommonConstants.CART_SESSION];
            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            return Json(new { status = true });
        }
        [HttpPost]
        public JsonResult Delete(long id)
        {
            var sessionCart = (List<CartModel>)Session[Common.CommonConstants.CART_SESSION];
            sessionCart.RemoveAll(x => x.Product.ID == id);
            Session[Common.CommonConstants.CART_SESSION] = sessionCart;
            return Json(new { status = true });
        }
        public ActionResult Report()
        {
            var userID = (UserLogin)Session[Common.CommonConstants.USER_SESSION];
            var lst = new OrderDao().ListOrder(userID.UserID);
            return View(lst);
        }
        [HttpDelete]
        public ActionResult DeleteReport(int id)
        {
            var orderDao = new OrderDao();
            var orderDetailDao = new OrderDetailDao();
            var userID = (UserLogin)Session[Common.CommonConstants.USER_SESSION];
            var userM = new UserDao();
            var moneyCur = userM.ViewDetail(userID.UserID);
            var money = orderDao.ViewDetail(id);
            var a = (moneyCur.Money + money.Price);
            if (orderDao.ViewDetail(id).Status == 0)
            {
                var checkView = new ProductDao().DownViewCount(id);
                orderDetailDao.Delete(id);
                orderDao.Delete(id);
                var userMon = new User();
                userMon.Money = a;
                userM.UpdateMoney(userMon.Money, userID.UserID);
               
                return Json(new { isok = true, message = "Hoàn trả thành công." });
            }
            return Json(new { isok = false, message = "Hoàn trả thất bại." });
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
    }
}