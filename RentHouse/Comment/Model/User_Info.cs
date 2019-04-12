using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comment.Model
{
    [Table("User_Info")]
    public class UserInfo
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Nullable<bool> Sex { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public string IDNum { get; set; }
        public bool UserStatus { get; set; }
        public string UserRole { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
    }
}