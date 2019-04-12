using System.Collections.Generic;

namespace Comment.Model.Query
{
    public class QueryModel
    {
        public QueryModel()
        {
            Items = new List<ConditionItem>();
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        public List<ConditionItem> Items { get; set; }
    }
}