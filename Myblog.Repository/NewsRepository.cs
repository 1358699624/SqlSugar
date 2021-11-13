
using MyBlog.IRepository;
using MyBlog.Model;
using MyBlog.Repository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Myblog.Repository
{
    public  class NewsRepository:BaseRepository <NewsTable>,INewsRepository
    {
        public async override Task<List<NewsTable>> GetAsync()
        {
            return await base.Context.Queryable<NewsTable>()
                .Mapper(c => c.codeFirstTable, c => c.codeFirstId,c=>c.codeFirstTable.Id)
                .Mapper(c => c.userInfo, c => c.userInfoId, c => c.userInfo.Id)
                .ToListAsync();
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="page">当前页</param>
        /// <param name="size">页大小</param>
        /// <param name="total">返回total</param>
        /// <returns></returns>
       public async override Task<List<NewsTable>> QueryAsync(int page,int size,RefAsync<int> total)
        {
                        return await base.Context.Queryable<NewsTable>()
                            .Mapper(c => c.codeFirstTable, c => c.codeFirstId,c=>c.codeFirstTable.Id)
                            .Mapper(c => c.userInfo, c => c.userInfoId, c => c.userInfo.Id)
                            .ToPageListAsync(page, size, total);
        }
    }
}
