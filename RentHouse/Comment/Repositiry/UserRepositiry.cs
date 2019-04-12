using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comment.Model;

namespace Comment.Repositiry
{
    public class UserRepositiry
    {
        public RentHouseEntities db = new RentHouseEntities();

        public User_Info Login(string loginName, string password)
        {
            return db.User_Info.FirstOrDefault(m => (m.LoginName.Equals(loginName) || m.Phone.Equals(loginName)) && m.Password.Equals(password) && m.UserStatus == true && m.UserRole.Equals("NORMAL"));
        }

        public bool Register(User_Info user)
        {
            try
            {
                db.User_Info.Add(user);
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
        /// 用户管理分页
        /// </summary>
        /// <param name="user">搜索参数</param>
        /// <param name="page">当前页</param>
        /// <param name="size">每页数量</param>
        /// <param name="pages">总页数</param>
        /// <param name="count">总数量</param>
        /// <returns></returns>
        public List<User_Info> GetUserList(User_Info user,int page,int size, out int pages, out int count)
        {
            count = db.User_Info.Where(m => (string.IsNullOrEmpty(user.UserName) || m.UserName.Contains(user.LoginName))).Count();
            pages = count % size == 0 ? count / size : count / size + 1;
            page = page <= 1 ? 1 : page;
            page = page >= pages && pages > 0 ? pages : page;
            return db.User_Info.Where(m => (string.IsNullOrEmpty(user.UserName) || m.UserName.Contains(user.LoginName))).OrderBy(m => m.UserId).Skip(size * (page - 1)).Take(size).ToList();
        }

        public bool EditUser(User_Info user)
        {
            try
            {
                User_Info user_Info = GetUserById(user.UserId);

                if (user_Info != null)
                {
                    user_Info.Phone = user.Phone;
                    user_Info.UserName = user.UserName;
                    user_Info.Email = user.Email;
                    user_Info.Sex = user.Sex;
                    user_Info.Birthday = user.Birthday;
                    user_Info.UpdateDate = DateTime.Now;
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

        public User_Info GetUserById(int userId)
        {
            return db.User_Info.FirstOrDefault(m => m.UserId == userId);
        }

        public bool VerifyLoginName(string loginName)
        {
            return db.User_Info.FirstOrDefault(m => m.LoginName.Equals(loginName)) == null ? true : false;
        }

        public bool VerifPhone(string phone)
        {
            return db.User_Info.FirstOrDefault(m => m.Phone.Equals(phone)) == null ? true : false;
        }

        public bool VerifPhone(string phone, int userId)
        {
            return db.User_Info.FirstOrDefault(m => m.Phone.Equals(phone) && m.UserId != userId) == null ? true : false;
        }

        public bool DeleteUser(User_Info user)
        {
            try
            {
                db.User_Info.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateUserStatus(int userId, bool status)
        {
            User_Info user_Info = GetUserById(userId);
            try
            {
                if (user_Info != null)
                {
                    user_Info.UserStatus = status;
                    user_Info.UpdateDate = DateTime.Now;
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
    }
}