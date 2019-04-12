using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comment.Model;

namespace Comment.Repositiry
{
    public class HouseRepositiry
    {
        public RentHouseEntities db = new RentHouseEntities();

       

        public bool AddHouse(House_Info house)
        {
            try
            {
                db.House_Info.Add(house);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 房屋管理分页
        /// </summary>
        /// <param name="user">搜索参数</param>
        /// <param name="page">当前页</param>
        /// <param name="size">每页数量</param>
        /// <param name="pages">总页数</param>
        /// <param name="count">总数量</param>
        /// <returns></returns>
        public List<House_Info> GetHouseList(House_Info house,bool delete,string username, int page, int size, out int pages, out int count)
        {
            count = db.House_Info.Where(m => (string.IsNullOrEmpty(house.Community) || m.Community.Contains(house.Community))&&(string.IsNullOrEmpty(username) || m.LandlordName==username)&&m.DeleteStatus==delete).Count();
            pages = count % size == 0 ? count / size : count / size + 1;
            page = page <= 1 ? 1 : page;
            page = page >= pages && pages > 0 ? pages : page;
            return db.House_Info.Where(m => (string.IsNullOrEmpty(house.Community) || m.Community.Contains(house.Community)) && (string.IsNullOrEmpty(username) || m.LandlordName == username)&&m.DeleteStatus==delete).OrderBy(m => m.HouseId).Skip(size * (page - 1)).Take(size).ToList();
        }

        public bool EditHouse(House_Info house)
        {
            try
            {
                House_Info Info = GetHouseById(house.HouseId);

                if (Info != null)
                {
                    Info.HouseName = house.HouseName;
                    Info.HouseStatus = house.HouseStatus;
                    Info.AreaCode = house.AreaCode;
                    Info.Community = house.Community;
                    Info.Building = house.Building;
                    Info.Unit = house.Unit;
                    Info.RoomNumber = house.RoomNumber;
                    Info.Toward = house.Toward;
                    Info.HouseType = house.HouseType;
                    Info.AreaSize = house.AreaSize;
                    Info.DegreeOfDecoration = house.DegreeOfDecoration;
                    Info.MonthlyRcnt = house.MonthlyRcnt;
                    Info.UpdateDate = DateTime.Now;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public House_Info GetHouseById(int houseId)
        {
            return db.House_Info.FirstOrDefault(m => m.HouseId == houseId);
        }

     

        public bool DeleteHouse(House_Info house)
        {
            try
            {
                house.DeleteStatus = true;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}