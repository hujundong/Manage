//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Comment.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Area_Info
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