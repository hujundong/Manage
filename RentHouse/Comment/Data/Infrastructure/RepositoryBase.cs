using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data;
using System.Linq.Expressions;
using Comment.Data;
using Comment.Data.Infrastructure;
using Comment.Model;
using Comment.Model.Query;
using Comment.Model.SearchModel;
using System.Data.Entity.Core.Objects;

namespace Comment.Repositiry
{
    public abstract class RepositoryBase<T> where T : class
    {
        protected HouseContext dataContext;
        protected readonly IDbSet<T> dbset;

        protected RepositoryBase(IDatabaseFactory databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            dbset = DataContext.Set<T>();
        }

        protected IDatabaseFactory DatabaseFactory
        {
            get;
            private set;
        }

        protected HouseContext DataContext
        {
            get { return dataContext ?? (dataContext = DatabaseFactory.Get()); }
        }

        protected virtual Func<T, object> DefaultOrderBy
        {
            get { return x => x.GetHashCode(); }
        }

        public PageData<T> Search(Expression<Func<T, bool>> condition, string orderfield, int pageindex = 1, int pagesize = 10, bool isdesc = true)
        {
            if (condition == null)
                throw new ArgumentNullException("entity");

            var query = dbset.Where(condition);
            PageData<T> pageData = new PageData<T>();
            pageData.TotalCount = query.Count();
            pageData.DataList = query.OrderBy(orderfield, isdesc).Skip((pageindex - 1) * pagesize).Take(pagesize).ToList();

            return pageData;
        }

        public PageData<T> Search(QueryModel model, string orderfield, int pageindex = 1, int pagesize = 10, bool isdesc = true)
        {
            if (model == null)
                throw new ArgumentNullException("entity");
            var query = dbset.Where(model);
            PageData<T> pageData = new PageData<T>();
            pageData.TotalCount = query.Count();
            pageData.DataList = query.OrderBy(orderfield, isdesc).Skip((pageindex - 1) * pagesize).Take(pagesize).ToList();

            return pageData;
        }

        public PageData<T> GetAll(string orderfield, int pageindex = 1, int pagesize = 10, bool isdesc = true)
        {
            PageData<T> pageData = new PageData<T>();
            pageData.TotalCount = dbset.Count();
            pageData.DataList = dbset.OrderBy(orderfield, isdesc).Skip((pageindex - 1) * pagesize).Take(pagesize).ToList();

            return pageData;
        }

        public virtual void Add(T entity)
        {
            dbset.Add(entity);
        }

        public virtual void Update(T entity)
        {
            dbset.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            dbset.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbset.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbset.Remove(obj);
        }

        public virtual T GetById(long id)
        {
            return dbset.Find(id);
        }

        public virtual T GetById(string id)
        {
            return dbset.Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return dbset.ToList();
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where).ToList();
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where).FirstOrDefault<T>();
        }

        #region 直接执行SQL语句

        public virtual IEnumerable<T> GetWithRawSql(string query, params object[] parameters)
        {
            DbSet<T> dbSet;
            dbSet = DataContext.Set<T>();
            return dbSet.SqlQuery(query, parameters).ToList();
        }

        public virtual IEnumerable<T> GetWhithdbSql(string query, params object[] parameters)
        {
            return DataContext.Database.SqlQuery<T>(query, parameters);
        }

        #endregion 直接执行SQL语句
    }
}