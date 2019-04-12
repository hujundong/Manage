using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comment.Model;
using Comment.Repositiry;

namespace Comment.Service
{
    public class HouseService
    {
        public HouseRepositiry _houseRepositiry = new HouseRepositiry();

        public bool AddHouse(House_Info house,out string message)
        {
            house.DeleteStatus = false;
            house.CreateDate = DateTime.Now;
            house.HouseStatus = false;
            if (_houseRepositiry.AddHouse(house))
            {
                message = "添加成功";
                return true;
            }
            else
            {
                message = "添加失败";
                return false;
            }
        }

        public List<House_Info> GetHouseList(House_Info house,bool delete,string username, int page, int size, out int pages, out int count)
        {
            return _houseRepositiry.GetHouseList(house,delete,username, page, size, out pages, out count);
        }

        public House_Info GetHouseById(int houseId)
        {
            return _houseRepositiry.GetHouseById(houseId);
        }

        public bool EditHouse(House_Info house, out string message)
        {
            if (_houseRepositiry.GetHouseById(house.HouseId) != null)
            {
                if (_houseRepositiry.EditHouse(house))
                {
                    message = "修改成功";
                    return true;
                }
                else
                {
                    message = "修改失败";
                    return false;
                }
            }
            else
            {
                message = "房屋不存在";
                return false;
            }
        }

        public bool DeleteHouse(int houseId, out string message)
        {
            House_Info house = _houseRepositiry.GetHouseById(houseId);
            if (house != null)
            {
                if (_houseRepositiry.DeleteHouse(house))
                {
                    message = "删除成功";
                    return true;
                }
            }
            message = "删除失败";
            return false;
        }
    }
}