using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MultiDataSourceExample.Repository
{
    /// <summary>
    /// 表示基础仓储的接口，用于对实体进行增删改查操作。
    /// </summary>
    /// <typeparam name="TEntity">实体类型。</typeparam>
    /// <remarks>
    /// 作者：于春波
    /// 创建日期：2024/1/29
    /// </remarks>
    public interface IBaseRepository<TEntity> where TEntity : class, new() 
    {
        /// <summary>
        /// 根据 ID 获取实体。
        /// </summary>
        /// <param name="id">实体的 ID。</param>
        /// <returns>找到的实体，如果找不到则为 null。</returns>
        TEntity GetById(object id);

        /// <summary>
        /// 获取所有实体。
        /// </summary>
        /// <returns>所有实体的集合。</returns>
        List<TEntity> GetAll();

        /// <summary>
        /// 根据条件查找实体。
        /// </summary>
        /// <param name="predicate">筛选条件的表达式。</param>
        /// <returns>符合条件的实体的集合。</returns>
        List<TEntity> QueryList(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 根据条件查找实体的查询对象。
        /// </summary>
        /// <param name="predicate">筛选条件的表达式。</param>
        /// <returns>符合条件的实体的查询对象。</returns>
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 根据条件查找实体的第一个或默认实体。
        /// </summary>
        /// <param name="predicate">筛选条件的表达式。</param>
        /// <returns>符合条件的第一个或默认实体。</returns>
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 添加实体。
        /// </summary>
        /// <param name="entity">要添加的实体。</param>
        void Add(TEntity entity);

        /// <summary>
        /// 批量添加实体。
        /// </summary>
        /// <param name="entities">要添加的实体集合。</param>
        void AddRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// 删除实体。
        /// </summary>
        /// <param name="entity">要删除的实体。</param>
        void Remove(TEntity entity);

        /// <summary>
        /// 批量删除实体。
        /// </summary>
        /// <param name="entities">要删除的实体集合。</param>
        void RemoveRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// 批量删除实体。
        /// </summary>
        /// <param name="entities">要删除的实体主键。</param>
        void RemoveRangeByPks(IEnumerable<object> pks);

        /// <summary>
        /// 更新实体。
        /// </summary>
        /// <param name="entity">要更新的实体。</param>
        void Update(TEntity entity);


        /// <summary>
        /// 根据 ID 获取实体。
        /// </summary>
        /// <param name="id">实体的 ID。</param>
        /// <returns>找到的实体，如果找不到则为 null。</returns>
        Task<TEntity> GetByIdAsync(object id);

        /// <summary>
        /// 获取所有实体。
        /// </summary>
        /// <returns>所有实体的集合。</returns>
        Task<List<TEntity>> GetAllAsync();

        /// <summary>
        /// 根据条件查找实体。
        /// </summary>
        /// <param name="predicate">筛选条件的表达式。</param>
        /// <returns>符合条件的实体的集合。</returns>
        Task<List<TEntity>> QueryListAsync(Expression<Func<TEntity, bool>> predicate);


        /// <summary>
        /// 根据条件查找实体的第一个或默认实体。
        /// </summary>
        /// <param name="predicate">筛选条件的表达式。</param>
        /// <returns>符合条件的第一个或默认实体。</returns>
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 添加实体。
        /// </summary>
        /// <param name="entity">要添加的实体。</param>
        Task AddAsync(TEntity entity);

        /// <summary>
        /// 批量添加实体。
        /// </summary>
        /// <param name="entities">要添加的实体集合。</param>
        Task AddRangeAsync(IEnumerable<TEntity> entities);
    }
}
