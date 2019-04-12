using System.Collections.Generic;

namespace Comment.Model
{
    public class PageData<T>
    {
        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 当前页数据集合
        /// </summary>
        public List<T> DataList { get; set; }
    }
}