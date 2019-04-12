using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comment.Model;
using Comment.Repositiry;

namespace Comment.Service
{
    public class AdminService
    {
        public AdminRepositiry _adminRepositiry = new AdminRepositiry();

        public User_Info Login(string loginName, string password)
        {
            return _adminRepositiry.Login(loginName, password);
        }
    }
}