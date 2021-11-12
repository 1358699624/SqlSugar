using MyBlog.IRepository;
using MyBlog.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBlog.Model;
using System.Linq.Expressions;

namespace Myblog.Repository
{
    public class UserInfoRepository : BaseRepository<UserInfo>, IUserInfoRepository
    {
        /*
        public async Task<List<UserInfo>> GetUserInfoLeftNews(Expression<Func<UserInfo, NewsTable, bool>> func)
        {
            return await base.Context.Queryable<Func<UserInfo,NewsTable,bool>>(func);
        }*/
    }
}
