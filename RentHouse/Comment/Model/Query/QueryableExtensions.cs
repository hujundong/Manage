using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Data;
using System;
using System.Linq.Expressions;
using System.Reflection;
using Comment.Model.Query;

namespace Comment.Model.SearchModel
{
    public static class QueryableExtensions
    {
        /// <summary>
        /// zoujian add , 使IQueryable支持QueryModel
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="table">IQueryable的查询对象</param>
        /// <param name="model">QueryModel对象</param>
        /// <param name="prefix">使用前缀区分查询条件</param>
        /// <returns></returns>
        public static IQueryable<TEntity> Where<TEntity>(this IQueryable<TEntity> table, QueryModel model, string prefix = "") where TEntity : class
        {
            Contract.Requires(table != null);
            return Where<TEntity>(table, model.Items, prefix);
        }

        private static IQueryable<TEntity> Where<TEntity>(IQueryable<TEntity> table, IEnumerable<ConditionItem> items, string prefix = "")
        {
            Contract.Requires(table != null);
            IEnumerable<ConditionItem> filterItems =
                string.IsNullOrWhiteSpace(prefix)
                    ? items.Where(c => string.IsNullOrEmpty(c.Prefix))
                    : items.Where(c => c.Prefix == prefix);
            if (filterItems.Count() == 0) return table;
            return new QueryableSearcher<TEntity>(table, filterItems).Search();
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName, bool ascending) where T : class
        {
            Type type = typeof(T);
            PropertyInfo property = type.GetProperty(propertyName);
            if (property == null)
                throw new ArgumentException("propertyName", "Not Exist");
            ParameterExpression param = Expression.Parameter(type, "p");
            Expression propertyAccessExpression = Expression.MakeMemberAccess(param, property);
            LambdaExpression orderByExpression = Expression.Lambda(propertyAccessExpression, param);
            string methodName = ascending ? "OrderBy" : "OrderByDescending";
            MethodCallExpression resultExp = Expression.Call(typeof(Queryable), methodName, new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExpression));
            return source.Provider.CreateQuery<T>(resultExp);
        }
    }
}