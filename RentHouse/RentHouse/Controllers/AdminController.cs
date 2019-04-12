using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Comment.Model;
using Comment.Service;
using RentHouse.Models;

namespace RentHouse.Controllers
{
    public class AdminController : BaseController
    {
        public UserService _userService = new UserService();
        public AdminService _adminService = new AdminService();

        // GET: 登录页面
        public ActionResult Login()
        {
            return View();
        }

        //登录方法
        [HttpPost]
        public JsonResult LoginSubmit(string loginname, string password)
        {
            User_Info user = _adminService.Login(loginname, password);
            if (user != null)
            {
                Session[SessionCode.HouseUser] = user;
                return Json(new { Flag = true, Message = "登录成功" });
            }
            return Json(new { Flag = false, Message = "登录失败" });
        }
    }
}