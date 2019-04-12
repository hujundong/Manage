using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comment.Model
{
    [Table("House_Info")]
    public class HouseInfo
    {
        public int HouseId { get; set; }
        public string LandlordName { get; set; }
        public string HouseName { get; set; }
        public bool HouseStatus { get; set; }
        public int AreaCode { get; set; }
        public string Community { get; set; }
        public string Building { get; set; }
        public Nullable<int> Unit { get; set; }
        public int RoomNumber { get; set; }
        public string Toward { get; set; }
        public string HouseType { get; set; }
        public int AreaSize { get; set; }
        public string DegreeOfDecoration { get; set; }
        public double MonthlyRcnt { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public bool DeleteStatus { get; set; }
    }
}