using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comment.Model
{
    public class EnumCode
    {
        public class UserRole
        {
            /// <summary>
            /// 超级管理员
            /// </summary>
            public const string SuperAdmin = "SUPERADMIN";

            /// <summary>
            /// 普通管理员
            /// </summary>
            public const string Admin = "ADMIN";

            /// <summary>
            /// 普通用户
            /// </summary>
            public const string Normal = "NORMAL";
        }
    }
}