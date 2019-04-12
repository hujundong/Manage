using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RentHouse.Models;
using Comment.Model;

namespace RentHouse.Controllers
{
    public class BaseController : Controller
    {
        public bool HasHouseUser()
        {
            User_Info user = (User_Info)Session[SessionCode.HouseUser];
            if (user != null)
            {
                return true;
            }
            return false;
        }

        public User_Info GetUser()
        {
            User_Info user = (User_Info)Session[SessionCode.HouseUser];
            if (user != null)
            {
                return user;
            }
            return null;
        }
    }
}