using IRepository;
using IService;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Service
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class
    {
        private readonly IBaseRepository<TEntity> _baseRepository;

        public BaseService(IBaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns>数据列表</returns>
        public List<TEntity> GetAll()
        {
            return _baseRepository.GetAll();
        }

        /// <summary>
        /// 创建实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>实体</returns>
        public TEntity Add(TEntity entity)
        {
            return _baseRepository.Add(entity);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>实体</returns>
        public TEntity Delete(TEntity entity)
        {
            return _baseRepository.Delete(entity);
        }

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="entity">实体</param>
        public void AddOrUpdate(TEntity entity)
        {
            _baseRepository.AddOrUpdate(entity);
        }

        /// <summary>
        /// 将操作保存到数据库
        /// </summary>
        /// <returns>成功返回的操作行数</returns>
        public int SaveChanges()
        {
            return _baseRepository.SaveChanges();
        }

        public TEntity GetSingleById(int id)
        {
            return _baseRepository.GetSingleById(id);
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> where)
        {
            return _baseRepository.GetList(where);
        }

        /// <summary>
        /// 根据条件查询实体集合并排序
        /// </summary>
        /// <typeparam name="Tkey"> 由 orderWhere 表示的函数返回的键类型</typeparam>
        /// <param name="seleWhere">查询条件</param>
        /// <param name="orderWhere">用于从元素中提取键的函数</param>
        /// <returns>查询结果</returns>
        public List<TEntity> GetListOrder<Tkey>(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, Tkey>> orderBy)
        {
            return _baseRepository.GetListOrder(where, orderBy);
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="where">删除条件</param>
        /// <returns>返回受影响行数</returns>
        public void DeleteByConditon(Expression<Func<TEntity, bool>> where)
        {
            _baseRepository.DeleteByConditon(where);
        }

        /// <summary>
        /// 查询是否存在
        /// </summary>
        /// <param name="expression">Lamda表达式</param>
        /// <returns>查询结果</returns>
        public bool Exist(Expression<Func<TEntity, bool>> expression)
        {
            return _baseRepository.Exist(expression);
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="list">实体集合</param>
        public void BatchInsert(IEnumerable<TEntity> list)
        {
            _baseRepository.BatchInsert(list);
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="where">条件：x =>x.LastLoginDate<DateTime.Now.AddYears(-2)</param>
        /// <param name="updateFactory">要更新的实体：x => new User() { IsSoftDeleted = 1 }</param>
        /// <returns>受影响的行数</returns>
        public int BatchUpdate(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, TEntity>> updateFactory)
        {
            return _baseRepository.BatchUpdate(where, updateFactory);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="where">条件</param>
        /// <returns>受影响的行数</returns>
        public int BatchDelete(Expression<Func<TEntity, bool>> where)
        {
            return _baseRepository.BatchDelete(where);
        }

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns>实体</returns>
        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _baseRepository.FirstOrDefault(predicate);
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
            return _baseRepository.GetListPagedOrder<Tkey>(pageIndex, pageSize, where, orderBy, out totalCount);
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
            return _baseRepository.GetListPagedOrderDec<Tkey>(pageIndex, pageSize, where, orderBy, out totalCount);
        }
    }
}
