using MyBlog.IRepository;
using MyBlog.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyBlog.Model;
using System.Linq.Expressions;
using SqlSugar;

namespace Myblog.Repository
{
    public class UserToNewsRepository : BaseRepository<UserAndNews>, IUserToNewsRepository
    {
        /// <summary>
        /// 分页,多表联查
        /// </summary>
        /// <param name="page">当前页</param>
        /// <param name="size">页大小</param>
        /// <param name="total">返回total</param>
        /// <returns></returns>
        public async override Task<List<UserAndNews>> QueryAsync(int page, int size, RefAsync<int> total)
        {
            /*
            //返回匿名

            var  list =
                await base.Context.Queryable<NewsTable>()
            .LeftJoin<UserInfo>((o, cus) => o.userInfoId == cus.Id)
            //.LeftJoin<OrderItem>((o, cus, oritem) => o.Id == oritem.OrderId)
            //.LeftJoin<OrderItem>((o, cus, oritem, oritem2) => o.Id == oritem2.OrderId)
            //.Where(o => o.Id == 1)
            .Select((o, cus) => new  { Id = o.Id, Text = o.Text, UserName = cus.UserName })
            .ToPageListAsync(page, size, total);
            //.Mapper(o => o.codeFirstId, o => c.codeFirstId, c => c.codeFirstTable.Id)
            //.Mapper(o => o.userInfo, o => c.userInfoId, c => c.userInfo.Id)
            */


            //返回UserAndNews类
            var list2 =
                await base.Context.Queryable<NewsTable>()
            .LeftJoin<UserInfo>((o, cus) => o.userInfoId == cus.Id)
            //.LeftJoin<OrderItem>((o, cus, oritem) => o.Id == oritem.OrderId)
            //.LeftJoin<OrderItem>((o, cus, oritem, oritem2) => o.Id == oritem2.OrderId)
            //.Where(o => o.Id == 1)
            .Select((o, cus) => new UserAndNews { Id = o.Id, Text = o.Text, UserName = cus.UserName })
            .ToPageListAsync(page, size, total);
            return list2;
        }




    }
}
