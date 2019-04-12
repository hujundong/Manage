using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comment.Model
{
    [Table("Tenants_Info")]
    public class TenantsInfo
    {
        public int TenantsId { get; set; }
        public string Name { get; set; }
        public Nullable<bool> Sex { get; set; }
        public string Phone { get; set; }
        public Nullable<System.DateTime> Birthday { get; set; }
        public string IDNum { get; set; }
    }
}