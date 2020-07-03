using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using Model.Dao;
using Model.EF;
using MVCDemo.Common;
using MVCDemo.Models;

namespace MVCDemo.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dao = new UserDao();
                    var result = dao.Login(model.UserName, Encryption.MD5Hash(model.PassWord));
                    if (result == 1)
                    {
                        var user = dao.GetByUserName(model.UserName);
                        var userSession = new UserLogin();
                        userSession.UserName = user.Username;
                        userSession.UserID = user.ID;
                        Session.Add(CommonConstants.USER_SESSION, userSession);
                        return RedirectToAction("Guess", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không chính xác");
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                
            }
            return View("Index");
        }
        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            Session[CommonConstants.CART_SESSION] = null;
            return Redirect("/");
        }
        [HttpPost]
        public ActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDao();
                var UserName = dao.GetByUserName(model.UserName);
                var Email = dao.GetByEmail(model.Email);
                
                if (UserName != null)
                {
                    ModelState.AddModelError("", "Tài khoản đã được sử dụng");
                }
                else if (Email != null)
                {
                    ModelState.AddModelError("", "Email đã được sử dụng");
                }
                else
                {
                    var user = new User();
                    user.Username = model.UserName;
                    user.Password = Encryption.MD5Hash(model.PassWord);
                    user.SDT = model.Phone;
                    user.Email = model.Email;
                    user.Fullname = model.Name;
                    user.Datetime = DateTime.Now;
                    user.Permission = false;
                    user.Status = true;
                    dao.Add(user);

                    return RedirectToAction("Index", "Login");
                }
            }
            return View("Index");

        }
    }
}