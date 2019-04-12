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
    public class HomeController : BaseController
    {
        public HouseService _houseService = new HouseService();
        public ActionResult Index()
        {
            if (HasHouseUser())
            {
                User_Info user = GetUser();
                return View(user);
            }
            return RedirectToAction("Login", "User");
        }

        public ActionResult Welcome()
        {
            if (HasHouseUser())
            {
                User_Info user = GetUser();
                return View(user);
            }
            return RedirectToAction("Login", "User");
        }

        /// <summary>
        /// 房屋列表页面
        /// </summary>
        /// <param name="house"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public ActionResult HouseList(House_Info house, int delete=0,int page = 1, int size = 8)
        {
            if (HasHouseUser())
            {
                string username = "";
                int pages = 0, count = 0;
                bool isdelete = false;
                if (delete == 1)
                {
                    isdelete = true;
                }
                User_Info user = GetUser();
                if (user.UserRole == EnumCode.UserRole.Normal)
                {
                    username = user.LoginName;
                }
                List<House_Info> list = _houseService.GetHouseList(house,isdelete, username, page, size, out pages, out count);
                ViewBag.house = house;
                ViewBag.count = count;
                ViewBag.pages = pages;
                ViewBag.page = page;
                ViewBag.delete = delete;
                return View(list);
            }
            return RedirectToAction("Login", "User");
        }

        //添加房屋页面
        public ActionResult AddHouse()
        {
            return View();
        }

        //添加房屋方法
        [HttpPost]
        public JsonResult AddHouseSubmit(House_Info house)
        {
            User_Info user = GetUser();
            if (user.UserRole == EnumCode.UserRole.Normal)
            {
                house.LandlordName = user.LoginName;
            }
            string message = "";
            bool flag = _houseService.AddHouse(house, out message);
            return Json(new { Flag = flag, Message = message });
        }

        //编辑房屋页面
        public ActionResult EditHouse(int houseId)
        {
            House_Info house = _houseService.GetHouseById(houseId);
            return View(house);
        }

        //编辑房屋方法
        [HttpPost]
        public JsonResult EditHouseSubmit(House_Info house)
        {
            string message = "";
            bool flag = _houseService.EditHouse(house, out message);
            return Json(new { Flag = flag, Message = message });
        }

        //删除房屋方法
        [HttpPost]
        public JsonResult DeleteHouse(int houseId)
        {
            string message = "";
            bool flag = _houseService.DeleteHouse(houseId, out message);
            return Json(new { Flag = flag, Message = message });
        }
    }
}