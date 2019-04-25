using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace IRepository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// 查找单个
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>查找结果</returns>
        TEntity GetSingleById(int id);

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>数据列表</returns>
        List<TEntity> GetAll();

        /// <summary>
        /// 创建实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>实体</returns>
        TEntity Add(TEntity entity);

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>实体</returns>
        TEntity Delete(TEntity entity);

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="entity">实体</param>
        void AddOrUpdate(TEntity entity);

        /// <summary>
        /// 将操作保存到数据库
        /// </summary>
        /// <returns>成功返回的操作行数</returns>
        int SaveChanges();

        List<TEntity> GetList(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// 根据条件查询实体集合并排序
        /// </summary>
        /// <typeparam name="Tkey"> 由 orderWhere 表示的函数返回的键类型</typeparam>
        /// <param name="seleWhere">查询条件</param>
        /// <param name="orderWhere">用于从元素中提取键的函数</param>
        /// <returns>查询结果</returns>
        List<TEntity> GetListOrder<Tkey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, Tkey>> orderBy);

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="where">条件</param>
        void DeleteByConditon(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// 查询是否存在
        /// </summary>
        /// <param name="expression">Lamda表达式</param>
        /// <returns>查询结果</returns>
        bool Exist(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="list">实体集合</param>
        void BatchInsert(IEnumerable<TEntity> list);

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="where">条件：x =>x.LastLoginDate<DateTime.Now.AddYears(-2)</param>
        /// <param name="updateFactory">要更新的实体：x => new User() { IsSoftDeleted = 1 }</param>
        /// <returns>受影响的行数</returns>
        int BatchUpdate(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TEntity>> updateFactory);

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns>受影响的行数</returns>
        int BatchDelete(Expression<Func<TEntity, bool>> where);

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns>实体</returns>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 根据条件查询实体集合并排序（分页）
        /// </summary>
        /// <typeparam name="Tkey"> 由 orderWhere 表示的函数返回的键类型</typeparam>
        /// <param name="where">查询条件</param>
        /// <param name="orderBy">用于从元素中提取键的函数</param>
        /// <returns></returns>
        List<TEntity> GetListPagedOrder<Tkey>(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, Tkey>> orderBy, out int totalCount);

        /// <summary>
        /// 根据条件查询实体集合并排序（分页）
        /// </summary>
        /// <typeparam name="Tkey"> 由 orderWhere 表示的函数返回的键类型</typeparam>
        /// <param name="where">查询条件</param>
        /// <param name="orderBy">用于从元素中提取键的函数</param>
        /// <returns></returns>
        List<TEntity> GetListPagedOrderDec<Tkey>(int pageIndex, int pageSize, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, Tkey>> orderBy, out int totalCount);
    }
}
