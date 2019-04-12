using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comment.Model;
using Comment.Repositiry;

namespace Comment.Service
{
    public class UserService
    {
        public UserRepositiry _userRepositiry = new UserRepositiry();

        public User_Info Login(string loginName, string password)
        {
            return _userRepositiry.Login(loginName, password);
        }

        public bool Register(string loginName, string phone, string password, out string message)
        {
            User_Info user = new User_Info
            {
                LoginName = loginName,
                Phone = phone,
                Password = password,
                UserStatus = true,
                UserRole = EnumCode.UserRole.Normal,
                CreateDate = DateTime.Now
            };
            if (_userRepositiry.VerifyLoginName(user.LoginName))
            {
                if (_userRepositiry.VerifPhone(user.Phone))
                {
                    if (_userRepositiry.Register(user))
                    {
                        message = "注册成功";
                        return true;
                    }
                    else
                    {
                        message = "注册失败，请联系管理员";
                        return false;
                    }
                }
                else
                {
                    message = "注册失败，手机号已存在";
                    return false;
                }
            }
            else
            {
                message = "注册失败，用户名已存在";
                return false;
            }
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user">用户对象</param>
        /// <param name="message">返回消息</param>
        /// <returns></returns>
        public bool AddUser(User_Info user, out string message)
        {
            user.UserStatus = true;
            user.CreateDate = DateTime.Now;
            if (_userRepositiry.VerifyLoginName(user.LoginName))
            {
                if (_userRepositiry.VerifPhone(user.Phone))
                {
                    if (_userRepositiry.Register(user))
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
                else
                {
                    message = "添加失败，手机号已存在";
                    return false;
                }
            }
            else
            {
                message = "添加失败，用户名已存在";
                return false;
            }
        }

        public List<User_Info> GetUserList(User_Info user, int page, int size, out int pages, out int count)
        {
            return _userRepositiry.GetUserList(user,page,size,out pages,out count);
        }

        public User_Info GetUserById(int userId)
        {
            return _userRepositiry.GetUserById(userId);
        }

        public bool EditUser(User_Info user, out string message)
        {
            if (_userRepositiry.GetUserById(user.UserId) != null)
            {
                if (_userRepositiry.VerifPhone(user.Phone, user.UserId))
                {
                    message = "修改成功";
                    return _userRepositiry.EditUser(user);
                }
                else
                {
                    message = "手机号已存在";
                    return false;
                }
            }
            else
            {
                message = "用户不存在";
                return false;
            }
        }

        public bool DeleteUser(int userId, out string message)
        {
            User_Info user = _userRepositiry.GetUserById(userId);
            if (user != null)
            {
                if (_userRepositiry.DeleteUser(user))
                {
                    message = "删除成功";
                    return true;
                }
            }
            message = "删除失败";
            return false;
        }

        public bool UpdateUserStatus(int userId, bool status)
        {
            return _userRepositiry.UpdateUserStatus(userId, status);
        }
    }
}