using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.IRepostitory
{
     public  interface IBaseRepository<T>  where T:class ,new()
    {
        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task<bool> CreateAsync(T t);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        Task<bool> UpdateAsync(T t);
        /// <summary>
        /// 根据id 查询T类型
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> FindAsync(int id);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync(int id);


        /// <summary>
        /// 查询全部数据
        /// </summary>
        /// <returns></returns>
        Task<T> GetAsync(Expression<Func<T, bool>> func);



        /// <summary>
        /// 查询全部数据
        /// </summary>
        /// <returns></returns>
        Task<List<T>> GetAsync();


        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        Task<List<T>> QueryAsync(Expression<Func<T,bool>> func);


      

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        Task<List<T>> QueryAsync(int page,int size,RefAsync<int> total);



        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        Task<List<T>> QueryAsync(Expression<Func<T, bool>> func, int page, int size, RefAsync<int> total);
    }
}
