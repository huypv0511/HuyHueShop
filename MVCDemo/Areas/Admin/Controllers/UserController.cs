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
    public class UserController : BaseController
    {
        // GET: Admin/Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListUser()
        {
            var dao = new UserDao();
            var lst = dao.ListUser();
            return View(lst);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var dao = new UserDao();
            dao.Delete(id);
            return RedirectToAction("ListUser", "User");
        }
        public ActionResult Edit(int id)
        {
            var user = new UserDao().ViewDetail(id);
            return View(user);
        }
        [HttpPost]
        public ActionResult Add(Register model)
        {
            if (ModelState.IsValid)
            {

                var dao = new UserDao();
                var UserName = dao.GetByUserName(model.UserName);
                var Email = dao.GetByEmail(model.Email);

                if (UserName != null)
                {
                    return Json(new { isok = false, message = "Tài khoản đã được sử dụng" });
                }
                else if (Email != null)
                {
                    return Json(new { isok = false, message = "Email đã được sử dụng" });
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

                    return Json(new { isok = true, message = "Thêm thành công!" });
                }
            }
            return Json(new { isok = false, message = "Thêm thất bại !" }); ;
        }
        [HttpPost]
        public ActionResult Edit(User user, int? id)
        {
            var dao = new UserDao();
            if (!string.IsNullOrEmpty(user.Password))
            {
                var encryptedMd5Pas = Encryption.MD5Hash(user.Password);
                user.Password = encryptedMd5Pas;
            }
            var result = dao.Update(user, id);
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
    }
}