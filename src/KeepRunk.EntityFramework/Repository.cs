using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using EntityFramework.Extensions;
using KeepRunk.EntityFramework.Extensions;
using KeepRunk.Model.Core;

namespace KeepRunk.EntityFramework
{
    public class Repository<T> where T : class, IRootEntity
    {
        /// <summary>
        /// 清除缓存
        /// </summary>
        protected virtual void OnCacheChanged()
        {
            //var enableCache = typeof(T).GetAttributeValue((CacheSettingAttribute dna) => dna.EnableCache);
            //if (enableCache)
            //    CacheManager.Instance.FindAndRemove(CacheTable<T>.Key());
        }

        /// <summary>
        ///  添加操作
        /// </summary>
        /// <param name="db">dbContext</param>
        /// <param name="model">当前对象</param>
        /// <returns>返回影响条数</returns>
        protected int Add(DbContextBase db, T model)
        {
            db.Set<T>().Add(model);
            var result = db.SaveChanges();
            OnCacheChanged();
            return result;
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="db">dbContext对象</param>
        /// <param name="list">集合对象</param>
        /// <returns>返回影响条数</returns>
        protected int AddRange(DbContextBase db, List<T> list)
        {
            db.Set<T>().AddRange(list);
            OnCacheChanged();
            return db.SaveChanges();
        }

        /// <summary>
        /// 删除操作,删除当前model对象,对象必须在同一个db里才能执行删除
        /// </summary>
        /// <param name="db">dbContext对象</param>
        /// <param name="model">删除对象</param>
        /// <returns>返回影响条数</returns>
        protected int Delete(DbContextBase db, T model)
        {
            db.Set<T>().Remove(model);
            OnCacheChanged();
            return db.SaveChanges();
        }

        /// <summary>
        /// 批量删除 exp=null 删除所有数据
        /// </summary>
        /// <param name="db">dbContext对象</param>
        /// <param name="exp">删除条件</param>
        /// <returns>返回受影响的条数</returns>
        protected int Delete(DbContextBase db, Expression<Func<T, bool>> exp)
        {
            OnCacheChanged();
            return db.Set<T>().Where(exp).Delete();
        }

        /// <summary>
        /// 更新方法
        /// </summary>
        /// <param name="db">dbContext对象</param>
        /// <param name="model">更新对象</param>
        /// <returns>返回受影响的条数</returns>
        public int Update(DbContextBase db, T model)
        {
            OnCacheChanged();
            db.Entry(model).State = EntityState.Modified;
            return db.SaveChanges();
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="db">dbContext对象</param>
        /// <param name="filterExpression">过滤条件</param>
        /// <param name="updateExpression">更新参数</param>
        /// <returns>返回受影响的条数</returns>
        public int Update(DbContextBase db, Expression<Func<T, bool>> filterExpression, Expression<Func<T, T>> updateExpression)
        {
            OnCacheChanged();
            return db.Set<T>().Where(filterExpression).Update(updateExpression);
        }

        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="db">dbContext对象</param>
        /// <param name="key">主键</param>
        /// <returns>返回当前对象</returns>
        protected T GetById(DbContextBase db, object key)
        {
            return db.Set<T>().Find(key);
        }

        /// <summary>
        /// SQL查询
        /// </summary>
        /// <param name="db">dbContext对象</param>
        /// <param name="query">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>返回IQueryable</returns>
        protected IQueryable<T> Get(DbContextBase db, string query, params object[] parameters)
        {
            return db.Database.SqlQuery<T>(query, parameters).AsQueryable();
        }

        /// <summary>
        /// 查询操作,返回数据调用ToList()
        /// </summary>
        /// <param name="db">dbContext对象</param>
        /// <param name="exp">表达式条件</param>
        /// <returns>返回IQueryable,返回数据调用ToList()</returns>
        protected IQueryable<T> GetQueryable(DbContextBase db, Expression<Func<T, bool>> exp)
        {
            return db.Set<T>().AsNoTracking().Get(exp);
        }
    }
}
