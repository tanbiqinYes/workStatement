using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CCWOnlineAPI.Management
{
    /// <summary>
    /// IOC注入 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : class, new()
    {
        #region 属性
        /// <summary>
        ///  获取 当前实体的查询数据集
        /// </summary>
        IQueryable<T> Entities { get; }

        #endregion 属性

        #region 方法
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isSave">是否执行保存</param>
        /// <returns></returns>
        string Insert(T entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<string> InsertAsync(T entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="isSave">是否执行保存</param>
        /// <returns></returns>
        string Insert(IEnumerable<T> entities);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<string> InsertAsync(IEnumerable<T> entities);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isSave">是否执行保存</param>
        /// <returns></returns>
        string Delete(T entity);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<string> DeleteAsync(T entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        string Delete(IEnumerable<T> entities);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<string> DeleteAsync(IEnumerable<T> entities);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isSave">是否执行保存</param>
        /// <returns></returns>
        string Update(T entity);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<string> UpdateAsync(T entity);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        string Update(IEnumerable<T> entities);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<string> UpdateAsync(IEnumerable<T> entities);

        ///// <summary>
        ///// 查询数据
        ///// </summary>
        ///// <param name="whereLambda">查询表达式</param>
        ///// <returns>实体</returns>
        //T Find(Expression<Func<T, bool>> whereLambda);
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="whereLambda"></param>
        ///// <returns></returns>
        //Task<T> FindAsync(Expression<Func<T, bool>> whereLambda);

        #endregion 方法
    }
}
