using MyBlog.IRepository;
using MyBlog.Model;
using MyBlog.Repository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myblog.Repository
{
    public  class SoftRepository : BaseRepository<Soft>, ISoftRepository
    {
        /// <summary>
        /// 分页,多表联查
        /// </summary>
        /// <param name="page">当前页</param>
        /// <param name="size">页大小</param>
        /// <param name="total">返回total</param>
        /// <returns></returns>
        public async override Task<List<Soft>> QueryAsync(int page, int size, RefAsync<int> total)
        {
            //返回UserAndNews类
            var list2 =
                await base.Context.Queryable<NewsTable>()
            .LeftJoin<UserInfo>((o, cus) => o.userInfoId == cus.Id)
            //.LeftJoin<OrderItem>((o, cus, oritem) => o.Id == oritem.OrderId)
            //.LeftJoin<OrderItem>((o, cus, oritem, oritem2) => o.Id == oritem2.OrderId)
            //.Where(o => o.Id == 1)
            .Select((o, cus) => new Soft { Id = o.Id, USCC = o.Text, SoftName = cus.UserName })
            .ToPageListAsync(page, size, total);
            return list2;
        }

        /// <summary>
        /// 使用sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<DataTable> GetSoftAsync(string uproc,string dic)
        {


            var nameP = new SugarParameter("@softname", dic);

            DataTable  v =  await  base.Context.Ado.UseStoredProcedure().GetDataTableAsync(uproc, nameP);

            //parameters 暂时不能设置为动态

            var v2 = await base.Context
                .Ado.SqlQueryAsync<Soft>
                ("select * from Soft where  id in(@ids)",
                       new { ids = new int[] { 6, 7, 100 } });

            return v;
        }

    }
}

