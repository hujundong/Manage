using Comment.Model;
using Comment.Model.Query;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Comment.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        PageData<T> Search(Expression<Func<T, bool>> condition, string orderfield, int pageindex = 1, int pagesize = 10, bool isdesc = true);

        PageData<T> Search(QueryModel model, string orderfield, int pageindex = 1, int pagesize = 10, bool isdesc = true);

        PageData<T> GetAll(string orderfield, int pageindex = 1, int pagesize = 10, bool isdesc = true);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Delete(Expression<Func<T, bool>> where);

        T GetById(long Id);

        T GetById(string Id);

        T Get(Expression<Func<T, bool>> where);

        IEnumerable<T> GetAll();

        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);
    }
}