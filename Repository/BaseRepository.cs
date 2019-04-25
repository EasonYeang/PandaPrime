using IRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using LinqKit;
using EntityFramework.Utilities;
using Z.EntityFramework.Plus;
using Utility;

namespace Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _dbContext = DbContextFactory.GetCurrentDbContext();

        protected DbSet<TEntity> DbSet => _dbContext.Set<TEntity>();

        public TEntity GetSingleById(int id)
        {
            return DbSet.Find(id);
        }

        /// <summary>
        /// 查询是否存在
        /// </summary>
        /// <param name="expression">Lamda表达式</param>
        /// <returns>查询结果</returns>
        public bool Exist(Expression<Func<TEntity, bool>> expression)
        {
            int ct = DbSet.Count(expression);
            return ct > 0 ? true : false;
        }

        public List<TEntity> GetAll()
        {
            return DbSet.ToList();
        }

        public TEntity Add(TEntity entity)
        {
            return DbSet.Add(entity);
        }

        public TEntity Delete(TEntity entity)
        {
            return DbSet.Remove(entity);
        }

        public void AddOrUpdate(TEntity entity)
        {
            DbSet.AddOrUpdate(entity);
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        /// <summary>
        /// 获取实体列表
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns>实体集合</returns>
        public List<TEntity> GetList(Expression<Func<TEntity, bool>> where)
        {
            return DbSet.Where(where).ToList();
        }

        /// <summary>
        /// 根据条件查询实体集合并排序
        /// </summary>
        /// <typeparam name="Tkey"> 由 orderWhere 表示的函数返回的键类型</typeparam>
        /// <param name="where">查询条件</param>
        /// <param name="orderBy">用于从元素中提取键的函数</param>
        /// <returns>查询结果</returns>
        public List<TEntity> GetListOrder<Tkey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, Tkey>> orderBy)
        {
            return DbSet.Where(where).OrderBy(orderBy).ToList();
        }

        /// <summary>
        /// 根据条件查询实体集合并排序（分页）
        /// </summary>
        /// <typeparam name="Tkey"> 由 orderWhere 表示的函数返回的键类型</typeparam>
        /// <param name="where">查询条件</param>
        /// <param name="orderBy">用于从元素中提取键的函数</param>
        /// <returns></returns>
        public List<TEntity> GetListPagedOrder<Tkey>(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, Tkey>> orderBy, out int totalCount)
        {
            //获取总数
            totalCount = DbSet.AsExpandable().Where(where).Count();
            //需要增加AsExpandable(),否则查询的是所有数据到内存，然后再排序  AsExpandable是linqkit.dll中的方法
            return DbSet.AsExpandable().Where(where).OrderBy(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 根据条件查询实体集合并排序（分页）
        /// </summary>
        /// <typeparam name="Tkey"> 由 orderWhere 表示的函数返回的键类型</typeparam>
        /// <param name="where">查询条件</param>
        /// <param name="orderBy">用于从元素中提取键的函数</param>
        /// <returns></returns>
        public List<TEntity> GetListPagedOrderDec<Tkey>(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, Tkey>> orderBy, out int totalCount)
        {
            //获取总数
            totalCount = DbSet.AsExpandable().Where(where).Count();
            //需要增加AsExpandable(),否则查询的是所有数据到内存，然后再排序  AsExpandable是linqkit.dll中的方法
            return DbSet.AsExpandable().Where(where).OrderByDescending(orderBy).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="where">条件</param>
        public void DeleteByConditon(Expression<Func<TEntity, bool>> where)
        {
            List<TEntity> entitys = DbSet.Where(where).ToList();
            entitys.ForEach(m => _dbContext.Entry<TEntity>(m).State = EntityState.Deleted);
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="list">实体集合</param>
        public void BatchInsert(IEnumerable<TEntity> list)
        {
            EFBatchOperation.For(_dbContext, DbSet).InsertAll(list);
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="where">条件：x =>x.LastLoginDate<DateTime.Now.AddYears(-2)</param>
        /// <param name="updateFactory">要更新的实体：x => new User() { IsSoftDeleted = 1 }</param>
        /// <returns>受影响的行数</returns>
        public int BatchUpdate(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TEntity>> updateFactory)
        {
            return DbSet.Where(where).Update(updateFactory);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns>受影响的行数</returns>
        public int BatchDelete(Expression<Func<TEntity, bool>> where)
        {
            return DbSet.Where(where).Delete();
        }

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns>实体</returns>
        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.FirstOrDefault(predicate);
        }
    }
}
