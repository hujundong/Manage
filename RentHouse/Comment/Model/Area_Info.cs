using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Comment.Model
{
    [Table("Area_Info")]
    public class AreaInfo
    {
        public int AreaId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ParentId { get; set; }
        public string RootId { get; set; }
        public string ParentName { get; set; }
        public string RootName { get; set; }
        public string TwoAreaName { get; set; }
        public string ThreeAreaName { get; set; }
        public string PinYin { get; set; }
        public short SortCode { get; set; }
        public bool Status { get; set; }
        public string Acronym { get; set; }
        public System.DateTime Addtime { get; set; }
        public short IsHot { get; set; }
        public Nullable<short> raise { get; set; }
        public Nullable<short> enable { get; set; }
        public string alias { get; set; }
    }
}