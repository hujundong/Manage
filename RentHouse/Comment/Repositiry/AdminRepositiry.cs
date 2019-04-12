using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comment.Model;

namespace Comment.Repositiry
{
    public class AdminRepositiry
    {
        public RentHouseEntities db = new RentHouseEntities();

        public User_Info Login(string loginName, string password)
        {
            return db.User_Info.FirstOrDefault(m => (m.LoginName.Equals(loginName) || m.Phone.Equals(loginName)) && m.Password.Equals(password) && m.UserStatus == true && m.UserRole.Contains("ADMIN"));
        }
    }
}