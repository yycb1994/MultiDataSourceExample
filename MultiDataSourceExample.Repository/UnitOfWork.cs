using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace MultiDataSourceExample.Repository
{
    /// <summary>
    /// 泛型接口定义工作单元模式，用于管理仓储和事务
    /// </summary>
    /// <typeparam name="TContext">工作单元所需的DbContext类型</typeparam>
    public interface IUnitOfWork<TContext> : IDisposable where TContext : DbContext
    {
        /// <summary>
        /// 获取特定实体类型的仓储
        /// </summary>
        IBaseRepository<TEntity> Repository<TEntity>() where TEntity : class, new();

        /// <summary>
        /// 开始事务
        /// </summary>
        void BeginTran();

        /// <summary>
        /// 提交事务
        /// </summary>
        void Commit();

        /// <summary>
        /// 异步提交事务
        /// </summary>
        Task CommitAsync();
    }

    /// <summary>
    /// 实现工作单元模式的基本功能
    /// </summary>
    /// <typeparam name="TContext">工作单元所需的DbContext类型</typeparam>
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly ConcurrentDictionary<string, dynamic> _repositories;
        private IDbContextTransaction _transaction;
        private bool _disposed;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="context">工作单元所需的DbContext实例</param>
        public UnitOfWork(TContext context)
        {
            _context = context;
            _repositories = new ConcurrentDictionary<string, dynamic>();
        }

        /// <inheritdoc/>
        public IBaseRepository<TEntity> Repository<TEntity>() where TEntity : class, new()
        {
            var type = typeof(TEntity).Name;

            return _repositories.GetOrAdd(type, (t) =>
            {
                var repositoryType = typeof(BaseRepository<>);
                return Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context) as IBaseRepository<TEntity>;
            });
        }

        /// <inheritdoc/>
        public void BeginTran()
        {
            if (_transaction != null)// 防止重复开始事务
            {
                throw new InvalidOperationException("A transaction is already in progress.");
            }
            _transaction = _context.Database.BeginTransaction();
        }

        /// <inheritdoc/>
        public void Commit()
        {
            try
            {
                _context.SaveChanges();
                _transaction?.Commit();
            }
            catch
            {
                _transaction?.Rollback();
                throw;
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null;
            }
        }

        /// <inheritdoc/>
        public async Task CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                if (_transaction != null)
                {
                    await _transaction.CommitAsync();
                }
            }
            catch
            {
                if (_transaction != null)
                {
                    await _transaction.RollbackAsync();
                }

                throw;
            }
            finally
            {
                if (_transaction != null)
                {
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            if (!_disposed)
            {
                _transaction?.Dispose();
                _context.Dispose();
                _disposed = true;
            }

            GC.SuppressFinalize(this);
        }
    }

}
