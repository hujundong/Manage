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
    public class UserController : BaseController
    {
        public UserService _userService = new UserService();

        // GET: 登录页面
        public ActionResult Login()
        {
            return View();
        }

        //登录方法
        [HttpPost]
        public JsonResult LoginSubmit(string loginname, string password)
        {
            User_Info user = _userService.Login(loginname, password);
            if (user != null)
            {
                if (user.UserStatus == false)
                {
                    return Json(new { Flag = false, Message = "用户已禁用" });
                }
                Session[SessionCode.HouseUser] = user;
                return Json(new { Flag = true, Message = "登录成功" });
            }
            return Json(new { Flag = false, Message = "登录失败" });
        }

        //注册方法
        [HttpPost]
        public JsonResult RegisterSubmit(string username, string phone, string password)
        {
            string message = "";
            bool flag = _userService.Register(username, phone, password, out message);
            return Json(new { Flag = flag, Message = "注册失败" });
        }

        //用户列表页面
        public ActionResult UserList(User_Info user,int page=1,int size=8)
        {
            if (HasHouseUser())
            {
                int pages = 0, count = 0;
                List<User_Info> userList = _userService.GetUserList(user,page,size,out pages,out count);
                ViewBag.user = user;
                ViewBag.count = count;
                ViewBag.pages = pages;
                ViewBag.page = page;
                return View(userList);
            }
            return RedirectToAction("Login");
        }

        //添加用户页面
        public ActionResult AddUser()
        {
            return View();
        }

        //添加用户方法
        [HttpPost]
        public JsonResult AddUserSubmit(User_Info user)
        {
            string message = "";
            bool flag = _userService.AddUser(user, out message);
            return Json(new { Flag = flag, Message = message });
        }

        //编辑用户页面
        public ActionResult EditUser(int userId)
        {
            User_Info user = _userService.GetUserById(userId);
            return View(user);
        }

        //编辑用户方法
        [HttpPost]
        public JsonResult EditUserSubmit(User_Info user)
        {
            string message = "";
            bool flag = _userService.EditUser(user, out message);
            return Json(new { Flag = flag, Message = message });
        }

        //删除用户方法
        [HttpPost]
        public JsonResult DeleteUser(int userId)
        {
            string message = "";
            bool flag = _userService.DeleteUser(userId, out message);
            return Json(new { Flag = flag, Message = message });
        }

        //修改用户状态
        [HttpPost]
        public JsonResult UpdateUserStatus(int userId, bool status)
        {
            bool flag = _userService.UpdateUserStatus(userId, status);
            return Json(new { Flag = flag });
        }
    }
}