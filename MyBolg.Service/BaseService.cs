
using MyBlog.IRepostitory;
using MyBlog.IService;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Myblog.Service
{
    public class BaseService<T> : IBaseService<T> where T : class, new()
    {
        //从子类的构造函数中传入

        protected IBaseRepository<T> _iBaseRepository;

        public async Task<bool> CreateAsync(T t)
        {
            return await _iBaseRepository.CreateAsync(t);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _iBaseRepository.DeleteAsync(id);
        }

        public   async Task<T> FindAsync(int id)
        {
            return await _iBaseRepository.FindAsync(id);
        }

        public async Task<List<T>> GetAsync()
        {
            return await _iBaseRepository.GetAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> func)
        {
            return await _iBaseRepository.GetAsync(func);
        }

        public async Task<List<T>> QueryAsync(Expression<Func<T, bool>> func)
        {
            return await _iBaseRepository.QueryAsync(func);
        }

        public async Task<List<T>> QueryAsync(int page, int size, RefAsync<int> total)
        {
            return await _iBaseRepository.QueryAsync(page, size, total);
        }

        public async Task<List<T>> QueryAsync(Expression<Func<T, bool>> func, int page, int size, RefAsync<int> total)
        {
            return await _iBaseRepository.QueryAsync(func,page, size, total);
        }

        public  async Task<bool> UpdateAsync(T t)
        {
            return await _iBaseRepository.UpdateAsync(t);
        }
    }
}
