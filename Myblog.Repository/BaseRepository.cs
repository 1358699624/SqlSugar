
using MyBlog.IRepostitory;
using MyBlog.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace MyBlog.Repository
{
    /// <summary>
    /// 实现接口类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseRepository<T> : SimpleClient<T>, IBaseRepository<T> where T : class, new()
    {
        public BaseRepository(ISqlSugarClient context = null) : base(context)
        {
            if (context == null)
            {
                base.Context = new SqlSugarClient(new ConnectionConfig()
                {
                    DbType = SqlSugar.DbType.SqlServer,
                    InitKeyType = InitKeyType.Attribute,
                    IsAutoCloseConnection = true,
                    ConnectionString = AppConfig.Appsetting
                }) ;
                //如果用SqlSugarScope
                //base.Context=单例的SqlSugarScope
            }
            /*

            //如果不存在创建数据库
           base.Context.DbMaintenance.CreateDatabase();

            //base.Context.CodeFirst.SetStringDefaultLength(200).InitTables(typeof(CodeFirstTable1));//这样一个表就能成功创建了
            
            base.Context.CodeFirst.InitTables(
                typeof(CodeFirstTable),
                typeof(UserInfo),
                typeof(NewsTable)
                );//这样一个表就能成功创建了
            */
        }

        public async  Task<bool> CreateAsync(T t)
        {
            return await base.InsertAsync(t);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await base.DeleteByIdAsync(id);
        }

        public async Task<List<T>> GetAsync()
        {
            return await base.GetListAsync();
        }

        public async Task<List<T>> QueryAsync(Expression<Func<T, bool>> func)
        {
            return await base.GetListAsync(func);
        }


        public override async Task<bool> UpdateAsync(T t)
        {
            return await base.UpdateAsync(t);
        }

        public async  Task<T> FindAsync(int id)
        {
            return await base.GetByIdAsync(id);
        }

        public async Task<List<T>> QueryAsync(int page, int size, RefAsync<int> total)
        {
            return await base.Context.Queryable<T>()
                .ToPageListAsync(page, size,total);
        }

        public async Task<List<T>> QueryAsync(Expression<Func<T, bool>> func, int page, int size, RefAsync<int> total)
        {
            return await base.Context.Queryable<T>()
                .Where(func)
                 .ToPageListAsync(page, size, total);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> func)
        {
            return await base.GetSingleAsync(func);
        }
    }
}
