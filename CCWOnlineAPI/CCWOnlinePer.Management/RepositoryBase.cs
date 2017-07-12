using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace CCWOnlineAPI.Management
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class RepositoryBase<T> : IRepository<T> where T : class, new()
    {
        /// <summary>
        /// 容器
        /// </summary>
        public DbContext context;

        /// <summary>
        /// 
        /// </summary>
        public RepositoryBase()
        {
            if (context == null)
            {
                this.context = DBContextFactory.GetCurrentDbContext();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public IQueryable<T> Entities
        {
            get { return context.Set<T>(); }
        }
        /// <summary>
        /// 
        /// </summary>
        public Database Database
        {
            get
            {
                return context.Database;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string Insert(T entity)
        {
            context.Set<T>().Add(entity);
            context.SaveChanges();
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<string> InsertAsync(T entity)
        {
            context.Set<T>().Add(entity);
            await context.SaveChangesAsync();
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public string Insert(IEnumerable<T> entities)
        {
            entities.ToList().ForEach(p => context.Entry(p).State = EntityState.Added);
            context.Set<T>().AddRange(entities);
            context.SaveChanges();
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task<string> InsertAsync(IEnumerable<T> entities)
        {
            entities.ToList().ForEach(p => context.Entry(p).State = EntityState.Added);
            context.Set<T>().AddRange(entities);
            await context.SaveChangesAsync();
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string Delete(T entity)
        {
            context.Set<T>().Remove(entity);
            context.SaveChanges();
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<string> DeleteAsync(T entity)
        {
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public string Delete(IEnumerable<T> entities)
        {
            entities.ToList().ForEach(p => context.Entry(p).State = EntityState.Deleted);
            context.Set<T>().RemoveRange(entities);
            context.SaveChanges();
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task<string> DeleteAsync(IEnumerable<T> entities)
        {
            entities.ToList().ForEach(p => context.Entry(p).State = EntityState.Deleted);
            context.Set<T>().RemoveRange(entities);
            await context.SaveChangesAsync();
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string Update(T entity)
        {
                context.Entry<T>(entity).State = EntityState.Modified;
                context.SaveChanges();

                return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<string> UpdateAsync(T entity)
        {
                context.Entry<T>(entity).State = EntityState.Modified;
                await context.SaveChangesAsync();

                return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public string Update(IEnumerable<T> entities)
        {
                foreach (T entity in entities)
                {
                    context.Entry<T>(entity).State = EntityState.Modified;
                }

                context.SaveChanges();
                return null;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public async Task<string> UpdateAsync(IEnumerable<T> entities)
        {
                foreach (T entity in entities)
                {
                    context.Entry<T>(entity).State = EntityState.Modified;
                }
                await context.SaveChangesAsync();
                return null;

        }

        ///// <summary>
        ///// 查询数据
        ///// </summary>
        ///// <param name="whereLambda">查询表达式</param>
        ///// <returns>实体</returns>
        public T FindOne(System.Linq.Expressions.Expression<System.Func<T, bool>> whereLambda)
        {
            T _entity = context.Set<T>().FirstOrDefault<T>(whereLambda);
            return _entity;
        }
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="whereLambda"></param>
        ///// <returns></returns>
        public async Task<T> FindAsync(System.Linq.Expressions.Expression<Func<T, bool>> whereLambda)
        {
            T _entity = await context.Set<T>().FirstOrDefaultAsync<T>(whereLambda);
            return _entity;
        }
    }
}
