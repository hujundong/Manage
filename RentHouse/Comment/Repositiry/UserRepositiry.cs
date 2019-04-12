using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Comment.Data.Infrastructure;
using Comment.Model;
using Comment.Model.Query;

namespace Comment.Repositiry
{
    public class UserRepositiry : RepositoryBase<UserInfo>, IUserRepositiry
    {
        public UserRepositiry(IDatabaseFactory databaseFactory)
           : base(databaseFactory)
        {
        }

        public UserInfo Login(string loginName, string password)
        {
            return Get(m => (m.LoginName.Equals(loginName) || m.Phone.Equals(loginName)) && m.Password.Equals(password) && m.UserStatus == true && m.UserRole.Equals("NORMAL"));
        }

        public bool Register(UserInfo user)
        {
            try
            {
                Add(user);
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
        public PageData<UserInfo> GetUserList(QueryModel model, string orderby, UserInfo user, int page, int size, bool isdesc = true)
        {
            return Search(model, orderby, page, size, isdesc);
        }

        public bool EditUser(UserInfo user)
        {
            try
            {
                Update(user);
                UserInfo UserInfo = GetUserById(user.UserId);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public UserInfo GetUserById(int userId)
        {
            return db.UserInfo.FirstOrDefault(m => m.UserId == userId);
        }

        public bool VerifyLoginName(string loginName)
        {
            return db.UserInfo.FirstOrDefault(m => m.LoginName.Equals(loginName)) == null ? true : false;
        }

        public bool VerifPhone(string phone)
        {
            return db.UserInfo.FirstOrDefault(m => m.Phone.Equals(phone)) == null ? true : false;
        }

        public bool VerifPhone(string phone, int userId)
        {
            return db.UserInfo.FirstOrDefault(m => m.Phone.Equals(phone) && m.UserId != userId) == null ? true : false;
        }

        public bool DeleteUser(UserInfo user)
        {
            try
            {
                db.UserInfo.Remove(user);
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
            UserInfo UserInfo = GetUserById(userId);
            try
            {
                if (UserInfo != null)
                {
                    UserInfo.UserStatus = status;
                    UserInfo.UpdateDate = DateTime.Now;
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

    public interface IUserRepositiry : IRepository<UserInfo>
    {
        UserInfo Login(string loginName, string password);

        bool Register(UserInfo user);

        PageData<UserInfo> GetUserList(QueryModel model, string orderby, UserInfo user, int page, int size, bool isdesc = true);

        bool EditUser(UserInfo user);
    }
}